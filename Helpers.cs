﻿using MathGame.Models;
using System.Diagnostics;
using System.Linq;

namespace MathGame;
internal static class Helpers {

    internal static List<Game> games = new();

    internal static void PrintGames() {
        Console.Clear();
        Console.WriteLine("----------------------------------");
        Console.WriteLine("Games history:");
        Console.WriteLine("\n");

        foreach (Game game in games) {
            Console.WriteLine($"{game.Date} - {game.Type}: {game.Score}");
            Console.WriteLine($"Time to finish: {game.TimeSpent}");
        }

        Console.WriteLine("----------------------------------\n");
        Console.WriteLine("Press any key to go back to main menu:");
        Console.ReadLine();
    }

    internal static int GenerateRandomNumber(int min, int limit) {
        Random random = new Random();
        int randomNumber = random.Next(min, limit);

        return randomNumber;
    }

    internal static int[] ValidateDivisionNumbers(int firstNumber, int secondNumber) {
        int[] result = new int[2];

        while (firstNumber % secondNumber != 0) {
            firstNumber = GenerateRandomNumber(1, 100);
            secondNumber = GenerateRandomNumber(1, 100);
        }

        result[0] = firstNumber;
        result[1] = secondNumber;

        return result;
    }

    internal static void AddToHistory(int gameScore, GameType gameType, string timeSpent) {
        games.Add(new Game {
            Date = DateTime.Now,
            Score = gameScore,
            Type = gameType,
            TimeSpent = timeSpent
        });
    }

    internal static string ValidateResult(string result) {
        while (string.IsNullOrEmpty(result) || !Int32.TryParse(result, out _)) {
            Console.WriteLine("Your answer needs to be an integer. Try again.");
            result = Console.ReadLine();
        }
        return result;
    }

    internal static string GetName() {
        Console.Write("Please, type in your name. \n" +
                      ">");
        string name = Console.ReadLine();

        while (string.IsNullOrEmpty(name)) {
            Console.WriteLine("Name can't be empty.");
            name = Console.ReadLine();
        }

        return name;
    }

    internal static int NumberOfQuestions() {
        Console.WriteLine("How many questions you would want to answer?");
        string questionNum = Console.ReadLine();
        questionNum = ValidateResult(questionNum);

        return int.Parse(questionNum);
    }

    // Works by increasing or decreasing the range of possible values to be displayed on game
    internal static List<int> DifficultySelector(GameType gameType, int difficulty) {
        int firstNumber;
        int secondNumber;

        // Default is "easy" mode

        //Division difficulty
        if (gameType == GameType.Division) {
            if (difficulty == 3) {
                firstNumber = Helpers.GenerateRandomNumber(1, 101);
                secondNumber = Helpers.GenerateRandomNumber(1, 101);

                ValidateDivisionNumbers(firstNumber, secondNumber);
            }
            else if (difficulty == 2) {
                firstNumber = Helpers.GenerateRandomNumber(1, 501);
                secondNumber = Helpers.GenerateRandomNumber(1, 501);

                ValidateDivisionNumbers(firstNumber, secondNumber);
            }
            else {
                firstNumber = Helpers.GenerateRandomNumber(1, 1001);
                secondNumber = Helpers.GenerateRandomNumber(1, 1001);

                ValidateDivisionNumbers(firstNumber, secondNumber);
            }
            return new List<int> { firstNumber, secondNumber };
        }
        // Addition, Subtraction, Multiplication and Random difficulty
        else {
            if (difficulty == 3) {
                firstNumber = Helpers.GenerateRandomNumber(1, 11);
                secondNumber = Helpers.GenerateRandomNumber(1, 11);
            }
            else if (difficulty == 2) {
                firstNumber = Helpers.GenerateRandomNumber(1, 51);
                secondNumber = Helpers.GenerateRandomNumber(1, 51);
            }
            else {
                firstNumber = Helpers.GenerateRandomNumber(1, 101);
                secondNumber = Helpers.GenerateRandomNumber(1, 101);
            }
            return new List<int> { firstNumber, secondNumber };
        }
    }

    internal static string Timer(TimerState timerState) {
        Stopwatch stopWatch = new Stopwatch();
        string elapsedTime;

        switch (timerState) {
            case TimerState.Start:
                stopWatch.Start();

                TimeSpan ts = stopWatch.Elapsed;
                elapsedTime = String.Format("{0:00}h:{1:00}m:{2:00}s.{3:00}ms",
                                                   ts.Hours, ts.Minutes, ts.Seconds,
                                                   ts.Milliseconds / 10);

                return elapsedTime;

            case TimerState.Stop:
                stopWatch.Stop();

                ts = stopWatch.Elapsed;
                elapsedTime = String.Format("{0:00}h:{1:00}m:{2:00}s.{3:00}ms",
                                                   ts.Hours, ts.Minutes, ts.Seconds,
                                                   ts.Milliseconds / 10);

                Console.WriteLine("Time elapsed during last game: " + elapsedTime);
                return elapsedTime;
            default:
                string message = "Use Helpers.Timer.Start or Helpers.Timer.Stop to use this function";
                Console.WriteLine(message);

                return message;
        }
    }

    internal enum TimerState {
        Start,
        Stop
    }
}

