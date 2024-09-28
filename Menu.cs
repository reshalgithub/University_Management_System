using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNIVERSITY_MANAGEMENT.Properties;

namespace UNIVERSITY_MANAGEMENT
{
    public class Menu
    {
        public void Display()
        {
            while (true)
            {
                //Console.Clear();
                Console.WriteLine(string.Concat(Enumerable.Repeat("- *", 10)));
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Remove Student");
                Console.WriteLine("3. Display Students");
                Console.WriteLine("4. Add Course");
                Console.WriteLine("5. Remove Course");
                Console.WriteLine("6. Display Courses");
                Console.WriteLine("7. Add Faculty");
                Console.WriteLine("8. Remove Faculty");
                Console.WriteLine("9. Display Faculties");
                Console.WriteLine("10. Calculate Student Fees");
                Console.WriteLine("11. Process Payment");
                Console.WriteLine("12. Exit");


                Console.WriteLine(string.Concat(Enumerable.Repeat("- *", 10)));

                Console.WriteLine("Enter your choice");
                int.TryParse(Console.ReadLine(), out int choice);
                switch (choice)
                {
                    case 1:
                        Student.AddStudent();        
                        break;
                    case 2:
                        Student.RemoveStudent();
                        break;
                    case 3:
                        Student.DisplayStudent();                  
                        break;
                    case 4:
                        Course.AddCourse();
                        break;
                    case 5:
                        Course.RemoveCourse();
                        break;
                    case 6:
                        Course.DisplayCourse();
                        break;
                    case 7:
                        Faculty.AddFaculty();
                        break;
                    case 8:
                        Faculty.RemoveFaculty();
                        break;
                    case 9:
                        Faculty.DisplayFaculty();
                        break;
                    case 10:
                        Student.CalculateStudentFees();
                        break;
                    case 11:
                        Payment.MakePayment();
                        break;
                    case 12:
                        Environment.Exit(0);
                        break;                    
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
