using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UNIVERSITY_MANAGEMENT.Properties;

namespace UNIVERSITY_MANAGEMENT
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int FacultyId { get; set; }


        public Course(int id, string name, int credits, int facultyid)
        {
            Id = id;
            Name = name;
            Credits = credits;
            FacultyId = facultyid;
        }

        public static void AddCourse()
        {
            Console.WriteLine("Enter the Course Name");
            string CourseName = Console.ReadLine();
            while (string.IsNullOrEmpty(CourseName))
            {
                Console.WriteLine("Student name cannot be empty. Please enter a valid name:");
                CourseName = Console.ReadLine();
            }
            Console.WriteLine("Enter the Credits for the course ");
            int.TryParse(Console.ReadLine(),out int CourseCredits);
            while(CourseCredits == 0) { Console.WriteLine("A course credit cannot be empty.Please enter again"); int.TryParse(Console.ReadLine(), out CourseCredits);  }
            try
            {   
                int courseID = DatabaseAccess.addcourse(CourseName, CourseCredits);
                Console.WriteLine("Course successfully added with ID : " +courseID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }           
        }
        public static void RemoveCourse()
        { 
            try
            {
                Console.WriteLine("Enter the courseId to remove");
                int courseId = int.Parse(Console.ReadLine());
                int result = DatabaseAccess.removeCourse(courseId);

                if (result > 0)
                {
                    Console.WriteLine("Course deleted successfully");
                }
                else
                {
                    throw new CourseNotFoundException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }          
        }
        public static void DisplayCourse()
        {
            try
            {
                DatabaseAccess.displaycourse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
