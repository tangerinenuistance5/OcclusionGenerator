using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OcclusionGenerator {
    public class PathNodeList {
        public List<PathNodeItem> Items { get; set; }

        public PathNodeList() {
            Items = new List<PathNodeItem>();
        }

        public static List<PathNodeItem> OrderPathNodeList(List<PathNodeItem> Items) {
            List<PathNodeItem> ItemsPositive = Items.Where(item => item.key >= 0).OrderBy(item => item.key).ToList();
            List<PathNodeItem> ItemsNegative = Items.Where(item => item.key < 0).OrderBy(item => item.key).ToList();

            ItemsPositive.AddRange(ItemsNegative);

            Items = ItemsPositive.OrderBy(item => item.pathType).ToList();

            return Items;
        }

        public XElement XML() {
            XElement pathNodeList = new XElement("PathNodeList",
                new XAttribute("itemType", "naOcclusionPathNodeMetadata")
            );

            Items = OrderPathNodeList(Items);

            for (int i = 0; i < Items.Count; i++) {
                pathNodeList.Add(Items[i].XML());
            }

            return pathNodeList;
        }
    }
}