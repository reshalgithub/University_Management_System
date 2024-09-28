using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNIVERSITY_MANAGEMENT.Properties;

namespace UNIVERSITY_MANAGEMENT
{
    public class CreateTables
    {
        public static void CreateTable()
        {
            string createStudentTable = @"
            CREATE TABLE Student (
                StudentId INTEGER PRIMARY KEY IDENTITY(1,1),
                StudentName NVARCHAR(50) NOT NULL,
                Email NVARCHAR(50) NOT NULL,
                DOB DATE NOT NULL,
                EnrollmentDate DATE NOT NULL,
                StudentType NVARCHAR(50)
            );";

            string createFacultyTable = @"
            CREATE TABLE Faculty (
            FacultyId INT PRIMARY KEY IDENTITY(1,1),
            FacultyName NVARCHAR(50) NOT NULL,
            FacultyDept NVARCHAR(50) NOT NULL
            );";

            string createCourseTable = @"
            CREATE TABLE Course (
            CourseId INT PRIMARY KEY IDENTITY(1,1),
            CourseName NVARCHAR(50) NOT NULL,
            Credits INT NOT NULL);
            ";

            string courseFacultyTable = @"
            CREATE TABLE FacultyCourses (
            Faculty_Id INT,         
            Course_Id INT,          
            PRIMARY KEY (Faculty_Id, Course_Id),  
            FOREIGN KEY (Faculty_Id) REFERENCES Faculty(FacultyId),
            FOREIGN KEY (Course_Id) REFERENCES Course(CourseId)
            );";

            string paymentTable = @"
            CREATE TABLE Payment (
            PaymentId INT PRIMARY KEY IDENTITY(1,1),
            StudentId INT NOT NULL,
            Amount DECIMAL(18, 2) NOT NULL,
            PaymentMethod NVARCHAR(50) NOT NULL,
            
            );";
            //PaymentDate DATETIME NOT NULL
            SqlParameter[] parameters = { };

            try
            {
                DatabaseAccess.ExecuteNonQuery(createStudentTable, parameters);
                Console.WriteLine("Student Table created successfully");

                DatabaseAccess.ExecuteNonQuery(createFacultyTable, parameters);
                Console.WriteLine("Faculty Table created successfully");

                DatabaseAccess.ExecuteNonQuery(createCourseTable, parameters);
                Console.WriteLine("Course Table created successfully");

                DatabaseAccess.ExecuteNonQuery(courseFacultyTable, parameters);
                Console.WriteLine("Course-Faculty Table created successfully");

                DatabaseAccess.ExecuteNonQuery(paymentTable, parameters);
                Console.WriteLine("Payment Table created successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
