using AoC.Core;

namespace AoC._2023;

public sealed class Day4 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var input = GetLineInput(nameof(Day4));
        var points = 0;

        foreach (var card in input)
        {
            var matches = GetMatches(card);

            if (matches == 0)
                continue;

            points += Enumerable.Range(0, matches).Skip(1).Aggregate(1, (agg, _) => agg * 2);
        }

        return points.ToString();
    }

    public string SolvePart2()
    {
        var input = GetLineInput(nameof(Day4));
        var scratchCards = 0;
        var currentCard = 0;
        var cardsLookup = new Dictionary<int, int>();

        foreach (var card in input)
        {
            currentCard++;
            scratchCards++;

            var matches = GetMatches(card);

            if (cardsLookup.TryGetValue(currentCard, out var copies))
                scratchCards += copies;

            if (matches == 0)
                continue;

            var range = Enumerable.Range(currentCard + 1, matches);

            foreach (var cardToAdd in range)
            {
                if (!cardsLookup.TryAdd(cardToAdd, copies + 1))
                    cardsLookup[cardToAdd] += copies + 1;
            }
        }

        return scratchCards.ToString();
    }

    private static int GetMatches(string card)
    {
        var numberSplit = card.Split(":")[1].Split("|");
        var winningNumbers = numberSplit[0].Trim().Replace("  ", " ").Split(" ").Select(int.Parse);
        var potentialNumbers = numberSplit[1].Trim().Replace("  ", " ").Split(" ").Select(x => int.Parse(x.Trim()));

        return winningNumbers.Count(x => potentialNumbers.Contains(x));
    }
}