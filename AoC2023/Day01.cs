using AoC.Utils;
using AoCHelper;

namespace AoC2023;

internal class Day01 : BaseDay
{
    public override ValueTask<string> Solve_1()
    {
        var input = InputFilePath.Read();

        var sum = 0;

        foreach (var s in input)
        {
            var x = s.First(c => c >= 48 && c <= 57);
            var y = s.Last(c => c >= 48 && c <= 57);

            var z = $"{x}{y}";
            sum += int.Parse(z);
        }
        return new(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var input = InputFilePath.Read();

        var sum = 0;

        foreach (var s in input)
        {
            var st = NameToInt(s);
            var x = st.First(c => c >= 48 && c <= 57);
            var y = st.Last(c => c >= 48 && c <= 57);

            var z = $"{x}{y}";
            sum += int.Parse(z);
        }
        return new(sum.ToString());
    }

    private string NameToInt(string s)
    {
        s = s.Replace("zero", "ze0ro");
        s = s.Replace("one", "o1ne");
        s = s.Replace("two", "tw2o");
        s = s.Replace("three", "th3ree");
        s = s.Replace("four", "fo4ur");
        s = s.Replace("five", "fi5ve");
        s = s.Replace("six", "si6x");
        s = s.Replace("seven", "se7ven");
        s = s.Replace("eight", "eig8ht");
        s = s.Replace("nine", "ni9ne");
        return s;
    }
}
