#define TEST_FOR_NON_LATIN

using System.Security;
using System.Text.RegularExpressions;

namespace First;

public class LatinCompress
{

#if TEST_FOR_NON_LATIN
    private static Regex ContainOnlyLatin() => new Regex(@"^[a-zA-Z]+$");
#endif


    // Tried to check if input is latin only when TEST_FOR_NON_LATIN is defined
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

        for (var i = 1; i < input.Length; ++i)
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

    // Assumed that input is valid comporessed string
    public static string Decompress(string input)
    {
        string output = "";
        var current = input[0];
        string number = "";

        var flushToOutput = () =>
        {
            if (number == "")
            {
                output += current;
                return;
            }

            output += new string(current, int.Parse(number));
        };


        for (var i = 1; i < input.Length; ++i)
        {
            char character = input[i];

            if (char.IsNumber(character))
            {
                number += character;
            }
            else
            {
                flushToOutput();
                number = "";
                current = character;
            }
        }
        flushToOutput();

        return output;
    }
}
