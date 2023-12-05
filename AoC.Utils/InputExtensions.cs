namespace AoC.Utils;

public static class InputExtensions
{
    public static IEnumerable<string> Read(this string filePath) => File.ReadAllLines(filePath);

    public static string ReadLine(this string filePath) => File.ReadAllText(filePath);

    public static IEnumerable<int> ReadAsInt(this string filePath) => filePath.Read().Select(int.Parse);

    public static Dictionary<(int x, int y), char> ReadAsCoordinateDictionary(this string filePath) => filePath.ReadAsCoordinateDictionary(_ => true);

    public static Dictionary<(int x, int y), char> ReadAsCoordinateDictionary(this string filePath, Func<char, bool> predicate)
    {
        var input = filePath.Read();
        var result = new Dictionary<(int x, int y), char>();

        foreach (var (row, y) in input.Select((x, i) => (x, i)))
        {
            foreach (var (c, x) in row.Select((x, i) => (x, i)))
            {
                if (predicate(c))
                {
                    result.Add((x, y), c);
                }
            }
        }

        return result;
    }

    public static char[][] ReadAsGrid(this string filePath) => filePath.Read().Select(x => x.ToArray()).ToArray();

    public static T[][] ReadAsGrid<T>(this string filePath, Func<char, T> selector) => filePath.Read().Select(x => x.Select(selector).ToArray()).ToArray();
}