using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IDE
{
    class Program
    {
        static void Main()
        {
            List<UserInput> userInput = new List<UserInput>();

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("C:\\Users\\JustasWin\\Documents\\IDE\\IDE\\IDE\\students.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    string header = sr.ReadLine();

                    while (sr.Peek() >= 0)
                    {
                        userInput.Add(ReadLineFromFile(sr.ReadLine()));
                        Console.WriteLine();
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            PrintTable(userInput);

            Console.ReadKey(true);
        }

        static void PrintTable(List<UserInput> userInput)
        {
            string header = "Surname".PadRight(12) +
                            "Name".PadRight(18) +
                            "Final points (Avg.)".PadLeft(20) +
                            "   " +
                            "Final points (Med.)".PadLeft(20); ;
            Console.WriteLine(header);

            string separator = "-------------------------------------------------------------------------";
            Console.WriteLine(separator);

            userInput = userInput.OrderBy(o => o.LastName).ToList();

            foreach (UserInput singleInput in userInput) {
                string row = singleInput.LastName.PadRight(12) +
                             singleInput.Name.PadRight(18) + 
                             CalculateFinalAverage(singleInput).ToString("0.00").PadLeft(20) +
                             CalculateFinalMedian(singleInput).ToString("0.00").PadLeft(23);
                Console.WriteLine(row);
            }
        }

        static double CalculateFinalAverage(UserInput userInput)
        {
            return (userInput.Grades.Average() * 0.3) + (0.7 * userInput.ExamGrade);
        }

        static double CalculateFinalMedian(UserInput userInput)
        {
            return (Convert.ToDouble(GetMedian(userInput.Grades)) * 0.3) + (0.7 * userInput.ExamGrade);
        }

        static decimal GetMedian(IEnumerable<int> source)
        {
            // Create a copy of the input, and sort the copy
            int[] temp = source.ToArray();
            Array.Sort(temp);

            int count = temp.Length;
            if (count == 0)
            {
                throw new InvalidOperationException("Empty collection");
            }
            else if (count % 2 == 0)
            {
                // count is even, average two middle elements
                int a = temp[count / 2 - 1];
                int b = temp[count / 2];
                return (a + b) / 2m;
            }
            else
            {
                // count is odd, return the middle element
                return temp[count / 2];
            }
        }

        static UserInput ReadLineFromFile(String inputLine)
        {
            string[] stringEntries = RemoveSpaces(inputLine).Trim(' ').Split(' ');

            UserInput userInput = new UserInput();
            userInput.LastName = stringEntries[0];
            userInput.Name = stringEntries[1];

            userInput.Grades = new List<int>();
            for (int i = 2; i < stringEntries.Length - 1; i++)
            {
                userInput.Grades.Add(ValidateInt(stringEntries[i]));
            }

            userInput.ExamGrade = ValidateInt(stringEntries[stringEntries.Length - 1]);

            return userInput;
        }

        static string RemoveSpaces(string inputLine)
        {
            string s2 = inputLine;
            do
            {
                inputLine = s2;
                s2 = s2.Replace("  ", " ");
            } while (inputLine != s2);

            return inputLine;
        }

        static int ValidateInt(string input)
        {

            if (!int.TryParse(input, out int x))
            {
                throw new System.ArgumentException("Incorrect input!");
            }


            return x;
        }
    }

    class UserInput
    {
        public string Name { set; get; }
        public string LastName { set; get; }
        public int NumberOfGrades { set; get; }
        public List<int> Grades { set; get; }
        public int ExamGrade { set; get; }
    }
}
