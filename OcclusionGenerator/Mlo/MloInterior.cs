using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace OcclusionGenerator {
    public class MloInterior {
        public string name;
        public Vector3? position;
        public ProxyHash occlHash;
        public List<Room> Rooms { get; }
        public List<Portal> Portals { get; }

        public MloInterior(string name, Vector3? position, List<Room> Rooms, List<Portal> Portals) {
            this.name = name;
            this.position = position;
            this.occlHash = new ProxyHash(name, (Vector3)position);
            this.Rooms = Rooms;
            this.Portals = Portals;
        }
    }
}