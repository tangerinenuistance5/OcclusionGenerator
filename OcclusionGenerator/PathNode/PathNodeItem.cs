using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;



namespace OcclusionGenerator {

    public class PathNodeItem {
        public int key;
        public PathType pathType;
        public Node nodeA { get; set; }
        public Node nodeB { get; set; }
        public List<PathNodeChildItem> Items { get; }
        public PathNodeItem(Node nodeA, Node nodeB, PathType pathType) {
            key = (nodeA == nodeB) ? 0 : (nodeA.key - nodeB.key) + (int)pathType;
            this.pathType = pathType;
            this.nodeA = nodeA;
            this.nodeB = nodeB;
            Items = new List<PathNodeChildItem>();
        }

        public XElement XML() {
            XElement pathNodeItem = new XElement("Item",
                new XElement("Key",
                    new XAttribute("value", key)
                ),
                new XElement("PathNodeChildList")
            );

            for (int i = 0; i < Items.Count; i++) {
                pathNodeItem.Element("PathNodeChildList").Add(Items[i].XML());
            }

            XElement[] orderedChildList = pathNodeItem.Element("PathNodeChildList").Elements("Item").OrderBy(item => (uint)item.Element("PortalInfoIdx").Attribute("value")).ToArray();

            pathNodeItem.Element("PathNodeChildList").RemoveAll();

            for (int i = 0; i < orderedChildList.Length; i++) {
                pathNodeItem.Element("PathNodeChildList").Add(orderedChildList[i]);
            }

            pathNodeItem.Element("PathNodeChildList").Add(new XAttribute("itemType", "naOcclusionPathNodeChildMetadata"));

            return pathNodeItem;
        }
    }
}