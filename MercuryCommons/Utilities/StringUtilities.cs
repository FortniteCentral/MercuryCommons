using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MercuryCommons.Utilities;

public static class StringUtilities
{
    public static string RemoveHtmlTags(string s)
    {
        var match = new Regex("<.*?>").Match(s);
        while (match.Success)
        {
            s = s.Replace(match.Value, string.Empty);
            match = match.NextMatch();
        }

        return s;
    }

    private static IEnumerable<string> SplitLines(string stringToSplit, int maximumLineLength)
    {
        var words = stringToSplit.Split(' ');
        var line = words.First();
        foreach (var word in words.Skip(1))
        {
            var test = $"{line} {word}";
            if (test.Length > maximumLineLength)
            {
                yield return line;
                line = word;
            }
            else
            {
                line = test;
            }
        }

        yield return line;
    }
}