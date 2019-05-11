using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDE
{
    class UserInput
    {
        public string Name { set; get; }
        public string LastName { set; get; }
        public int NumberOfGrades { set; get; }
        public List<int> Grades { set; get; }
        public int ExamGrade { set; get; }
    }
}
