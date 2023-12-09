using AoC.Utils;
using AoCHelper;
using System.Text.RegularExpressions;

namespace AoC2023;
public partial class Day08 : BaseDay
{
    public override ValueTask<string> Solve_1()
    {
        var input = InputFilePath.Read().ToList();


        var map = new Dictionary<string, (string left, string right)>();

        foreach (var s in input.Skip(2))
        {
            var match = InputRegex().Match(s);
            map[match.Groups[1].Value] = (match.Groups[2].Value, match.Groups[3].Value);
        }

        var directions = input[0].Select(c => c).ToList();

        var position = "AAA";

        var steps = 0;

        while (position != "ZZZ")
        {
            var direction = directions[steps % directions.Count];
            if (direction == 'L')
            {
                position = map[position].left;
            }
            else
            {
                position = map[position].right;

            }
            steps++;
        }

        return new(steps.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var input = InputFilePath.Read().ToList();

        var map = new Dictionary<string, (string left, string right)>();

        foreach (var s in input.Skip(2))
        {
            var match = InputRegex().Match(s);
            map[match.Groups[1].Value] = (match.Groups[2].Value, match.Groups[3].Value);
        }

        var directions = input[0].Select(c => c).ToList();

        var positions = map.Keys.Where(x => x.Last() == 'A').ToList();

        var steps = 0;
        var done = new Dictionary<int, int>();

        while (positions.Count(x => x.Last() == 'Z') != positions.Count && done.Count != positions.Count)
        {
            var tempPosition = positions.ToList();
            foreach (var position in positions.Select((position, i) => (position, i)))
            {
                var direction = directions[steps % directions.Count];
                if (direction == 'L')
                {
                    tempPosition[position.i] = map[position.position].left;
                }
                else
                {
                    tempPosition[position.i] = map[position.position].right;

                }

                if (position.position.Last() == 'Z')
                {
                    done[position.i] = steps;
                }
            }
            positions = tempPosition;

            steps++;
        }

        return new(LCM(done.Values.Select(x => (long)x).ToArray()).ToString());
    }

    static long LCM(long[] numbers) => numbers.Aggregate(LCM);
    static long LCM(long a, long b) => Math.Abs(a * b) / GCD(a, b);
    static long GCD(long a, long b) => b == 0 ? a : GCD(b, a % b);

    [GeneratedRegex(@"(.*) = \((.*), (.*)\)")]
    private static partial Regex InputRegex();
}
