using AoC.Utils;
using AoCHelper;

namespace AoC2023;
public class Day07 : BaseDay
{
    public override ValueTask<string> Solve_1()
    {
        var input = InputFilePath.Read();

        var players = input.Select(s => s.Split(' ')).Select(x => (hand: x[0], bet: int.Parse(x[1])));

        var handOrder = new List<(int score, int bet, string hand)>();

        foreach (var (hand, bet) in players)
        {
            handOrder.Add((CalculateScore(hand), bet, hand));
        }

        var handComparer = new HandComparer(Cards1);
        handOrder = [.. handOrder.OrderBy(x => x.score).ThenBy(x => x.hand, handComparer)];

        var sum = 0;
        foreach (var bet in handOrder.Select((x, i) => (x.bet, i)))
        {
            sum += bet.bet * (bet.i + 1);
        }
        return new(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var input = InputFilePath.Read();

        var handOrder = input.Select(s => s.Split(' ')).Select(x => (score: CalculateScore(x[0], true), hand: x[0], bet: int.Parse(x[1])));

        var handComparer = new HandComparer(Cards2, true);
        handOrder = [.. handOrder.OrderBy(x => x.score).ThenBy(x => x.hand, handComparer)];

        var sum = 0;
        foreach (var bet in handOrder.Select((x, i) => (x.bet, i)))
        {
            sum += bet.bet * (bet.i + 1);
        }
        return new(sum.ToString());
    }

    public static int CalculateScore(string hand, bool part2 = false)
    {
        var table = Cards1.ToDictionary(x => x, x => 0);

        foreach (var card in hand)
        {
            if (card == 'J' && part2)
            {
                foreach (var c in Cards1)
                {
                    if (c != 'J')
                    {
                        table[c]++;
                    }
                }
            }
            table[card]++;
        }

        if (table.Values.Any(x => x == 5))
        {
            return _fiveOfAKind;
        }
        else if (table.Values.Any(x => x == 4))
        {
            return _fourOfAKind;
        }
        else if (
            (table.Values.Count(x => x == 3) == 1 && table.Values.Count(x => x == 2) == 1
            || (table.Values.Count(x => x == 3) == 2)))
        {
            return _fullHouse;
        }
        else if (table.Values.Any(x => x == 3))
        {
            return _threeOfAKind;
        }
        else if (table.Values.Where(x => x == 2).Count() == 2)
        {
            return _twoPair;
        }
        else if (table.Values.Any(x => x == 2))
        {
            return _pair;
        }

        return _highCard;
    }

    private const int _fiveOfAKind = 10;
    private const int _fourOfAKind = 9;
    private const int _fullHouse = 8;
    private const int _threeOfAKind = 7;
    private const int _twoPair = 6;
    private const int _pair = 5;
    private const int _highCard = 4;

    public static readonly List<char> Cards1 = ['2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'];
    public static readonly List<char> Cards2 = ['J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A'];
}

public class HandComparer(List<char> cards, bool part2 = false) : IComparer<string>
{
    private readonly List<char> _cards = cards;
    private readonly bool _part2 = part2;

    public int Compare(string? x, string? y)
    {
        for (var i = 0; i < x!.Length; i++)
        {
            var xc = _cards.IndexOf(x[i]);
            var yc = _cards.IndexOf(y![i]);

            if (xc > yc)
            {
                return 1;
            }
            else if (xc < yc)
            {
                return -1;
            }
        }

        if (_part2 == true)
        {
            var xj = x.Count(x => x == 'J');
            var yj = y!.Count(x => x == 'J');

            if (xj > yj)
            {
                return -1;
            }
            else if (xj < yj)
            {
                return 1;
            }
        }
        return 0;
    }
}
