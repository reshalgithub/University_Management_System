using System;
using System.Data.SqlClient;
using UNIVERSITY_MANAGEMENT.Properties;

namespace UNIVERSITY_MANAGEMENT
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        //public List<string> CoursesTaught { get; set; }
        public Faculty(int id,string name, string dept)
        {
            Id = id;    
            Name = name;
            Department = dept;                 
        }
        public static void AddFaculty()
        {
            Console.WriteLine("Enter the Faculty Name");
            string FacultyName = Console.ReadLine();
            while (string.IsNullOrEmpty(FacultyName))
            {
                Console.WriteLine("Faculty name cannot be empty. Please enter a valid name:");
                FacultyName = Console.ReadLine();
            }
            Console.WriteLine("Enter the Department for the Faculty ");
            string dept = Console.ReadLine();
            while (string.IsNullOrEmpty(dept))
            {
                Console.WriteLine("Department name cannot be empty. Please enter a valid name:");
                dept = Console.ReadLine();
            }
            int facultyId = DatabaseAccess.addFaculty(FacultyName, dept);
            Console.WriteLine("Faculty successfully added with ID: " + facultyId);           
            try
            {
                while (true)
                {
                    Console.WriteLine("Enter the Course ID to assign to faculty(or type 'exit' to stop):");
                    string courseInput = Console.ReadLine();

                    if (courseInput.ToLower() == "exit")
                        break;
                    if (int.TryParse(courseInput, out int courseId))
                    {                       
                        try
                        {
                            int result = DatabaseAccess.addfacultycourses(facultyId, courseId);
                            if (result > 0)
                            {
                                Console.WriteLine("Course ID " + courseId + " successfully assigned to Faculty");
                            }
                        }
                        catch (SqlException)
                        {
                            Console.WriteLine("Invalid Course ID. Please enter a valid number.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }                     
        }
        public static void RemoveFaculty()
        {
            Console.WriteLine("Enter the facultyId to remove");
            int facultyId = int.Parse(Console.ReadLine());
            try
            {
                int result = DatabaseAccess.removefaculty(facultyId);
                if (result > 0)
                {
                    Console.WriteLine("Faculty Deleted");
                }
                else
                {
                    throw new FacultyNotFoundException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        public static void DisplayFaculty()
        {
            try
            {
                DatabaseAccess.displayfaculty();
            }   
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }           
        }
    }
}
