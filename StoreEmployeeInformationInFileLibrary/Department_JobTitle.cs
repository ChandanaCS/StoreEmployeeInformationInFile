using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreEmployeeInformationInFileLibrary
{
    class Department_JobTitle
    {
        string returnJobTitle;
        public string dept_jobtitle(int choice)
        {
            int choiceForJobTitle;
            Dictionary<int, string> hrJobTitleDictionary = EmployeeMethods.LoadDictionary("HR");
            Dictionary<int, string> salesJobTitleDictionary = EmployeeMethods.LoadDictionary("Sales");
            Dictionary<int, string> marketingJobTitleDictionary = EmployeeMethods.LoadDictionary("Marketing");
            Dictionary<int, string> financeJobTitleDictionary = EmployeeMethods.LoadDictionary("Finance");
            Dictionary<int, string> itJobTitleDictionary = EmployeeMethods.LoadDictionary("IT");

            if (choice == 1)
            {
                Console.WriteLine("Select the Job Title:");
                foreach (var kvp in hrJobTitleDictionary)
                {
                    Console.WriteLine($"{kvp.Key}.{kvp.Value}");
                }
                choiceForJobTitle = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
                returnJobTitle = hrJobTitleDictionary[choiceForJobTitle];
            }
            else if (choice == 2)
            {
                Console.WriteLine("Select the Job Title:");
                foreach (var kvp in salesJobTitleDictionary)
                {
                    Console.WriteLine($"{kvp.Key}.{kvp.Value}");
                }
                choiceForJobTitle = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
                returnJobTitle = salesJobTitleDictionary[choiceForJobTitle];
            }
            else if (choice == 3)
            {
                Console.WriteLine("Select the Job Title:");
                foreach (var kvp in marketingJobTitleDictionary)
                {
                    Console.WriteLine($"{kvp.Key}.{kvp.Value}");
                }
                choiceForJobTitle = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
                returnJobTitle = marketingJobTitleDictionary[choiceForJobTitle];
            }
            else if (choice == 4)
            {
                Console.WriteLine("Select the Job Title:");
                foreach (var kvp in financeJobTitleDictionary)
                {
                    Console.WriteLine($"{kvp.Key}.{kvp.Value}");
                }
                choiceForJobTitle = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
                returnJobTitle = financeJobTitleDictionary[choiceForJobTitle];
            }
            else if (choice == 5)
            {
                Console.WriteLine("Select the Job Title:");
                foreach (var kvp in itJobTitleDictionary)
                {
                    Console.WriteLine($"{kvp.Key}.{kvp.Value}");
                }
                choiceForJobTitle = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
                returnJobTitle = itJobTitleDictionary[choiceForJobTitle];
            }

            return returnJobTitle;
        }
    }
}
