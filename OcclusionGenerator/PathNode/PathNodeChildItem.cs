using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OcclusionGenerator {
    public class PathNodeChildItem {
        public PathNodeItem pathNode;
        public PortalInfoItem portalInfo;

        public PathNodeChildItem(PathNodeItem pathNode, PortalInfoItem portalInfo) {
            this.pathNode = pathNode;
            this.portalInfo = portalInfo;
        }

        public XElement XML() {
            return new XElement("Item",
                new XElement("PathNodeKey",
                    new XAttribute("value", pathNode.key)
                ),
                new XElement("PortalInfoIdx",
                    new XAttribute("value", portalInfo.infoIdx)
                )
            );
        }
    }
}