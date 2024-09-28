using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using static UNIVERSITY_MANAGEMENT.Student;

namespace UNIVERSITY_MANAGEMENT.Properties
{
    public class DatabaseAccess
    {
        private static string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        private static SqlConnection conn = new SqlConnection(cs);

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(cs);
        }
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public static object ExecuteScalar(string query, SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
        public static SqlDataReader ExecuteReader(string query, SqlParameter[] parameters)
        {
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand(query, conn);
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            conn.Open();
            return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }
        public static void addStudent(string studentName,string studentEmail,string DOB,string enrollDate,int option)
        {
            string query = "INSERT INTO Student (StudentName, Email, DOB, EnrollmentDate, StudentType) OUTPUT INSERTED.StudentId VALUES (@studentName, @studentEmail, @DOB, @enrollDate, @studentType)";
            SqlParameter[] parameters = {
            new SqlParameter("@studentName", studentName),
            new SqlParameter("@studentEmail", studentEmail),
            new SqlParameter("@DOB", DOB),
            new SqlParameter("@enrollDate", enrollDate),
            new SqlParameter("@studentType", option == 1 ? "Part Time" : "Full Time")
            };

            int StudentID = Convert.ToInt32(ExecuteScalar(query, parameters));
            Console.WriteLine("Student successfully added with ID : " + StudentID);
        }
        public static int removeStudent(int studentId)
        {
            string query = "DELETE FROM Student WHERE StudentId = @studentId";
            SqlParameter[] parameters = { new SqlParameter("@studentId", studentId) };

            int result = ExecuteNonQuery(query, parameters);
            return result;
        }
        public static void displayStudent()
        {
            string query = "select * from student";
            SqlParameter[] parameters = { };
            using (SqlDataReader reader = ExecuteReader(query, parameters))
            {
                Console.WriteLine("{0,-6} {1,-10} {2,-18} {3, -14} {4, -16} {5, -10}", "ID", "Name", "Email", "D-O-B", "Enrollment Date", "Student Type");
                Console.WriteLine(new string('-', 80));
                while (reader.Read())
                {
                    string id = reader.GetInt32(0).ToString();
                    string name = reader.GetString(1);
                    string email = reader.GetString(2);
                    string dob = reader.GetDateTime(3).ToShortDateString();
                    string enrollment = reader.GetDateTime(4).ToShortDateString();
                    string studentType = reader.GetString(5);
                    Console.WriteLine("{0,-6} {1,-10} {2,-18} {3, -14} {4, -16} {5, -10}", id,name,email,dob,enrollment, studentType);
                }
                reader.Close();
            }
        }       
        public static int addFaculty(string FacultyName,string dept)
        {
            string addFaculty = "Insert into Faculty(FacultyName,FacultyDept) OUTPUT INSERTED.FacultyId values (@facultyName,@facultyDept)";
            SqlParameter[] parameters = {
                new SqlParameter("@facultyName", FacultyName),
                new SqlParameter("@facultyDept", dept) };

            int facultyId = Convert.ToInt32(ExecuteScalar(addFaculty, parameters));
            return facultyId;
        }
        public static int addfacultycourses(int facultyId, int courseId)
        {
            string assignCourseToFacultyQuery = " INSERT INTO FacultyCourses (Faculty_Id, Course_Id) VALUES (@FacultyId, @CourseId)";
            SqlParameter[] parameters1 = {
             new SqlParameter("@FacultyId", facultyId),
             new SqlParameter("@CourseId", courseId) };

            return ExecuteNonQuery(assignCourseToFacultyQuery, parameters1);
             
        }
        public static int removefaculty(int facultyId)
        {
            string deleteFacultyCourses = "DELETE FROM FacultyCourses WHERE Faculty_Id = @facultyId";
            SqlParameter[] parameters = {
                    new SqlParameter("@facultyId", facultyId)
                };
            int result = ExecuteNonQuery(deleteFacultyCourses, parameters);


            string removeFaculty = "Delete from Faculty where FacultyId = @facultyId";
            SqlParameter[] parameters1 = {
                    new SqlParameter("@facultyId", facultyId)
                };
            ExecuteNonQuery(removeFaculty, parameters1);
            return result;
        }
        public static void displayfaculty()
        {
            string displayFaculty = "SELECT f.FacultyId, f.FacultyName, f.FacultyDept,c.CourseId, c.CourseName FROM Faculty f Left JOIN FacultyCourses fc ON f.FacultyId = fc.Faculty_Id LEft JOIN Course c ON fc.Course_Id = c.CourseId";
            SqlParameter[] parameters = new SqlParameter[] { };

            using (SqlDataReader dr = ExecuteReader(displayFaculty, parameters))
            {
                Console.WriteLine("{0,-6} {1,-12} {2,-10}", "ID", "Name", "Department");
                Console.WriteLine(new string('-', 35));
                while (dr.Read())
                {
                    string id = dr.GetInt32(0).ToString();
                    string name = dr.GetString(1);
                    string Dept = dr.GetString(2);
                    Console.WriteLine("{0,-6} {1,-12} {2,-10}",id,name,Dept);
                }
                dr.Close();
            }
        }
        public static int addcourse(string CourseName,int CourseCredits)
        {
            string addCourseQuery = "INSERT INTO Course (CourseName, Credits) output Inserted.CourseId VALUES (@CourseName, @CourseCredits)";
            SqlParameter[] parameters = {
                    new SqlParameter("@CourseName", CourseName),
                    new SqlParameter("@CourseCredits", CourseCredits) };
            return (Convert.ToInt32(ExecuteScalar(addCourseQuery, parameters)));
        }
        public static int removeCourse(int courseId)
        {
            string deleteFacultyCourses = "DELETE FROM FacultyCourses WHERE Course_Id = @courseId";
            SqlParameter[] parameters = new SqlParameter[]
            {new SqlParameter("@courseId", courseId)};
            int result = ExecuteNonQuery(deleteFacultyCourses, parameters);

            string removeCourse = "Delete from Course where CourseID = @courseId";
            SqlParameter[] parameters1 = new SqlParameter[]
            {new SqlParameter("@courseId", courseId)};
            ExecuteNonQuery(removeCourse, parameters1);
            return result;
        }
        public static void displaycourse()
        {
            string displayCourse = "select * from Course";
            SqlParameter[] parameters = { };
            using (SqlDataReader dr = ExecuteReader(displayCourse, parameters))
            {
                Console.WriteLine("{0,-6} {1,-12} {2,-6}", "ID", "Name", "Credits");
                Console.WriteLine(new string('-', 30));
                while (dr.Read())
                {
                    string id = dr.GetInt32(0).ToString();
                    string name = dr.GetString(1);
                    string Credits = dr.GetInt32(2).ToString();
                    Console.WriteLine("{0,-6} {1,-12} {2,-6}", id, name, Credits);
                }
                dr.Close();
            }
        }

        public static int checkPaymentdone(int studentId)
        {
            string checkPaymentQuery = "SELECT COUNT(1) FROM Payment WHERE StudentId = @studentId";
            SqlParameter[] parameters = { new SqlParameter("@studentId", studentId) };
            return(Convert.ToInt32(DatabaseAccess.ExecuteScalar(checkPaymentQuery, parameters)));
        }
        public static int doPayment(int studentID,double amount,int option)
        {
            string payment = "INSERT INTO Payment (StudentId, Amount, PaymentMethod) VALUES (@studentId, @amount, @payMethod)";
            SqlParameter[] parameters = {
                new SqlParameter("@studentId", studentID),
                new SqlParameter("@amount", amount),
                new SqlParameter("@payMethod", option == 1 ? "Credit Card" : "Bank Transfer")
                };

            Console.WriteLine("Please Wait...");
            Thread.Sleep(2000);
            return(ExecuteNonQuery(payment, parameters));
        }
    }
}
