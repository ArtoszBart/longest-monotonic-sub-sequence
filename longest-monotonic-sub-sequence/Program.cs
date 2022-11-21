using System;
using System.IO;
using System.Text.RegularExpressions;

namespace longest_monotonic_sub_sequence
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = GetNumbersFromFile(args);

            if (numbers == null)
            {
                Console.ReadLine();
                return;
            }

            MonotoneSequence result = FindLongestMonotonicSubSequence(numbers);

            Console.WriteLine(result);
            Console.WriteLine($"{result.GetLength()} {result.GetSum()}");
            Console.ReadLine();
        }

        private static MonotoneSequence FindLongestMonotonicSubSequence(int[] numbers)
        {
            MonotoneSequence maxMs = new MonotoneSequence(numbers.Length, numbers[0]);
            MonotoneSequence tmpMs = new MonotoneSequence(numbers.Length, numbers[0]);

            bool isGoingBack = false;
            int newArrStartValue = 0;
            for (int ii = 1; ii < numbers.Length;)
            {
                if (!isGoingBack)
                {
                    bool isSuccess = tmpMs.TryPush(numbers[ii]);

                    if (!isSuccess)
                    {
                        if (tmpMs.GetLength() > maxMs.GetLength())
                        {
                            maxMs = tmpMs;
                        }
                        isGoingBack = true;
                        ii--;
                        newArrStartValue = numbers[ii];
                    }
                    else
                    {
                        ii++;
                    }
                }
                else
                {
                    if (numbers[ii - 1] != newArrStartValue)
                    {
                        isGoingBack = false;
                        tmpMs = new MonotoneSequence(numbers.Length - ii, numbers[ii]);
                        ii++;
                    }
                    else
                    {
                        ii--;
                    }
                }
            }

            if (tmpMs.GetLength() > maxMs.GetLength())
            {
                maxMs = tmpMs;
            }

            return maxMs;
        }

        private static int[] GetNumbersFromFile(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("File path has to be provided in program arguments.");
                return null;
            }

            string fileString;
            try
            {
                fileString = File.ReadAllText(args[0]).Trim();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            if (!Regex.IsMatch(fileString, @"^[0-9]+( [0-9]+)*$"))
            {
                Console.WriteLine($"File {args[0]} is empty or has wrong format");
                Console.ReadLine();
                return null;
            }
            string[] stringNumbers = fileString.Split(' ');

            int[] numbers = new int[stringNumbers.Length];
            for (int ii = 0; ii < stringNumbers.Length; ii++)
            {
                numbers[ii] = int.Parse(stringNumbers[ii]);
            }

            return numbers;
        }
    }
}
