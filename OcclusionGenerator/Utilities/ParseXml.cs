using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace OcclusionGenerator {
    public class ParseXml {
        public static MloInterior GetMloInterior(XDocument XDocItyp, XDocument XDocImap) {
            List<Portal> mloPortals = GetPortals(XDocItyp);
            List<Room> mloRooms = GetRooms(XDocItyp);
            string mloName = GetInstanceName(XDocImap);
            Vector3? mloPosition = GetInstancePosition(XDocImap);
            MloInterior mloInterior = new MloInterior(mloName, mloPosition, mloRooms, mloPortals);

            return mloInterior;
        }

        public static List<Room> GetRooms(XDocument XDoc) {
            List<Room> mloRooms = new List<Room>();
            XElement archetypes = XDoc.Root.Element("archetypes");

            foreach (XElement archetype in archetypes.Elements("Item")) {
                if (archetype.Attribute("type").Value == "CMloArchetypeDef") {
                    XElement rooms = archetype.Element("rooms");

                    foreach (XElement room in rooms.Elements("Item")) {
                        mloRooms.Add(new Room(
                            room.Element("name").Value,
                            uint.Parse(room.Element("portalCount").Attribute("value").Value, CultureInfo.InvariantCulture),
                            (uint)rooms.Elements("Item").ToList().IndexOf(room)
                            ));
                    }
                }
            }

            return mloRooms;
        }

        public static List<Entity> GetPortalEntities(XElement mlo, XElement portal) {
            string attachedObjectsArray = portal.Element("attachedObjects").Value.ToString();
            string[] attachedObjects = attachedObjectsArray.Split(' ');
            List<Entity> attachedEntities = new List<Entity>();

            foreach (string i in attachedObjects) {
                int index;
                bool found = int.TryParse(i, out index);
                if (found) {
                    Entity entity = new Entity(mlo.Element("entities").Elements("Item").ElementAt(index).Element("archetypeName").Value);

                    if (entity.name.Contains("door")) {
                        entity.entityType = EntityType.ENTITY_TYPE_DOOR;
                    } else if (entity.name.Contains("glass") || entity.name.Contains("window")) {
                        entity.entityType = EntityType.ENTITY_TYPE_GLASS;
                    }

                    attachedEntities.Add(entity);
                }

            }
            return attachedEntities;
        }

        public static List<Portal> GetPortals(XDocument XDoc) {
            List<Portal> mloPortals = new List<Portal>();
            XElement archetypes = XDoc.Root.Element("archetypes");

            foreach (XElement archetype in archetypes.Elements("Item")) {
                if (archetype.Attribute("type").Value == "CMloArchetypeDef") {
                    XElement portals = archetype.Element("portals");
                    uint portalIndex = 0;

                    foreach (XElement portal in portals.Elements("Item")) {

                        List<Entity> attachedEntities = GetPortalEntities(archetype, portal);

                        mloPortals.Add(new Portal(
                            uint.Parse(portal.Element("roomFrom").Attribute("value").Value, CultureInfo.InvariantCulture),
                            uint.Parse(portal.Element("roomTo").Attribute("value").Value, CultureInfo.InvariantCulture),
                            uint.Parse(portal.Element("flags").Attribute("value").Value, CultureInfo.InvariantCulture),
                            attachedEntities,
                            portalIndex
                            ));
                        portalIndex++;
                    }
                }
            }

            return mloPortals;
        }

        public static Vector3? GetInstancePosition(XDocument XDoc) {
            XElement entities = XDoc.Root.Element("entities");

            foreach (XElement entity in entities.Elements("Item")) {
                if (entity.Attribute("type").Value == "CMloInstanceDef") {
                    Vector3 position = new Vector3(
                        float.Parse(entity.Element("position").Attribute("x").Value, CultureInfo.InvariantCulture),
                        float.Parse(entity.Element("position").Attribute("y").Value, CultureInfo.InvariantCulture),
                        float.Parse(entity.Element("position").Attribute("z").Value, CultureInfo.InvariantCulture));
                    return position;
                }
            }
            return null;
        }

        public static string GetInstanceName(XDocument XDoc) {
            XElement entities = XDoc.Root.Element("entities");

            foreach (XElement entity in entities.Elements("Item")) {
                if (entity.Attribute("type").Value == "CMloInstanceDef") {
                    string mloArchetypeName = entity.Element("archetypeName").Value;
                    return mloArchetypeName;
                }
            }

            return null;
        }

        public static void CommentOcclFile(XDocument XDoc, PortalInfoList portalInfoList, MloInterior mloInterior) {
            XElement portalInfos = XDoc.Root.Element("PortalInfoList");
            XElement pathNodeKeys = XDoc.Root.Element("PathNodeList");
            List<Node> nodes = Node.GetNodes(portalInfoList, mloInterior);

            foreach (XElement portalInfo in portalInfos.Elements("Item")) {
                XComment XCom = new XComment(portalInfo.ElementsBeforeSelf().Count().ToString());
                portalInfo.AddFirst(XCom);

            }

            foreach (XElement keyItem in pathNodeKeys.Elements("Item")) {
                XElement childList = keyItem.Element("PathNodeChildList");

                foreach (Node nodeA in nodes) {
                    foreach (Node nodeB in nodes) {
                        if (nodeA != nodeB) {
                            for (int i = 1; i < 6; i++) {
                                if ((nodeA.key - nodeB.key) + i == int.Parse(keyItem.Element("Key").Attribute("value").Value, CultureInfo.InvariantCulture)) {
                                    XComment XCom = new XComment(nodeA.name + "[" + nodeA.index + "]" + " -> " + nodeB.name + "[" + nodeB.index + "]" + " +" + i);
                                    keyItem.Element("Key").AddAfterSelf(XCom);
                                }
                            }
                        }
                    }
                }

                foreach (XElement childItem in childList.Elements("Item")) {

                    string value =
                        " "
                        +
                        portalInfos.Elements("Item").ElementAt(int.Parse(childItem.Element("PortalInfoIdx").Attribute("value").Value, CultureInfo.InvariantCulture)).Element("RoomIdx").Attribute("value").Value.ToString()
                        +
                        "->"
                        +
                        portalInfos.Elements("Item").ElementAt(int.Parse(childItem.Element("PortalInfoIdx").Attribute("value").Value, CultureInfo.InvariantCulture)).Element("DestRoomIdx").Attribute("value").Value.ToString()
                        + " ";

                    XComment XComPortal = new XComment(value);
                    childItem.Element("PortalInfoIdx").AddAfterSelf(XComPortal);

                    foreach (Node nodeA in nodes) {
                        foreach (Node nodeB in nodes) {
                            if (nodeA != nodeB) {
                                for (int i = 1; i < 6; i++) {
                                    if ((nodeA.key - nodeB.key) + i == int.Parse(childItem.Element("PathNodeKey").Attribute("value").Value, CultureInfo.InvariantCulture)) {
                                        XComment XCom = new XComment(nodeA.name + "[" + nodeA.index + "]" + " -> " + nodeB.name + "[" + nodeB.index + "]" + " +" + i);

                                        childItem.Element("PathNodeKey").AddAfterSelf(XCom);

                                    }
                                }
                            }
                        }
                    }
                }
            }

            XDoc.Save("commentexample.xml");
        }
    }
}
