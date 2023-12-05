using System.Text;

namespace AoC.Utils;
public static class OutputExtensions
{
    public static string GridString<T>(this T[][] grid)
    {
        var sb = new StringBuilder();
        for (var x = 0; x < grid.Length; x++)
        {
            for (var y = 0; y < grid[x].Length; y++)
            {
                sb.Append(grid[x][y]);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
    public static void Print<T>(this T[][] grid) => Console.WriteLine(grid.GridString());
}
