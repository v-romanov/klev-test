using System.Globalization;
using System.Reflection.Metadata;
using System.Xml;

namespace Third;

public static class LogFormater
{
    static readonly int DateLength = 24;

    public static bool FormatLog(string input, out string output)
    {
        output = "";



        var dateSlice = input.Substring(0, DateLength);

        bool isDateFirstFormat = DateTime.TryParseExact(dateSlice, "dd.MM.yyyy HH:mm:ss.fff ", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime firstDate);
        bool isDateSecondFormat = DateTime.TryParseExact(dateSlice, "yyyy-MM-dd HH:mm:ss.ffff", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime secondDate);

        if (!isDateFirstFormat && !isDateSecondFormat)
        {
            return false;
        }

        if (isDateFirstFormat)
        {
            return FormatFirstLog(firstDate, input.Substring(24), out output);
        }
        else
        {
            return FormatSecondLog(secondDate, input.Substring(24), out output);
        }
    }

    private static string FinalOutput(DateTime time, string level, string method, string message)
    {
        return $"{time.ToString("dd-MM-yyyy\tHH:mm:ss.ffff")}\t{level}\t{method}\t{message}";
    }

    private static bool FormatFirstLog(DateTime date, string input, out string output)
    {
        output = "";
        var splitted = input.Split(" ");


        var level = splitted[0].Trim();

        switch (level)
        {
            case "INFORMATION": level = "INFO"; break;
            case "WARNING": level = "WARN"; break;
            case "ERROR": break;
            case "DEBUG": break;

            default: return false;
        }

        var message = string.Join(' ', splitted[1..]).Trim();
        var method = "DEFAULT";

        output = FinalOutput(date, level, method, message);
        return true;
    }

    private static bool FormatSecondLog(DateTime date, string input, out string output)
    {
        output = "";
        var splitted = input.Split("|");

        // Because first is empty
        var level = splitted[1].Trim();

        switch (level)
        {
            case "INFO": break;
            case "WARN": break;
            case "ERROR": break;
            case "DEBUG": break;

            default: return false;
        }

        var method = splitted[2].Trim();
        var message = string.Join('|', splitted[3..]).Trim();

        output = FinalOutput(date, level, method, message);
        return true;
    }
}


public static class Ulala
{
    public static void Main()
    {
        var lines = File.ReadAllLines("./txts/input.txt");

        using var output = new StreamWriter("./txts/output.txt");
        using var problems = new StreamWriter("./txts/problems.txt");

        foreach (var line in lines)
        {
            if (LogFormater.FormatLog(line, out string formatted))
            {
                output.WriteLine(formatted);
            }
            else
            {
                problems.WriteLine(line);
            }
        }
    }
}