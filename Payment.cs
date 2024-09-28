using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using UNIVERSITY_MANAGEMENT.Properties;

namespace UNIVERSITY_MANAGEMENT
{
    public class Payment
    {
        public static void MakePayment()
        {
            try
            {
                Console.WriteLine("Enter the type of payment: 1.Credit Card or 2.Bank Transfer");
                int.TryParse(Console.ReadLine(), out int option);
                while (option != 1 && option != 2)
                {
                    Console.WriteLine("Enter the right payment option");
                    int.TryParse(Console.ReadLine(), out option);
                }
                AbstractPaymentGateway abstractPaymentGateway = null;

                //studentid and amount from calculateStudentFees which sees which type of student is:PartTime or FullTime.
                (int studentId, double amount) = Student.CalculateStudentFees();
        
                int paymentExists = DatabaseAccess.checkPaymentdone(studentId);
                if (paymentExists > 0)
                {
                    Console.WriteLine("But, Payment is already processed by " + studentId);
                    return;
                }
                if (amount == 0) { return; }
          
                if (option == 1)
                {
                    abstractPaymentGateway = new CreditCardPaymentGateway();
                }
                else
                {
                    abstractPaymentGateway = new BankTransferPaymentGateway();
                }
                abstractPaymentGateway.PaymentDetails();
                insertPayment(studentId, amount, option);     
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }                  
        }
        public static void insertPayment(int studentID,double amount,int option)
        {
            try
            {
                int result = DatabaseAccess.doPayment(studentID, amount, option);
                if (result > 0)
                {
                    Console.WriteLine("Payment Successfull. Thank You");
                }
            }            
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }       
    }
    public abstract class AbstractPaymentGateway
    {
        public abstract void PaymentDetails();
    }

    public class CreditCardPaymentGateway : AbstractPaymentGateway
    {
        public override void PaymentDetails()
        {
            Console.WriteLine("Payment to be done by credit card::");
            Console.WriteLine("Enter your 12 digit credit card no.");
        
            string cardNo = Console.ReadLine();
            while(!IsValidCard(cardNo))
            {
                Console.WriteLine("Invalid Card Number.Please Enter it Again..");
                cardNo = Console.ReadLine();
            }
        }
        public static bool IsValidCard(string cardNo)
        {
            return cardNo.Length == 12 && cardNo.All(char.IsDigit);
        }
    }

    public class BankTransferPaymentGateway : AbstractPaymentGateway
    {
        public override void PaymentDetails()
        {
            Console.WriteLine("Payment to be done by Bank Transfer::");
            Console.WriteLine("Enter your 10 digit Bank Account Number.");
            string accountNo = Console.ReadLine();
            while (!IsValidAccountNo(accountNo))
            {
                Console.WriteLine("Invalid Bank Account Number.Please Enter it Again..");
                accountNo = Console.ReadLine();
            }
        }
        public static bool IsValidAccountNo(string accountno)
        {
            return accountno.Length == 10 && accountno.All(char.IsDigit);
        }
    }   
}
