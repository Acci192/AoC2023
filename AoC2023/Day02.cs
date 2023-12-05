using AoC.Utils;
using AoCHelper;

namespace AoC2023;

internal class Day02 : BaseDay
{
    public override ValueTask<string> Solve_1()
    {
        var input = InputFilePath.Read();
        var games = input.Select(x => new Game(x)).ToList();

        var sum = 0;
        for (var i = 0; i < games.Count; i++)
        {
            var game = games[i];
            if (!game.Sets.Any(x => x.Red > 12 || x.Blue > 14 || x.Green > 13))
            {
                sum += (i + 1);
            }

        }
        return new(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var input = InputFilePath.Read();

        var games = input.Select(x => new Game(x)).ToList();

        var sum = 0;
        for (var i = 0; i < games.Count; i++)
        {
            var game = games[i];
            var r = game.Sets.Max(x => x.Red);
            var g = game.Sets.Max(x => x.Green);
            var b = game.Sets.Max(x => x.Blue);

            sum += (r * g * b);
        }

        return new(sum.ToString());
    }

    private class Game
    {
        public List<Set> Sets = new();

        public Game(string s)
        {
            var sp = s.Split(": ");

            var sets = sp[1].Split("; ");

            foreach (var set in sets)
            {
                Sets.Add(new Set(set));
            }
        }
    }

    private class Set
    {
        public int Red;
        public int Green;
        public int Blue;

        public Set(string s)
        {
            var sp = s.Split(", ");
            foreach (var s2 in sp)
            {
                var sp1 = s2.Split(' ');
                if (sp1[1].First() == 'b')
                {
                    Blue += int.Parse(sp1[0]);
                }
                else if (sp1[1].First() == 'r')
                {
                    Red += int.Parse(sp1[0]);
                }
                else if (sp1[1].First() == 'g')
                {
                    Green += int.Parse(sp1[0]);
                }
            }
        }
    }
}
