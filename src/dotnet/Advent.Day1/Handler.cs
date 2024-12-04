using System.CommandLine;
using System.Text.RegularExpressions;
using Advent.Common;

namespace Advent.Day1;

public class Day1Handler : IHandler
{

    public Command CreateHandlerCommand()
    {
        var inputOption = new Option<string>("--file", "File path of the puzzle input file");
        var day1command = new Command("day1", "Returns the answer for Advent of Code Day 1.");
        var part1command = new Command("part1", "Part 1 of the Day1 puzzle"){
            inputOption
        };
        part1command.SetHandler(GetTotalDistance, inputOption);
        var part2command = new Command("part2", "Part 2 of the Day1 puzzle"){
            inputOption
        };
        part2command.SetHandler(GetSimilarityScore, inputOption);

        day1command.AddCommand(part1command);
        day1command.AddCommand(part2command);
        return day1command;
    }

    private void GetSimilarityScore(string filePath)
    {
        var lists = FileParser(filePath);

        int similarityScore = 0;

        for (var i = 0; i < lists.Item1.Count; i++)
        {
            var leftNumber = lists.Item1[i];
            var rightListCount = lists.Item2.Where(n => n == leftNumber).Count();
            similarityScore += leftNumber * rightListCount;
        }

        Console.WriteLine(similarityScore);
    }

    private void GetTotalDistance(string filePath)
    {
        var lists = FileParser(filePath);

        lists.Item1.Sort();
        lists.Item2.Sort();
        var sortedList1 = lists.Item1;
        var sortedList2 = lists.Item2;

        int totalDistance = 0;

        for (int i = 0; i < sortedList1.Count; i++)
        {
            totalDistance += Math.Abs(sortedList1[i] - sortedList2[i]);
        }

        Console.WriteLine(totalDistance);
    }

    // TODO: Do this normally with arrays
    private Tuple<List<int>, List<int>> FileParser(string filePath)
    {
        try
        {
            var list1 = new List<int>();
            var list2 = new List<int>();

            var rgx = new Regex(@"(\d*)   (\d*)");
            // Open the text file using a stream reader.
            using StreamReader reader = new(filePath);

            // Read the stream line by line.
            String line;
            while ((line = reader?.ReadLine()) != null)
            {
                // Process line
                var matches = rgx.Match(line);
                list1.Add(int.Parse(matches.Groups[1].Value));
                list2.Add(int.Parse(matches.Groups[2].Value));
            }

            return new Tuple<List<int>, List<int>>(list1, list2);
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
            return null;
        }
    }
}
