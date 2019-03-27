﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDE
{
    class Program
    {
        static void Main(string[] args)
        {
            List<UserInput> userInput = new List<UserInput>();

            userInput.Add(readUserInput());
            
            int userSelection = 0;
            do
            {
                Console.Clear();

                Console.WriteLine("If you want to add one more student, type 1, otherwise type 0: ");
                userSelection = inputAndValidateInt();
                switch (userSelection)
                {
                    case 1:
                        userInput.Add(readUserInput());
                        break;
                    default:
                        break;
                }

            } while (userSelection == 1);

            printTable(userInput);

            Console.ReadKey(true);
        }

        static void printTable(List<UserInput> userInput)
        {
            string header = "Surname".PadRight(12) +
                            "Name".PadRight(18) +
                            "Final points (Avg.)".PadLeft(20);
            Console.WriteLine(header);

            string separator = "--------------------------------------------------";
            Console.WriteLine(separator);

            foreach (UserInput singleInput in userInput) {
                string row = singleInput.lastName.PadRight(12) +
                             singleInput.name.PadRight(18) +
                             calculateFinalResult(singleInput).ToString("0.00").PadLeft(20);
                Console.WriteLine(row);
            }
        }

        static double calculateFinalResult(UserInput userInput)
        {
            return (userInput.grades.Average() * 0.3) + (0.7 * userInput.examGrade);
        }

        static UserInput readUserInput()
        {
            UserInput userInput = new UserInput();
            Console.WriteLine("Please enter your name: ");
            userInput.name = Console.ReadLine();
            Console.WriteLine("Please enter your last name: ");
            userInput.lastName = Console.ReadLine();
            Console.WriteLine("Please enter amount of results you will provide: ");
            userInput.numberOfGrades = inputAndValidateGrade();

            userInput.grades = new List<int>();
            for (int i = 1; i <= userInput.numberOfGrades; i++)
            {
                Console.WriteLine("Please enter {0} result: ", i);
                userInput.grades.Add(inputAndValidateGrade());
            }

            Console.WriteLine("Please enter result of the exam: ");
            userInput.examGrade = inputAndValidateGrade();

            return userInput;
        }

        static int inputAndValidateGrade()
        {
            var x = inputAndValidateInt();

            if (x < 1 || x > 10)
            {
                Console.WriteLine("{0} is not in range 1..10. Please try again: ", x);
                x = inputAndValidateGrade();
            }

            return x;
        }

        static int inputAndValidateInt()
        {
            var input = Console.ReadLine();
            int x;

            while (!int.TryParse(input, out x))
            {
                Console.Write("{0} is not a integer. Please try again: ", input);
                input = Console.ReadLine();
            }

            return x;
        }
    }

    class UserInput
    {
        public string name { set; get; }
        public string lastName { set; get; }
        public int numberOfGrades { set; get; }
        public List<int> grades { set; get; }
        public int examGrade { set; get; }
    }
}
