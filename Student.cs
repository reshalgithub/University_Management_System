using System;
using System.Data.SqlClient;
using System.Globalization;
using UNIVERSITY_MANAGEMENT.Properties;

namespace UNIVERSITY_MANAGEMENT
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string StudentType {get; set; }
        public Student(int id, string name, string email, DateTime dOB, DateTime enrollmentDate)
        {
            Id = id;
            Name = name;
            Email = email;
            DOB = dOB;
            EnrollmentDate = enrollmentDate;
           
        }
        public virtual double CalculateFees()
        {
            //default calculate fees
            return 10000;
        }
        public static void AddStudent()
        {
            Console.WriteLine("Enter the type of student: 1.Part Time Student or 2.Full Time Student");
            int.TryParse(Console.ReadLine(),out int option);
            while (option != 1 && option != 2)
            {
                Console.WriteLine("Enter the right option");
                int.TryParse(Console.ReadLine(), out option);
            }

            Console.WriteLine("Enter the student Name");
            string studentName = Console.ReadLine();
            while (string.IsNullOrEmpty(studentName))
            {
                Console.WriteLine("Student name cannot be empty. Please enter a valid name:");
                studentName = Console.ReadLine();
            }

            Console.WriteLine("Enter the student Email");
            string studentEmail = Console.ReadLine();
            while (string.IsNullOrEmpty(studentEmail) || !IsValidEmail(studentEmail))
            {
                Console.WriteLine("Invalid email format. Please enter a valid email:");
                studentEmail = Console.ReadLine();
            }

            string DOB = "";
            DOB = ValidDate(DOB,"DOB");

            string enrollDate = "";
            enrollDate = ValidDate(enrollDate,"Enrollment date");

           
            
            DatabaseAccess.addStudent(studentName, studentEmail, DOB, enrollDate,option);
            
        }
        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private static string ValidDate(string date, string msg)
        {
            string DOB = "";
            while (string.IsNullOrEmpty(DOB))
            {
                Console.WriteLine($"Enter the student {msg}: ");

                if (DateTime.TryParse(Console.ReadLine(), out DateTime dateInput))
                {
                    // If parsing is successful, assign the formatted date to DOB
                    DOB = dateInput.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    // If parsing fails, print an error message
                    Console.WriteLine("Invalid Date.Enter again.");
                }
            }
            return DOB;
        }
        public static void RemoveStudent()
        {
            try
            {
                Console.WriteLine("Enter the studentId to remove");
                int studentId = int.Parse(Console.ReadLine());

                int result = DatabaseAccess.removeStudent(studentId);
                if(result > 0)
                {
                    Console.WriteLine("Student removed successfully");
                }
                else
                {
                    throw new StudentNotFoundException();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public static void DisplayStudent()
        {
            DatabaseAccess.displayStudent();
        }
        public static (int studentId, double fees) CalculateStudentFees()
        {
            Console.WriteLine("Enter student ID");
            int id = int.Parse(Console.ReadLine());

            string getStudentquery = "select * from student where StudentId = @id";
            SqlParameter[] parameters = { new SqlParameter("@id", id) };

            using (SqlDataReader dr = DatabaseAccess.ExecuteReader(getStudentquery, parameters))
            {
                if (dr.Read())
                {
                    int s_id = dr.GetInt32(0);
                    string name = dr.GetString(1);
                    string email = dr.GetString(2);
                    DateTime dob = dr.GetDateTime(3);
                    DateTime enrollment = dr.GetDateTime(4);
                    string studentType = dr.GetString(5);

                    Student student;
                    if (studentType == "Part Time")
                    {
                        student = new PartTimeStudent(s_id, name, email, dob, enrollment);
                    }
                    else
                    {
                        student = new FullTimeStudent(s_id, name, email, dob, enrollment);
                    }
                    double amount = student.CalculateFees();
                    return (id, amount);
                }
                else
                {
                    Console.WriteLine("Student not found");
                    return (0, 0);
                }
            }
        }
        public class PartTimeStudent : Student
        {
            public PartTimeStudent(int id, string name, string email, DateTime dOB, DateTime enrollmentDate) : base(id, name, email, dOB, enrollmentDate)
            { }
            public override double CalculateFees()
            {
                Console.WriteLine("You are a Part Time Student. So your fees is 5000");
                return 5000;
            }
        }
        public class FullTimeStudent : Student
        {
            public FullTimeStudent(int id, string name, string email, DateTime dOB, DateTime enrollmentDate) : base(id, name, email, dOB, enrollmentDate)
            { }
            public override double CalculateFees()
            {
                Console.WriteLine("You are a Full Time Student. So your fees is 10000");
                return 10000;
            }
        }
    }
}
