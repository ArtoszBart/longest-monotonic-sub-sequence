using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ASD1
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

    class MonotoneSequence
    {
        private readonly int[] stack;
        private string order;
        private int currentLength;

        public MonotoneSequence(int maxLength, int firstElement)
        {
            stack = new int[maxLength];
            stack[0] = firstElement;
            currentLength = 1;
        }

        public bool TryPush(int element)
        {
            if (stack[currentLength - 1] > element && order == "asc")
            {
                return false;
            }

            if (stack[currentLength - 1] < element && order == "desc")
            {
                return false;
            }

            Push(element);

            if (string.IsNullOrEmpty(order) && currentLength >= 2)
            {
                DetectAndSetOrder();
            }

            return true;
        }

        public int GetLength()
        {
            return currentLength;
        }

        public int GetSum()
        {
            int sum = 0;
            for (int ii = 0; ii < currentLength; ii++)
            {
                sum += stack[ii];
            }
            return sum;
        }

        private void Push(int element)
        {
            stack[currentLength] = element;
            currentLength++;
        }

        private void DetectAndSetOrder()
        {
            if (stack[currentLength - 2] < stack[currentLength - 1])
            {
                order = "asc";
            }
            else if (stack[currentLength - 2] > stack[currentLength - 1])
            {
                order = "desc";
            }
        }

        public override string ToString()
        {
            string result = "";
            for (int ii = 0; ii < currentLength; ii++)
            {
                result += stack[ii].ToString() + ' ';
            }

            return result;
        }
    }
}
