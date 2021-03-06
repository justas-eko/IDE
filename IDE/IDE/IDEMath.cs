﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace IDE
{
    class IDEMath
    {
        public double CalculateFinalAverage(UserInput userInput)
        {
            return (userInput.Grades.Average() * 0.3) + (0.7 * userInput.ExamGrade);
        }

        public double CalculateFinalMedian(UserInput userInput)
        {
            return (Convert.ToDouble(GetMedian(userInput.Grades)) * 0.3) + (0.7 * userInput.ExamGrade);
        }

        public decimal GetMedian(IEnumerable<int> source)
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
    }
}
