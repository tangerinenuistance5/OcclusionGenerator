using System;
using System.Collections.Generic;
using System.Text;

namespace OcclusionGenerator {
    public class Bit {
        public static bool IsBitSet(uint value, int bit) {
            return (((value >> bit) & 1) > 0);
        }
    }
}
