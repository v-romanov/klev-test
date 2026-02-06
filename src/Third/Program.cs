using System.Globalization;
using System.Reflection.Metadata;

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
        var message = string.Join(' ', splitted[3..]).Trim();

        output = FinalOutput(date, level, method, message);
        return true;
    }
}


public static class Ulala
{
    public static void Main()
    {
        Console.WriteLine(LogFormater.FormatLog("2025-03-10 15:14:51.5882| INFO|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'", out string output));
        Console.WriteLine(LogFormater.FormatLog("10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'", out string output2));

        Console.WriteLine(output);
        Console.WriteLine(output2);
    }
}