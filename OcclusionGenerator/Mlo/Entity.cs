using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcclusionGenerator {

    public enum EntityType {
        ENTITY_TYPE_DEFAULT,
        ENTITY_TYPE_DOOR,
        ENTITY_TYPE_GLASS,
    }

    public class Entity {
        public string name;
        public int hash;
        public EntityType entityType;
        public Entity(string name) {
            this.name = name;
            this.hash = (int)(JenkinsHash.jooat(name));
            entityType = EntityType.ENTITY_TYPE_DEFAULT;
        }
        public Entity(string name, EntityType type) {
            this.name = name;
            this.hash = (int)(JenkinsHash.jooat(name));
            entityType = type;
        }
    }
}
