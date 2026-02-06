using System.Globalization;

namespace Third;

public static class LogFormater
{
    public static bool FormatLog(string input, out string output)
    {
        output = "";

        var dateSlice = input.Substring(0, 10);

        bool isDateFirstFormat = DateTime.TryParseExact(dateSlice, "yyyy-MM-dd", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime firstDate);
        bool isDateSecondFormat = DateTime.TryParseExact(dateSlice, "dd.MM.yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime secondDate);

        if (!isDateFirstFormat && !isDateSecondFormat)
        {
            return false;
        }

        if (isDateFirstFormat)
        {
            return FormatFirstLog(firstDate, input.Substring(11), out output);
        }
        else
        {
            return FormatSecondLog(secondDate, input.Substring(11), out output);
        }
    }

    private static bool FormatFirstLog(DateTime date, string input, out string output)
    {
        output = "";
        return false;
    }

    private static bool FormatSecondLog(DateTime date, string input, out string output)
    {
        output = "";
        return true;
    }
}


public static class Ulala
{
    public static void Main()
    {
        Console.WriteLine(LogFormater.FormatLog("2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'", out string output));
        Console.WriteLine(LogFormater.FormatLog("10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'", out string output2));
    }
}