using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OcclusionGenerator {
    public class PortalEntityItem {
        private Entity entity;
        private bool isDoor;
        private bool isGlass;
        private float maxOccl;

        public PortalEntityItem(Entity entity, bool isDoor, bool isGlass) {
            this.entity = entity;
            this.isDoor = isDoor;
            this.isGlass = isGlass;
        }
        public PortalEntityItem(Entity entity, bool isDoor, bool isGlass, float maxOccl) {
            this.entity = entity;
            this.isDoor = isDoor;
            this.isGlass = isGlass;
            this.maxOccl = maxOccl;
        }

        public XElement XML() {
            return new XElement("Item",
                new XElement("LinkType",
                    new XAttribute("value", "1")
                ),
                new XElement("MaxOcclusion",
                    new XAttribute("value", maxOccl)
                ),
                new XElement("EntityModelHashkey",
                    new XAttribute("value", entity.hash)
                ),
                new XElement("IsDoor",
                    new XAttribute("value", isDoor)
                ),
                new XElement("IsGlass",
                    new XAttribute("value", isGlass)
                )
            );
        }
    }
}