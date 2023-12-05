using AoC.Utils;
using AoCHelper;
using System.Text.RegularExpressions;

namespace AoC2023;
public partial class Day04 : BaseDay
{
    public override ValueTask<string> Solve_1()
    {
        var cards = InputFilePath.Read().Select(x => new Card(x)).ToList();

        var sum = 0;

        foreach (var card in cards)
        {
            var cardPoints = 0;
            foreach (var winning in card.WinningNumbers)
            {
                if (card.DrawnNumbers.Contains(winning))
                {
                    cardPoints = cardPoints == 0 ? 1 : cardPoints * 2;
                }
            }
            sum += cardPoints;
        }

        return new(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var originalCards = InputFilePath.Read().Select(x => new Card(x)).ToList();

        var cards = Enumerable.Repeat(0, originalCards.Count).ToList();

        foreach (var item in originalCards.Select((card, i) => new { card, i }))
        {
            cards[item.i]++;

            var matches = item.card.WinningNumbers.Count(item.card.DrawnNumbers.Contains);

            for (var i = 1; i <= matches; i++)
            {
                cards[item.i + i] += cards[item.i];
            }
        }
        return new(cards.Sum().ToString());
    }

    private partial class Card
    {
        public HashSet<int> WinningNumbers { get; set; } = new HashSet<int>();
        public HashSet<int> DrawnNumbers { get; set; } = new HashSet<int>();

        public Card(string row)
        {
            var match = CardParser().Match(row);
            WinningNumbers = match.Groups[1].Value.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
            DrawnNumbers = match.Groups[2].Value.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
        }

        [GeneratedRegex("^.*: (.*) \\| (.*)$")]
        private static partial Regex CardParser();
    }
}

