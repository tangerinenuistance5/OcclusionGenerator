using System;
using System.Collections.Generic;
using System.Text;

namespace OcclusionGenerator {

    /* This Node is a Room
     * Every Room will have edges,
     * which are neighbor Rooms.
     */
    public class Node {
        public uint index;
        public string name;
        public int key;
        public List<PortalInfoItem> Portals { get; }
        public List<Node> Edges { get; }
        public Node(Room room, ProxyHash occlHash) {
            index = room.roomIndex;
            name = room.name;
            Portals = new List<PortalInfoItem>();
            Edges = new List<Node>();
            key = (room.name == "limbo") ? (int)(JenkinsHash.jooat("outside")) : (int)(occlHash.proxyHash ^ JenkinsHash.jooat(room.name));

        }

        public static List<Node> GetNodes(PortalInfoList portalInfoList, MloInterior mloInterior) {
            List<Node> nodes = new List<Node>();
            foreach (Room room in mloInterior.Rooms) {
                Node node = new Node(room, mloInterior.occlHash);

                foreach (PortalInfoItem portal in portalInfoList.Items) {
                    if (portal.roomIdx == node.index) {
                        node.Portals.Add(portal);
                    }
                }

                nodes.Add(node);
            }

            foreach (Node node in nodes) {
                foreach (Node neighbor in nodes) {
                    foreach (PortalInfoItem nodePortal in node.Portals) {
                        if (nodePortal.destRoomIdx == neighbor.index) {
                            if (!node.Edges.Contains(neighbor)) {
                                node.Edges.Add(neighbor);
                            }
                        }
                    }
                }
            }
            return nodes;
        }
    }
}

