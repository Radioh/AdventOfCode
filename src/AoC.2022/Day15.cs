using System.Text.RegularExpressions;
using AoC.Core;

namespace AoC._2022;

public sealed class Day15 : PuzzleCore, IPuzzle
{
    private static readonly Regex InputRegex = new(
        @"\b(?:Sensor at x=(?<sensorX>-?\d+), y=(?<sensorY>-?\d+): closest beacon is at x=(?<beaconX>-?\d+), y=(?<beaconY>-?\d+))\b",
        RegexOptions.Compiled);

    public string SolvePart1()
    {
        var pairs = GetBeaconSensorPairs();
        return GetXPositions(pairs, y: 2_000_000).Count.ToString();
    }

    // public string SolvePart2()
    // {
    //     var print = 0;
    //
    //     const int maxY = 4_000_000;
    //     var xPositions = new HashSet<int>();
    //     var foundY = 0;
    //
    //     var pairs = GetBeaconSensorPairs();
    //
    //     for (var i = 0; i < maxY; i++)
    //     {
    //         xPositions = GetXPositions(pairs, y: i, yLimit: maxY);
    //
    //         print++;
    //
    //         if (print == 100)
    //         {
    //             Console.WriteLine("Y: " + i);
    //             print = 0;
    //         }
    //
    //         if (xPositions.Count > maxY)
    //             continue;
    //
    //         foundY = i;
    //         break;
    //     }
    //
    //     Console.WriteLine("FOUND Y: " + foundY);
    //
    //     var foundX = Enumerable.Range(xPositions.Min(), xPositions.Count).Except(xPositions).First();
    //
    //     Console.WriteLine("FOUND X: " + foundX);
    //
    //     return (foundX * maxY + foundY).ToString();
    // }

    private static HashSet<int> GetXPositions(List<InputPair> pairs, int y, int? yLimit = null)
    {
        var map = new HashSet<int>();

        foreach (var pair in pairs)
        {
            var positions = GetXPositionsOnY(pair, y, yLimit);

            foreach (var position in positions)
                map.Add(position);
        }

        if (yLimit != null)
            return map;

        foreach (var pair in pairs)
        {
            if (pair.Beacon.Y == y)
                map.Remove(pair.Beacon.X);

            if (pair.Sensor.Y == y)
                map.Remove(pair.Sensor.X);
        }

        return map;
    }

    private static List<int> GetXPositionsOnY(InputPair inputPair, int y, int? yLimit)
    {
        var (x1, y1) = inputPair.Sensor;
        var (x2, y2) = inputPair.Beacon;

        var manhattanDistance = Math.Abs(x1 - x2) + Math.Abs(y1 - y2);

        var yDiff = Math.Abs(y1 - y);

        if (yDiff > manhattanDistance)
            return new List<int>();

        var distDiff = Math.Abs(manhattanDistance - yDiff);
        var xStart = x1 - distDiff;
        var xEnd = x1 + distDiff;

        if (yLimit.HasValue)
        {
            if (xStart < 0)
                xStart = 0;

            if (xEnd > yLimit)
                xEnd = yLimit.Value;
        }

        var range = Enumerable.Range(xStart, xEnd - xStart + 1);

        return range.ToList();
    }

    private List<InputPair> GetBeaconSensorPairs()
    {
        var pairs = new List<InputPair>();

        foreach (var line in GetLineInput(nameof(Day15)))
        {
            var match = InputRegex.Match(line);

            var sensorX = int.Parse(match.Groups["sensorX"].Value);
            var sensorY = int.Parse(match.Groups["sensorY"].Value);
            var beaconX = int.Parse(match.Groups["beaconX"].Value);
            var beaconY = int.Parse(match.Groups["beaconY"].Value);

            pairs.Add(new InputPair(Beacon: (beaconX, beaconY), Sensor: (sensorX, sensorY)));
        }

        return pairs;
    }

    private sealed record InputPair ((int X, int Y) Beacon, (int X, int Y) Sensor);
}
