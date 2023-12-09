using AoC.Utils;
using AoCHelper;
using System.Text.RegularExpressions;

namespace AoC2023;
public partial class Day06 : BaseDay
{
    public override ValueTask<string> Solve_1()
    {
        var input = InputFilePath.Read().ToList();

        var times = DigitRegex().Matches(input[0]).Select(x => x.Groups.Values.First().Value).Select(int.Parse).ToList();
        var distances = DigitRegex().Matches(input[1]).Select(x => x.Groups.Values.First().Value).Select(int.Parse).ToList();

        var races = times.Select((x, i) => (time: x, distance: distances[i]));

        var result = 1;
        foreach (var (time, distance) in races)
        {
            var wins = 0;
            for (var i = 0; i < time; i++)
            {
                if (i * (time - i) > distance)
                {
                    wins++;
                }
            }

            result *= wins;
        }

        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var input = InputFilePath.Read().ToList();

        var times = DigitRegex().Matches(input[0]).Select(x => x.Groups.Values.First().Value).ToList();
        var time = long.Parse(string.Join("", times));
        var distances = DigitRegex().Matches(input[1]).Select(x => x.Groups.Values.First().Value).ToList();
        var distance = long.Parse(string.Join("", distances));

        var races = times.Select((x, i) => (time: x, distance: distances[i]));

        var wins = 0;
        for (var i = 0; i < time; i++)
        {
            if (i * (time - i) > distance)
            {
                wins++;
            }
        }

        return new(wins.ToString());
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex DigitRegex();
}
