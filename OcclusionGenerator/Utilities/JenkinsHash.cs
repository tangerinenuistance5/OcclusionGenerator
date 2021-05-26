using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace OcclusionGenerator {
    public class JenkinsHash {
        public string text { get; set; }
        public int hashInt { get; set; }
        public uint hashUint { get; set; }

        public JenkinsHash(string text) {
            this.text = text;
            this.hashUint = jooat(text);
            this.hashInt = (int)hashUint;
        }

        public static uint jooat(string text) {
            uint h = 0;
            for (int i = 0; i < text.Length; i++) {
                h += (byte)text.ToLower()[i];
                h += (h << 10);
                h ^= (h >> 6);
            }
            h += (h << 3);
            h ^= (h >> 11);
            h += (h << 15);

            return h;
        }

    }
}
