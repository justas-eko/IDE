using System;
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
            var userInput = readUserInput();

            Console.WriteLine("Final result for student {0} {1} is: {2}", userInput.name, userInput.lastName, calculateFinalResult(userInput));

            Console.ReadKey(true);
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
            var input = Console.ReadLine();
            int x;

            while (!int.TryParse(input, out x))
            {
                Console.Write("{0} is not a integer. Please try again: ", input);
                input = Console.ReadLine();
            }

            if (x < 1 || x > 10)
            {
                Console.WriteLine("{0} is not in range 1..10. Please try again: ", x);
                x = inputAndValidateGrade();
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
