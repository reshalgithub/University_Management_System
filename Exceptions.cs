using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNIVERSITY_MANAGEMENT
{
    
    public class StudentNotFoundException : Exception
    {
        public StudentNotFoundException() : base("Student not found")
        {
        }
    }
    public class CourseNotFoundException : Exception
    {
        public CourseNotFoundException() : base("Course Not Found")
        {
        }
    }
    public class FacultyNotFoundException : Exception
    {
        public FacultyNotFoundException() : base("Faculty not found")
        {
        }
    }
}
