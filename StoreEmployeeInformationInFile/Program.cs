using System;

using StoreEmployeeInformationInFileLibrary;

namespace StoreEmployeeInformationInFile
{
    class Program
    {
        static void Main(string[] args)
        {
            IEmployee employee = new EmployeeMethods();
            int userchoice = 0;
            string doesUserWantToContinue = "Y";

            do
            {
                try
                {
                    Console.WriteLine("Menu:\n1.Enter 1 to Add Employee Details\n2.Enter 2 to Remove an Employee \n" +
                        "3.Enter 3 to search for an Employee through EmployeeID\n" +
                        "4.Enter 4 to search for an Employee through Employee Name\n" +
                        "5.Enter 5 to get information about all Employees\n");
                    userchoice = Convert.ToInt32(Console.ReadLine());
                    switch (userchoice)
                    {
                        case 1:
                            employee.AddEmployeeDetails();
                            break;
                        case 2:
                            employee.RemoveEmployeeDetails();
                            break;
                        case 3:
                            employee.DisplayEmployeeDetailsByEmployeeID();
                            break;
                        case 4:
                            employee.DisplayEmployeeDetailsByEmployeeName();
                            break;
                        case 5:
                            employee.DiaplayAllEmployeeDetails();
                            break;
                        default:
                            Console.WriteLine("Enter valid number!!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An exception occured: {ex.Message}");
                }
                Console.WriteLine("Do u want to continue y/Y, n/N");
                doesUserWantToContinue = Console.ReadLine();
            }
            while (doesUserWantToContinue.ToUpper() == "Y");
        }
    }
}
