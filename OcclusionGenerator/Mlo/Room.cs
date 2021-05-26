using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcclusionGenerator {
    public class Room {
        public string name;
        public uint portalCount;
        public uint roomIndex;

        public Room(string name, uint portalCount, uint roomIndex) {
            this.name = name;
            this.portalCount = portalCount;
            this.roomIndex = roomIndex;
        }
    }
}

