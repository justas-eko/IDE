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

            try
            {
                PrintTable(userInput);
            }
            catch (IOException e)
            {
                Console.WriteLine("Errors occured while printing: ");
                Console.WriteLine(e.Message);
            }


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

            IDEMath ideMath = new IDEMath();

            foreach (UserInput singleInput in userInput) {
                string row = singleInput.LastName.PadRight(12) +
                             singleInput.Name.PadRight(18) +
                             ideMath.CalculateFinalAverage(singleInput).ToString("0.00").PadLeft(20) +
                             ideMath.CalculateFinalMedian(singleInput).ToString("0.00").PadLeft(23);
                Console.WriteLine(row);
            }
        }

        static UserInput ReadLineFromFile(string inputLine)
        {
            IDEUtils ideUtils = new IDEUtils();
            string[] stringEntries = ideUtils.RemoveSpaces(inputLine).Trim(' ').Split(' ');

            UserInput userInput = new UserInput();
            userInput.LastName = stringEntries[0];
            userInput.Name = stringEntries[1];

            userInput.Grades = new List<int>();
            for (int i = 2; i < stringEntries.Length - 1; i++)
            {
                userInput.Grades.Add(ideUtils.ValidateInt(stringEntries[i]));
            }

            userInput.ExamGrade = ideUtils.ValidateInt(stringEntries[stringEntries.Length - 1]);

            return userInput;
        }
    }
}
