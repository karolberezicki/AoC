using System.Collections.Generic;
using System.Linq;

namespace AoC._2018.Day08
{
    public class Node
    {
        public (int ChildrenCount, int MetadataCount) Header { get; private init; }
        public List<Node> Children { get; private init; }
        public List<int> Metadata { get; private set; }

        public static (Node Node, int currentIndex) CreateNode(List<int> license, int currentIndex)
        {
            var node = new Node
            {
                Header = (license[currentIndex], license[currentIndex + 1]),
                Children = new List<Node>(),
                Metadata = new List<int>()
            };

            for (int i = 0; i < node.Header.ChildrenCount; i++)
            {
                var child = CreateNode(license, currentIndex + 2);
                node.Children.Add(child.Node);
                currentIndex = child.currentIndex;
            }

            node.Metadata = license.Skip(currentIndex + 2).Take(node.Header.MetadataCount).ToList();
            currentIndex += node.Header.MetadataCount;

            return (node, currentIndex);
        }
    }
}
