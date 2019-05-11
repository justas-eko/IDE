using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IDE
{
    class Program
    {
        private static readonly Random r = new Random();
        private static readonly string dir = "C:\\Users\\JustasWin\\Documents\\IDE\\IDE\\IDE\\";
        static void Main()
        {
            GenerateStudentsFile(100);
            GenerateStudentsFile(1000);
            GenerateStudentsFile(10000);
            GenerateStudentsFile(100000);

            Console.WriteLine("-----Queue");
            ReadAndOutputStudentsFileDeque(100);
            ReadAndOutputStudentsFileDeque(1000);
            ReadAndOutputStudentsFileDeque(10000);
            ReadAndOutputStudentsFileDeque(100000);
            Console.WriteLine("-----------------");


            Console.WriteLine("Press any key to end..");
            Console.ReadKey(true);
        }

        static void ReadAndOutputStudentsFileDeque(int length)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            ReadFilesAndOutputResultsDeque(length, "Queue");
            Console.WriteLine("To process " + length.ToString() + " lines took: " + watch.ElapsedMilliseconds + "ms");
            Console.WriteLine();
        }

        static void ReadFilesAndOutputResultsDeque(int length, string enumerableType)
        {
            Queue<UserInput> userInput = new Queue<UserInput>();
            string inFileName = "students" + length.ToString() + ".txt";
            Console.WriteLine("Reading from: " + inFileName);
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(dir + inFileName))
                {
                    // Read the stream to a string, and write the string to the console.
                    string header = sr.ReadLine();

                    while (sr.Peek() >= 0)
                    {
                        userInput.Enqueue(ReadLineFromFile(sr.ReadLine()));
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }


            PrintTable(userInput, length, enumerableType);

            Console.WriteLine("Done reading from: " + inFileName);
        }

        static void GenerateStudentsFile(int length)
        {
            using (StreamWriter outputFile = new StreamWriter(dir + "students" + length.ToString() + ".txt"))
            {
                outputFile.WriteLine("Surname     Name        HW1 HW2 HW3 HW4 HW5  Exam");
                for (int i = 0; i < length; i++)
                {
                    outputFile.WriteLine(GenerateUserInputString(GenerateUserInput(i)));
                }
            }
        }

        static UserInput GenerateUserInput(int i)
        { 
            return new UserInput
            {
                LastName = "Surname" + i.ToString(),
                Name = "Name" + i.ToString(),
                Grades = new List<int>() { r.Next(1, 10), r.Next(1, 10), r.Next(1, 10), r.Next(1, 10), r.Next(1, 10) },
                ExamGrade = r.Next(1, 10)
            };
        }

        static string GenerateUserInputString(UserInput userInput)
        {
            return userInput.LastName.PadRight(12) +
                userInput.Name.PadRight(12) +
                userInput.Grades[0].ToString().PadLeft(3) +
                userInput.Grades[1].ToString().PadLeft(4) +
                userInput.Grades[2].ToString().PadLeft(4) +
                userInput.Grades[3].ToString().PadLeft(4) +
                userInput.Grades[4].ToString().PadLeft(4) +
                userInput.ExamGrade.ToString().PadLeft(6);
        }

        static void PrintTable(IEnumerable<UserInput> userInput, int length, string enumerableType)
        {
            try
            {
                userInput = userInput.OrderBy(o => o.LastName).ToList();

                string outFileNameFailed = "students" + length.ToString() + "_failed_" + enumerableType + ".txt";
                string outFileNamePassed = "students" + length.ToString() + "_passed_" + enumerableType + ".txt";

                StreamWriter failedStream = new StreamWriter(dir + outFileNameFailed);
                StreamWriter passedStream = new StreamWriter(dir + outFileNamePassed);
                StreamWriter outStream;

                string header = "Surname".PadRight(12) +
                    "Name".PadRight(18) +
                    "Final points (Avg.)".PadLeft(20) +
                    "   " +
                    "Final points (Med.)".PadLeft(20); ;
                failedStream.WriteLine(header);
                passedStream.WriteLine(header);
                string separator = "-------------------------------------------------------------------------";
                failedStream.WriteLine(separator);
                passedStream.WriteLine(separator);

                IDEMath ideMath = new IDEMath();
                foreach (UserInput singleInput in userInput)
                {
                    double average = ideMath.CalculateFinalAverage(singleInput);

                    if (average < 5.0)
                    {
                        outStream = failedStream;
                    }
                    else
                    {
                        outStream = passedStream;
                    }
                    string row = singleInput.LastName.PadRight(12) +
                                 singleInput.Name.PadRight(18) +
                                 average.ToString("0.00").PadLeft(20) +
                                 ideMath.CalculateFinalMedian(singleInput).ToString("0.00").PadLeft(23);
                    outStream.WriteLine(row);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Errors occured while printing: ");
                Console.WriteLine(e.Message);
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
