using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace OcclusionGenerator {
    public class naOcclusionInteriorMetadata {

        public static void SavePsoXML(string directory, uint fileName, PortalInfoList portalInfoList, PathNodeList pathNodeList) {
            XElement occlusionFile = new XElement("naOcclusionInteriorMetadata");
            occlusionFile.Add(portalInfoList.XML());
            occlusionFile.Add(pathNodeList.XML());
            occlusionFile.Save(Path.Combine(directory, fileName + ".ymt.pso.xml"));
        }

        public static PortalInfoList GetPortalInfoList(MloInterior mloInterior) {
            PortalInfoList portalInfoList = new PortalInfoList();

            foreach (Room room in mloInterior.Rooms) {
                uint portalIdx = 0;

                foreach (Portal portal in mloInterior.Portals) {
                    if (!Bit.IsBitSet(portal.flags, 1) && !Bit.IsBitSet(portal.flags, 2)) {
                        if (portal.roomFrom == room.roomIndex) {
                            PortalInfoItem portalInfoItem = new PortalInfoItem(
                               mloInterior.occlHash.proxyHash,
                               portalIdx,
                               portal.roomFrom,
                               portal.roomTo
                            );


                            foreach (Entity entity in portal.attachedEntities) {
                                PortalEntityItem portalEntityItem;
                                switch (entity.entityType) {
                                    case (EntityType.ENTITY_TYPE_DOOR):
                                        portalEntityItem = new PortalEntityItem(entity, true, false, 0.7f);
                                        break;
                                    case (EntityType.ENTITY_TYPE_GLASS):
                                        portalEntityItem = new PortalEntityItem(entity, false, true, 0.0f);
                                        break;
                                    default:
                                        portalEntityItem = new PortalEntityItem(entity, false, false, 0.0f);
                                        break;
                                }


                                portalInfoItem.Items.Add(portalEntityItem);
                            }

                            portalInfoList.Items.Add(portalInfoItem);

                            portalIdx++;
                        } else if (portal.roomTo == room.roomIndex) {
                            PortalInfoItem portalInfoItem = new PortalInfoItem(
                               mloInterior.occlHash.proxyHash,
                               portalIdx,
                               portal.roomTo,
                               portal.roomFrom
                            );

                            foreach (Entity entity in portal.attachedEntities) {
                                PortalEntityItem portalEntityItem;
                                switch (entity.entityType) {
                                    case (EntityType.ENTITY_TYPE_DOOR):
                                        portalEntityItem = new PortalEntityItem(entity, true, false, 0.7f);
                                        break;
                                    case (EntityType.ENTITY_TYPE_GLASS):
                                        portalEntityItem = new PortalEntityItem(entity, false, true, 0.0f);
                                        break;
                                    default:
                                        portalEntityItem = new PortalEntityItem(entity, false, false, 0.0f);
                                        break;
                                }

                                portalInfoItem.Items.Add(portalEntityItem);
                            }

                            portalInfoList.Items.Add(portalInfoItem);

                            portalIdx++;
                        }

                    }
                }
            }

            foreach (PortalInfoItem portal in portalInfoList.Items) {
                portal.infoIdx = (uint)portalInfoList.Items.IndexOf(portal);
            }

            return portalInfoList;
        }

        public static PathNodeList GetPathNodeList(PortalInfoList portalInfoList, MloInterior mloInterior) {
            PathNodeList pathNodeList = PathAlgorithm.GetPaths(portalInfoList, mloInterior);

            return pathNodeList;
        }
    }
}
