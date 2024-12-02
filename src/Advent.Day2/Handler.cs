using System.CommandLine;
using System.Security;
using System.Text.RegularExpressions;
using Advent.Common;

namespace Advent.Day2;

public class Day2Handler : IHandler
{

    public Command CreateHandlerCommand()
    {
        var inputOption = new Option<string>("--file", "File path of the puzzle input file");
        var day2command = new Command("day2", "Returns the answer for Advent of Code Day 2.");
        var part1command = new Command("part1", "Part 1 of the Day 2 puzzle"){
            inputOption
        };
        part1command.SetHandler((filePath) =>
        {
            GetReportSafety(filePath, false);
        }
        , inputOption);
        var part2command = new Command("part2", "Part 2 of the Day 2 puzzle"){
            inputOption
        };
        part2command.SetHandler((filePath) =>
        {
            GetReportSafety(filePath, true);
        }
        , inputOption);

        day2command.AddCommand(part1command);
        day2command.AddCommand(part2command);
        return day2command;
    }

    private void GetReportSafety(string filePath, bool dampener = false)
    {
        var reports = FileParser(filePath);

        int safeReports = 0;

        reports.ForEach(r =>
        {
            bool safe = true;
            safe = IsReportSafe(r);
            if (!safe && dampener)
            {
                for (int i = 0; i < r.Count; i++)
                {
                    var removed = new List<int>(r);
                    removed.RemoveAt(i);
                    safe = IsReportSafe(removed);
                    if (safe) break;
                }
            }

            if (safe) safeReports++;
        });

        Console.WriteLine(safeReports);
    }

    private bool IsReportSafe(List<int> report)
    {
        bool increasing = false;
        bool safe = true;
        for (int i = 1; i < report.Count; i++)
        {
            var diff = report[i] - report[i - 1];
            if (i == 1)
            {
                increasing = diff > 0;
            }
            if (Math.Abs(diff) is < 1 or > 3
            || increasing && diff < 0
            || !increasing && diff > 0)
            {
                safe = false;
                break;
            }
        }

        return safe;
    }

    private List<List<int>> FileParser(string filePath)
    {
        try
        {
            var result = new List<List<int>>();
            using StreamReader reader = new(filePath);

            // Read the stream line by line.
            String line;
            while ((line = reader?.ReadLine()) != null)
            {
                // Process line
                var numbers = line.Split(' ').Select(int.Parse).ToList();
                result.Add(numbers);
            }

            return result;
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
            return null;
        }
    }
}
