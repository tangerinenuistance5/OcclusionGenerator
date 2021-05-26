using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OcclusionGenerator {
    public class PathAlgorithm {
        public static PathNodeList GetPaths(PortalInfoList portalInfoList, MloInterior mloInterior) {
            List<Node> nodes = Node.GetNodes(portalInfoList, mloInterior);
            PathNodeList pathNodeList = new PathNodeList();

            GetPathsOfType(pathNodeList, nodes, PathType.Unk01, PathType.Unk00);
            GetPathsOfType(pathNodeList, nodes, PathType.Unk02, PathType.Unk01);
            GetPathsOfType(pathNodeList, nodes, PathType.Unk03, PathType.Unk02);
            GetPathsOfType(pathNodeList, nodes, PathType.Unk04, PathType.Unk03);
            GetPathsOfType(pathNodeList, nodes, PathType.Unk05, PathType.Unk04);

            return pathNodeList;
        }

        public static void GetPathsOfType(PathNodeList pathNodeList, List<Node> nodes, PathType typeA, PathType typeB) {
            if (typeA == PathType.Unk01) {
                foreach (Node node in nodes) {
                    foreach (Node edge in node.Edges) {
                        PathNodeItem pathNodeItem = new PathNodeItem(node, edge, typeA);

                        foreach (PortalInfoItem portalInfoItem in node.Portals) {
                            if (portalInfoItem.destRoomIdx == edge.index) {
                                PathNodeChildItem pathNodeChildItem = new PathNodeChildItem(new PathNodeItem(node, node, typeB), portalInfoItem);
                                pathNodeItem.Items.Add(pathNodeChildItem);
                            }
                        }

                        pathNodeList.Items.Add(pathNodeItem);
                    }
                }
            } else {
                List<Pair> pairs = Pair.GetPairs(nodes);

                foreach (Pair pair in pairs) {
                    GetRoutes(pathNodeList, pair.limboPair, pair.nodeA, pair.nodeB, typeA, typeB);
                    GetRoutes(pathNodeList, pair.limboPair, pair.nodeB, pair.nodeA, typeA, typeB);
                }
            }
        }

        static (bool found, PathNodeItem pathNodeItem) FindPathNodeInList(PathNodeList pathNodeList, Node nodeA, Node nodeB, PathType pathType) {
            PathNodeItem pathNodeItem = pathNodeList.Items.Find(item => (item.nodeA == nodeA) && (item.nodeB == nodeB) && (item.pathType == pathType));
            bool found;
            if (pathNodeItem != null) {
                found = true;
            } else {
                found = false;
            }
            return (found, pathNodeItem);
        }

        public static bool HasPathAlreadyBeenFound(PathNodeList pathNodeList, Node nodeA, Node nodeB) {
            PathNodeItem pathNodeItem = pathNodeList.Items.Find(item => (item.nodeA == nodeA) && (item.nodeB == nodeB) && (item.pathType == PathType.Unk03));
            bool found;
            if (pathNodeItem != null) {
                found = true;
            } else {
                found = false;
            }

            return found;
        }

        public static void GetRoutes(PathNodeList pathNodeList, bool limboPair, Node nodeA, Node nodeB, PathType typeA, PathType typeB) {
            IEnumerable<Node> edges = (limboPair) ? nodeA.Edges : nodeA.Edges.Where(i => i.name != "limbo");

            foreach (PathNodeItem pathNode in pathNodeList.Items.Where(i => i.pathType == typeB).ToList()) {
                switch (typeA) {
                    case PathType.Unk01:
                    case PathType.Unk02:
                    case PathType.Unk03:

                        if (pathNode.nodeA == nodeA && pathNode.nodeB == nodeB) {
                            (bool found, PathNodeItem existingPath) = FindPathNodeInList(pathNodeList, nodeA, nodeB, typeA);

                            if (found) {
                                PathNodeItem pathNodeItem = existingPath;

                                foreach (PortalInfoItem portalInfoItem in nodeA.Portals) {
                                    if (portalInfoItem.destRoomIdx == nodeB.index) {
                                        PathNodeChildItem pathNodeChildItem = new PathNodeChildItem(new PathNodeItem(nodeA, nodeA, typeB), portalInfoItem);
                                        pathNodeItem.Items.Add(pathNodeChildItem);
                                    }
                                }

                            } else {
                                PathNodeItem pathNodeItem = new PathNodeItem(nodeA, nodeB, typeA);

                                foreach (PortalInfoItem portalInfoItem in nodeA.Portals) {
                                    if (portalInfoItem.destRoomIdx == nodeB.index) {
                                        PathNodeChildItem pathNodeChildItem = new PathNodeChildItem(new PathNodeItem(nodeA, nodeA, typeB), portalInfoItem);
                                        pathNodeItem.Items.Add(pathNodeChildItem);
                                    }
                                }
                                pathNodeList.Items.Add(pathNodeItem);
                            }

                        } else {
                            foreach (Node edge in edges) {
                                if (pathNode.nodeA == edge && pathNode.nodeB == nodeB) {
                                    (bool found, PathNodeItem existingPath) = FindPathNodeInList(pathNodeList, nodeA, nodeB, typeA);

                                    if (found) {
                                        PathNodeItem pathNodeItem = existingPath;

                                        foreach (PortalInfoItem portalInfoItem in nodeA.Portals) {
                                            if (portalInfoItem.destRoomIdx == edge.index) {
                                                PathNodeChildItem pathNodeChildItem = new PathNodeChildItem(new PathNodeItem(edge, nodeB, typeB), portalInfoItem);
                                                pathNodeItem.Items.Add(pathNodeChildItem);
                                            }
                                        }
                                    } else {
                                        PathNodeItem pathNodeItem = new PathNodeItem(nodeA, nodeB, typeA);
                                        foreach (PortalInfoItem portalInfoItem in nodeA.Portals) {
                                            if (portalInfoItem.destRoomIdx == edge.index) {
                                                PathNodeChildItem pathNodeChildItem = new PathNodeChildItem(new PathNodeItem(edge, nodeB, typeB), portalInfoItem);
                                                pathNodeItem.Items.Add(pathNodeChildItem);
                                            }
                                        }
                                        pathNodeList.Items.Add(pathNodeItem);
                                    }
                                }
                            }
                        }
                        break;
                    case PathType.Unk04:
                    case PathType.Unk05:
                        foreach (Node edge in edges) {
                            if (pathNode.nodeA == edge && pathNode.nodeB == nodeB) {
                                (bool found, PathNodeItem existingPath) = FindPathNodeInList(pathNodeList, nodeA, nodeB, typeA);
                                bool hasPathBeenFound = HasPathAlreadyBeenFound(pathNodeList, nodeA, nodeB);
                                if (hasPathBeenFound == false) {
                                    if (found) {
                                        PathNodeItem pathNodeItem = existingPath;

                                        foreach (PortalInfoItem portalInfoItem in nodeA.Portals) {
                                            if (portalInfoItem.destRoomIdx == edge.index) {
                                                PathNodeChildItem pathNodeChildItem = new PathNodeChildItem(new PathNodeItem(edge, nodeB, typeB), portalInfoItem);
                                                pathNodeItem.Items.Add(pathNodeChildItem);
                                            }
                                        }
                                    } else {
                                        PathNodeItem pathNodeItem = new PathNodeItem(nodeA, nodeB, typeA);
                                        foreach (PortalInfoItem portalInfoItem in nodeA.Portals) {
                                            if (portalInfoItem.destRoomIdx == edge.index) {
                                                PathNodeChildItem pathNodeChildItem = new PathNodeChildItem(new PathNodeItem(edge, nodeB, typeB), portalInfoItem);
                                                pathNodeItem.Items.Add(pathNodeChildItem);
                                            }
                                        }
                                        pathNodeList.Items.Add(pathNodeItem);
                                    }
                                }
                            }
                        }
                        break;
                }
            }
        }
    }
}