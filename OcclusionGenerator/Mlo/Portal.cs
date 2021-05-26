using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcclusionGenerator {
    public class Portal {
        public uint roomFrom;
        public uint roomTo;
        public uint flags;
        public List<Entity> attachedEntities { get; }
        public uint portalIndex;

        public Portal(uint roomFrom, uint roomTo, uint flags, List<Entity> attachedEntities, uint portalIndex) {
            this.roomFrom = roomFrom;
            this.roomTo = roomTo;
            this.flags = flags;
            this.attachedEntities = attachedEntities;
            this.portalIndex = portalIndex;
        }
    }
}