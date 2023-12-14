using System.Text.Json;
using AoC.Core;

namespace AoC._2022;

// https://adventofcode.com/2022/day/13
public sealed class Day13 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var rightOrder = GetPackets()
            .Chunk(2)
            .Select((x, i) => (left: x[0], right: x[1], position: i + 1))
            .Where((x) => Compare(x.left, x.right) <= 0)
            .Select(x => x.position)
            .Sum();

        return rightOrder.ToString();
    }

    public string SolvePart2()
    {
        var dividers = new[]
        {
            JsonSerializer.Deserialize<JsonElement>("[[2]]"),
            JsonSerializer.Deserialize<JsonElement>("[[6]]"),
        };

        var packets = GetPackets().Concat(dividers).ToList();

        packets.Sort(Compare);

        var decoderKey = packets
            .Select((x, i) => (packet: x, position: i + 1))
            .Where(x => dividers.Contains(x.packet))
            .Select(x => x.position)
            .Aggregate((x, y) => x * y);

        return decoderKey.ToString();
    }

    private static int Compare(JsonElement left, JsonElement right)
    {
        return left.ValueKind switch
        {
            JsonValueKind.Number when right.ValueKind == JsonValueKind.Number => left.GetInt32().CompareTo(right.GetInt32()),
            JsonValueKind.Array when right.ValueKind == JsonValueKind.Array => CompareTo(left, right),
            JsonValueKind.Array when right.ValueKind == JsonValueKind.Number => Compare(left, CreateArray(right)),
            _ => Compare(CreateArray(left), right)
        };
    }

    private static JsonElement CreateArray(JsonElement element)
    {
        return JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(new[] {element.GetInt32()}));
    }

    private static int CompareTo(JsonElement left, JsonElement right)
    {
        var leftArray = left.EnumerateArray().ToArray();
        var rightArray = right.EnumerateArray().ToArray();

        var i = 0;
        while (i < leftArray.Length && i < rightArray.Length)
        {
            var compare = Compare(leftArray[i], rightArray[i]);

            if (compare != 0)
                return compare;

            ++i;
        }

        return leftArray.Length.CompareTo(rightArray.Length);
    }

    private List<JsonElement> GetPackets()
    {
        var packets = new List<JsonElement>();

        foreach (var line in GetLineInput(nameof(Day13)))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            packets.Add(JsonSerializer.Deserialize<JsonElement>(line));
        }

        return packets;
    }
}
