using System.CommandLine;
using System.Text.RegularExpressions;
using Advent.Common;

namespace Advent.Day3;

public class Day3Handler : IHandler
{

    public Command CreateHandlerCommand()
    {
        var inputOption = new Option<string>("--file", "File path of the puzzle input file");
        var day3command = new Command("day3", "Returns the answer for Advent of Code Day 3.");
        var part1command = new Command("part1", "Part 1 of the Day 3 puzzle"){
            inputOption
        };
        part1command.SetHandler(GetMultipliersPart1, inputOption);
        var part2command = new Command("part2", "Part 2 of the Day 3 puzzle"){
            inputOption
        };
        part2command.SetHandler(GetMultipliersPart2, inputOption);

        day3command.AddCommand(part1command);
        day3command.AddCommand(part2command);
        return day3command;
    }

    private void GetMultipliersPart1(string filePath)
    {
        var contents = FileParser(filePath);

        var rgx = new Regex(@"(?<=mul\()(\d{1,3}),(\d{1,3})(?=\))");

        int total = 0;
        var matches = rgx.Matches(contents).ToList();
        matches.ForEach(m =>
        {
            var a = int.Parse(m.Groups[1].Value);
            var b = int.Parse(m.Groups[2].Value);
            total += a * b;
        });

        Console.WriteLine(total);
    }

    private void GetMultipliersPart2(string filePath)
    {
        var contents = FileParser(filePath);

        int total = 0;

        var rgx = new Regex(@"(?<=mul\()(\d{1,3}),(\d{1,3})(?=\))|do\(\)|don't\(\)");
        var matches = rgx.Matches(contents).ToList();

        bool d = true;
        foreach (var m in matches)
        {
            if (m.Value == "do()")
            {
                d = true;
                continue;
            }
            else if (m.Value == "don't()")
            {
                d = false;
                continue;
            }
            if (d)
            {
                var a = int.Parse(m.Groups[1].Value);
                var b = int.Parse(m.Groups[2].Value);
                total += a * b;
            }
        };

        Console.WriteLine(total);
    }

    private string FileParser(string filePath)
    {
        try
        {
            var result = new List<List<int>>();
            using StreamReader reader = new(filePath);

            return reader.ReadToEnd();
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
            return null;
        }
    }
}
