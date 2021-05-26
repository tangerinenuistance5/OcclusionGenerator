using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace OcclusionGenerator {
    public class ProxyHash {
        public int proxyHash { get; set; }
        public uint occlusionHash { get; set; }

        public ProxyHash(string text, Vector3 position) {
            this.proxyHash = GenerateProxyHash(text, position);
            this.occlusionHash = (uint)proxyHash;
        }
        public int GenerateProxyHash(string text, Vector3 position) {
            JenkinsHash h = new JenkinsHash(text);
            proxyHash = (int)(h.hashInt) ^ (int)(position.X * 100) ^ (int)(position.Y * 100) ^ (int)(position.Z * 100);

            return proxyHash;
        }
    }
}
