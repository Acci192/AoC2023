using AoC.Utils;
using AoCHelper;
using System.Text.RegularExpressions;

namespace AoC2023;
public partial class Day05 : BaseDay
{
    public override ValueTask<string> Solve_1()
    {
        var input = InputFilePath.Read();
        var seeds = SeedRegex().Match(input.First()).Groups[1].Value.Split(" ").Select(long.Parse).ToList();

        var stages = ExtractStages(input.Skip(2)).ToList();


        foreach (var stage in stages)
        {
            for (var i = 0; i < seeds.Count; i++)
            {
                seeds[i] = stage.Transform(seeds[i]);
            }
        }
        return new(seeds.Min().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var input = InputFilePath.Read();
        var parsed = SeedRegex().Match(input.First()).Groups[1].Value.Split(" ").Select(long.Parse).ToList();

        var bags = new List<(long start, long end)>();
        for (var i = 0; i + 1 < parsed.Count; i += 2)
        {
            bags.Add((parsed[i], parsed[i] + parsed[i + 1] - 1));
        }

        var stages = ExtractStages(input.Skip(2)).ToList();

        foreach (var stage in stages)
        {
            var newBags = new List<(long start, long end)>();
            for (var i = 0; i < bags.Count; i++)
            {
                newBags.AddRange(stage.Transform(bags[i]));
            }

            bags = newBags;
        }

        return new(bags.Min(x => x.start).ToString());
    }

    private IEnumerable<Stage> ExtractStages(IEnumerable<string> input)
    {
        var stage = new Stage();

        foreach (var row in input)
        {
            if (string.IsNullOrWhiteSpace(row))
            {
                yield return stage;
                stage = new Stage();
                continue;
            }

            if (!char.IsDigit(row[0]))
            {
                continue;
            }

            var map = row.Split(" ").Select(long.Parse).ToList();
            stage.Mappings.Add((map[1], map[1] + map[2], map[0]));
        }

        yield return stage;
    }

    private class Stage
    {
        public List<(long start, long end, long destination)> Mappings = [];
        public List<(long start, long end)> SeedBags = [];

        public long Transform(long seed)
        {
            foreach (var (start, end, destination) in Mappings)
            {
                if (seed >= start && seed <= end)
                {
                    return seed + (destination - start);
                }
            }

            return seed;
        }

        public List<(long start, long end)> Transform((long start, long end) seedBag)
        {
            var result = new List<(long start, long end)>();

            var bags = new List<(long start, long end)> { seedBag };

            while (bags.Count != 0)
            {
                var handled = false;
                var bag = bags.First();

                bags.Remove(bag);

                foreach (var (start, end, destination) in Mappings)
                {
                    if (bag.start >= start && bag.start < end)
                    {
                        var bagStart = bag.start + (destination - start);
                        var bagEnd = bag.end + (destination - start);

                        if (bag.end < end)
                        {
                            result.Add((bagStart, bagEnd));

                            handled = true;
                            break;
                        }

                        bagEnd = end + (destination - start);
                        result.Add((bagStart, bagEnd));

                        bags.Add((end, bag.end));
                        handled = true;
                        break;
                    }

                    if (bag.start < start && bag.end >= start)
                    {
                        bags.Add((bag.start, start - 1));

                        if (bag.end < end)
                        {
                            result.Add((destination, bag.end + (destination - start)));
                            handled = true;
                            break;
                        }

                        result.Add((destination, end + (destination - start)));

                        bags.Add((end, bag.end));
                        handled = true;
                        break;
                    }
                }

                if (!handled)
                {
                    result.Add(bag);
                }
            }

            return result;
        }
    }

    [GeneratedRegex(".*: (.*)")]
    private static partial Regex SeedRegex();
}
