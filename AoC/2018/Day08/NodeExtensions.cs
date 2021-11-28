using System.Linq;

namespace AoC._2018.Day08
{
    public static class NodeExtensions
    {
        public static int CalcValue(this Node node)
        {
            if (node.Header.ChildrenCount == 0)
            {
                return node.Metadata.Sum();
            }

            var value = 0;
            foreach (var index in node.Metadata)
            {
                if (node.Children.ElementAtOrDefault(index - 1) != null)
                {
                    value += CalcValue(node.Children[index - 1]);
                }
            }

            return value;
        }

        public static int SumMetadata(this Node node)
        {
            var sum = 0;
            foreach (var child in node.Children)
            {
                sum += SumMetadata(child);
            }

            sum += node.Metadata.Sum();

            return sum;
        }
    }
}
