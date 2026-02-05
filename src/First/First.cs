#define TEST_FOR_NON_LATIN

using System.Security;
using System.Text.RegularExpressions;

namespace First;

public class LatinCompress
{

#if TEST_FOR_NON_LATIN
    private static Regex ContainOnlyLatin() => new Regex(@"^[a-zA-Z]+$");
#endif

    public static string Comporess(string input)
    {
#if TEST_FOR_NON_LATIN

        if (!ContainOnlyLatin().IsMatch(input))
        {
            throw new ArgumentException("Input must be only latin characters");
        }

#endif

        if (input == "" || input.Length == 1)
        {
            return input;
        }

        string output = "";
        int count = 1;
        char current = input[0];

        var flushToOutput = () =>
        {
            string newPart = current.ToString();

            if (count > 1)
            {
                newPart += count.ToString();
            }

            output += newPart;
        };

        for (var i = 0; i < input.Length; ++i)
        {
            if (input[i] == current)
            {
                count += 1;
            }
            else
            {
                flushToOutput();
                current = input[i];
                count = 1;
            }
        }

        flushToOutput();

        return output;
    }
}
