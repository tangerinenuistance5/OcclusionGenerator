using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OcclusionGenerator {
    public class PortalInfoItem {
        public int proxyHash;
        public uint portalIdx;
        public uint roomIdx;
        public uint destRoomIdx;
        public uint infoIdx;
        public List<PortalEntityItem> Items { get; }
        public PortalInfoItem(int proxyHash, uint portalIdx, uint roomIdx, uint destRoomIdx) {
            this.proxyHash = proxyHash;
            this.portalIdx = portalIdx;
            this.roomIdx = roomIdx;
            this.destRoomIdx = destRoomIdx;

            Items = new List<PortalEntityItem>();
        }

        public XElement XML() {
            XElement portalInfoItem = new XElement("Item",
                new XElement("InteriorProxyHash",
                    new XAttribute("value", proxyHash)
                ),
                new XElement("PortalIdx",
                    new XAttribute("value", portalIdx)
                ),
                new XElement("RoomIdx",
                    new XAttribute("value", roomIdx)
                ),
                new XElement("DestInteriorHash",
                    new XAttribute("value", proxyHash)
                ),
                new XElement("DestRoomIdx",
                    new XAttribute("value", destRoomIdx)
                ),
                new XElement("PortalEntityList",
                    new XAttribute("itemType", "naOcclusionPortalEntityMetadata")
                )
            );

            for (int i = 0; i < Items.Count; i++) {
                portalInfoItem.Element("PortalEntityList").Add(Items[i].XML());
            }

            return portalInfoItem;
        }
    }
}