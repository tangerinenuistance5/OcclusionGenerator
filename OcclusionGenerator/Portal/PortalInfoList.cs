using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OcclusionGenerator {
    public class PortalInfoList {
        public List<PortalInfoItem> Items { get; }

        public PortalInfoList() {
            Items = new List<PortalInfoItem>();
        }

        public XElement XML() {
            XElement portalInfoList = new XElement("PortalInfoList",
                new XAttribute("itemType", "naOcclusionPortalInfoMetadata")
            );

            for (int i = 0; i < Items.Count; i++) {
                portalInfoList.Add(Items[i].XML());
            }

            return portalInfoList;
        }
    }
}