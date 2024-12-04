// See https://aka.ms/new-console-template for more information
using System.CommandLine;
using Advent.Day1;
using Advent.Day2;
using Advent.Day3;

var rootCommand = new RootCommand("CLI app for Advent of Code");

// TODO: Do this better
var day1Handler = new Day1Handler();
var day2Handler = new Day2Handler();
var day3Handler = new Day3Handler();

rootCommand.AddCommand(day1Handler.CreateHandlerCommand());
rootCommand.AddCommand(day2Handler.CreateHandlerCommand());
rootCommand.AddCommand(day3Handler.CreateHandlerCommand());

return await rootCommand.InvokeAsync(args);