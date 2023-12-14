using AoC.Core;

namespace AoC._2022;

// https://adventofcode.com/2022/day/7
public sealed class Day7 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var tree = GetTree();

        return tree.GetDirectories(size: 100_000)
            .Sum(x => x.TotalSize)
            .ToString();
    }

    public string SolvePart2()
    {
        const int maxSize = 70_000_000;
        const int requiredSize = 30_000_000;

        var tree = GetTree();

        var unusedSpace = maxSize - tree.TotalSize;
        var requiredSpace = requiredSize - unusedSpace;

        return tree.GetDirectories(size: requiredSpace, lessThan: false)
            .OrderBy(x => x.TotalSize)
            .First()
            .TotalSize
            .ToString();
    }

    private Node GetTree()
    {
        var root = new Node("/", null, null);
        var node = root;

        foreach (var line in GetLineInput(nameof(Day7)))
        {
            var parts = line.Split(' ');

            switch (parts[0])
            {
                case "$":
                    if (parts[1] == "cd")
                        node = node.GetNode(parts[2]);
                    break;
                case "dir":
                    node.AddChild(new Node(parts[1], node, null));
                    break;
                default:
                    node.AddChild(new Node(parts[1], node, int.Parse(parts[0])));
                    break;
            }
        }

        return root;
    }

    private sealed class Node
    {
        internal Node(string name, Node? parent, int? fileSize)
        {
            Name = name;
            Parent = parent;
            Children = new List<Node>();
            FileSize = fileSize;
        }

        private string Name { get; }
        private List<Node> Children { get; }
        private Node? Parent { get; }
        private int? FileSize { get; }
        internal int TotalSize => FileSize ?? Children.Sum(c => c.TotalSize);

        internal Node GetNode(string path)
        {
            return path switch
            {
                "/" => this,
                ".." => Parent ?? this,
                _ => Children.Single(x => x.Name == path)
            };
        }

        internal void AddChild(Node child)
        {
            Children.Add(child);
        }

        internal IEnumerable<Node> GetDirectories(int size, bool lessThan = true)
        {
            return GetNodes()
                .Where(x => x.Children.Count > 0)
                .Where(x => lessThan ? x.TotalSize <= size : x.TotalSize >= size);
        }

        private IEnumerable<Node> GetNodes()
        {
            yield return this;

            foreach (var node in Children.SelectMany(child => child.GetNodes()))
            {
                yield return node;
            }
        }
    }
}
