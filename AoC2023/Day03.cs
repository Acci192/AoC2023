using AoC.Utils;
using AoCHelper;
using System.Text;

namespace AoC2023;
public class Day03 : BaseDay
{
    public override ValueTask<string> Solve_1()
    {
        var input = InputFilePath.ReadAsGrid();

        var sum = 0;
        var visited = new HashSet<(int x, int y)>();

        foreach (var (row, y) in input.Select((x, i) => (x, i)))
        {
            foreach (var (c, x) in row.Select((x, i) => (x, i)))
            {
                if (input[y][x] == '.' || char.IsDigit(input[y][x]))
                {
                    continue;
                }

                var parts = GetAdjacentParts(x, y, input, visited);

                foreach (var part in parts)
                {
                    sum += part;
                }
            }
        }
        return new(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var input = InputFilePath.ReadAsGrid();

        var sum = 0;
        var visited = new HashSet<(int x, int y)>();

        foreach (var (row, y) in input.Select((x, i) => (x, i)))
        {
            foreach (var (c, x) in row.Select((x, i) => (x, i)))
            {
                if (input[y][x] != '*')
                {
                    continue;
                }

                var parts = GetAdjacentParts(x, y, input, visited);

                if (parts.Count != 2)
                {
                    continue;
                }

                sum += parts[0] * parts[1];
            }
        }
        return new(sum.ToString());
    }

    private static List<int> GetAdjacentParts(int x, int y, char[][] grid, HashSet<(int x, int y)> visited)
    {
        var result = new List<int>();
        for (var xd = -1; xd <= 1; xd++)
        {
            for (var yd = -1; yd <= 1; yd++)
            {
                var tempX = x + xd;
                var tempY = y + yd;
                if (xd == 0 && yd == 0
                    || tempX < 0 || tempX >= grid.Length
                    || tempY < 0 || tempY >= grid.Length)
                {
                    continue;
                }

                var c = grid[tempY][tempX];
                if (char.IsDigit(c))
                {
                    var part = ExtractPart(tempX, tempY, grid, visited);
                    if (part != 0)
                    {
                        result.Add(part);
                    }
                }
            }
        }

        return result;
    }

    private static int ExtractPart(int x, int y, char[][] grid, HashSet<(int x, int y)> visited)
    {
        var i = x;
        var row = grid[y];
        while (i > 0)
        {
            if (!char.IsDigit(row[i - 1]))
            {
                break;
            }
            i--;
        }

        var sb = new StringBuilder();

        while (i < row.Length)
        {
            if (char.IsDigit(row[i]))
            {
                if (!visited.Add((i, y)))
                {
                    return 0;
                }
                sb.Append(row[i]);
                i++;
            }
            else
            {
                break;
            }
        }

        return int.Parse(sb.ToString());
    }
}
