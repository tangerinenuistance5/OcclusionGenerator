using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OcclusionGenerator {
    public class Pair {
        public Node nodeA;
        public Node nodeB;
        public bool limboPair;
        public Pair(Node nodeA, Node nodeB) {
            this.nodeA = nodeA;
            this.nodeB = nodeB;
            limboPair = (nodeA.name == "limbo" || nodeB.name == "limbo") ? true : false;
        }


        public static List<Pair> GetPairs(List<Node> nodes) {
            List<Pair> pairs = new List<Pair>();
            List<Node> inactiveNodes = new List<Node>();

            foreach (Node nodeA in nodes) {
                foreach (Node nodeB in nodes) {

                    if (inactiveNodes.Any(x => x == nodeB)) {
                        continue;
                    }

                    if (nodeA != nodeB) {
                        pairs.Add(new Pair(nodeA, nodeB));
                        inactiveNodes.Add(nodeA);
                    }
                }
            }

            return pairs;
        }
    }
}
