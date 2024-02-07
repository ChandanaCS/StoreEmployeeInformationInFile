using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;

namespace StoreEmployeeInformationInFileLibrary
{
    public class EmployeeMethods : IEmployee
    {
        string filePath = ConfigurationManager.AppSettings["EmployeeDataFilePath"];
        int lastAssignedEmployeeID;

        private void InitializeLastAssignedEmployeeID()
        {
            if (File.Exists(filePath))
            {
                string existingJson = File.ReadAllText(filePath);
                List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(existingJson);
                int maxEmployeeID = 0;

                if (employees != null && employees.Any())
                {
                    maxEmployeeID = employees.Max(e => e.EmployeeId);
                }
                else
                {
                    maxEmployeeID = 100;
                }
                lastAssignedEmployeeID = maxEmployeeID;
            }
            else
            {
                lastAssignedEmployeeID = Convert.ToInt32(ConfigurationManager.AppSettings["EmployeeStartID"]);
            }
        }
        private int GenerateNextEmployeeID()
        {
            lastAssignedEmployeeID++;
            return lastAssignedEmployeeID;
        }

        public static Dictionary<int, string> LoadDictionary(string dictionaryPrefix)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            int key = 1;

            while (true)
            {
                string keyName = $"{dictionaryPrefix}_{key}";
                string value = ConfigurationManager.AppSettings[keyName];
                if (value == null)
                {
                    break;
                }
                dictionary.Add(key, value);
                key++;
            }

            return dictionary;
        }

        #region AddEmployeeDetails

        public void AddEmployeeDetails()
        {
            try
            {
                InitializeLastAssignedEmployeeID();
                Employee newemployee = new Employee();
                Dictionary<int, string> departmentDictionary = LoadDictionary("Department");
                newemployee.EmployeeId = GenerateNextEmployeeID();

                int isEmployeeNameString = 0;
                while (isEmployeeNameString == 0)
                {
                    Console.Write("Enter Employee name: ");
                    string tempEmployeeName = Console.ReadLine();
                    if (tempEmployeeName.All(char.IsLetter))
                    {
                        newemployee.EmployeeName = tempEmployeeName;
                        isEmployeeNameString = 1;
                    }
                    else
                    {
                        Console.WriteLine("Enter a Valid name..!");
                    }
                }

                int isEmployeePhnoValid = 0;
                while (isEmployeePhnoValid == 0)
                {
                    Console.Write("Enter Employee Phone Number: ");
                    string tempEmployeePhno = Console.ReadLine();
                    if (tempEmployeePhno.Length == 10 && tempEmployeePhno.All(char.IsDigit))
                    {
                        newemployee.EmployeePhoneNumber = tempEmployeePhno;
                        isEmployeePhnoValid = 1;
                    }
                    else
                    {
                        Console.WriteLine("Please Enter Valid Phone Number...!");
                    }
                }
                Console.Write("Enter Employee Address:");
                newemployee.EmployeeAddress = Console.ReadLine();


                Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
                Console.WriteLine("Select the Employee Department: ");

                foreach (var kvp in departmentDictionary)
                {
                    Console.WriteLine($"{kvp.Key}.{kvp.Value}");
                }
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
                newemployee.EmployeeDepartment = departmentDictionary[choice];


                Department_JobTitle dep_jobTitleObj = new Department_JobTitle();
                newemployee.EmployeeJobTitle = dep_jobTitleObj.dept_jobtitle(choice);

                Console.Write("Enter Employee Salary: ");
                newemployee.EmployeeSalary = Convert.ToInt32(Console.ReadLine());

                if (File.Exists(filePath))
                {
                    string existingJson = File.ReadAllText(filePath);
                    List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(existingJson);
                    if (employees == null)
                    {
                        employees = new List<Employee>();
                    }
                    employees.Add(newemployee);
                    string updatedJson = JsonConvert.SerializeObject(employees, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(filePath, updatedJson);

                    Console.WriteLine("Employee Information is appended into the File");
                }
                else
                {
                    List<Employee> employees = new List<Employee> { newemployee };
                    string newJson = JsonConvert.SerializeObject(employees, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(filePath, newJson);

                    Console.WriteLine("Employee Information is written into the File");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occured: {ex.Message}");
            }
        }

        #endregion

        #region RemoveEmployeeDetails

        public void RemoveEmployeeDetails()
        {
            try
            {
                Console.Write("Enter the Employee ID to remove: ");
                int employeeIDToRemove = Convert.ToInt32(Console.ReadLine());

                if (File.Exists(filePath))
                {
                    string existingJson = File.ReadAllText(filePath);
                    List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(existingJson);

                    if (employees == null)
                    {
                        Console.WriteLine($"No employee found with EmployeeID: {employeeIDToRemove}");
                        return;
                    }

                    int removedRecords = employees.RemoveAll(e => e.EmployeeId == employeeIDToRemove);

                    if (removedRecords > 0)
                    {
                        string updatedJson = JsonConvert.SerializeObject(employees, Newtonsoft.Json.Formatting.Indented);
                        File.WriteAllText(filePath, updatedJson);
                        Console.WriteLine($"Employee with EmployeeID {employeeIDToRemove} removed successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"No employee found with this EmployeeID: {employeeIDToRemove}");
                    }
                }
                else
                {
                    Console.WriteLine($"File not found: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
            }
        }

        #endregion

        #region DisplayEmployeeDetailsByEmployeeID

        public void DisplayEmployeeDetailsByEmployeeID()
        {
            try
            {
                Console.WriteLine("Enter Employee ID to get the details: ");
                int employeeIDToSearch = Convert.ToInt32(Console.ReadLine());

                if (File.Exists(filePath))
                {
                    string existingJson = File.ReadAllText(filePath);
                    List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(existingJson);

                    if (employees != null)
                    {
                        Employee foundEmployee = employees.Find(e => e.EmployeeId == employeeIDToSearch);

                        if (foundEmployee != null)
                        {
                            PrintEmployee(foundEmployee);
                        }
                        else
                        {
                            Console.WriteLine($"No employee found with EmployeeID: {employeeIDToSearch}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No employees found in the JSON file.");
                    }
                }
                else
                {
                    Console.WriteLine($"File not found: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
            }
        }

        #endregion

        #region DisplayEmployeeDetailsByEmployeeName

        public void DisplayEmployeeDetailsByEmployeeName()
        {
            try
            {
                Console.WriteLine("Enter Employee Name to get the details: ");
                string employeeNameToSearch = Console.ReadLine();

                if (File.Exists(filePath))
                {
                    string existingJson = File.ReadAllText(filePath);
                    List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(existingJson);

                    if (employees != null)
                    {
                        Employee foundEmployee = employees.Find(e => e.EmployeeName == employeeNameToSearch);

                        if (foundEmployee != null)
                        {
                            PrintEmployee(foundEmployee);
                        }
                        else
                        {
                            Console.WriteLine($"No employee found with Employee Name: {employeeNameToSearch}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No employees found in the JSON file.");
                    }
                }
                else
                {
                    Console.WriteLine($"File not found: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
            }
        }

        #endregion

        #region DiaplayAllEmployeeDetails

        public void DiaplayAllEmployeeDetails()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string existingJson = File.ReadAllText(filePath);
                    List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(existingJson);

                    if (employees != null)
                    {
                        Console.WriteLine("All Employee Details:");
                        Console.WriteLine("------------------------------");
                        foreach (Employee employee in employees)
                        {
                            PrintEmployee(employee);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No employees found in the JSON file.");
                    }
                }
                else
                {
                    Console.WriteLine($"File not found: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
            }
        }

        #endregion

        private static void PrintEmployee(Employee foundEmployee)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Employee found with EmployeeID {foundEmployee.EmployeeId}:");
            Console.WriteLine($"Name: {foundEmployee.EmployeeName}");
            Console.WriteLine($"Phone Number: {foundEmployee.EmployeePhoneNumber}");
            Console.WriteLine($"Address: {foundEmployee.EmployeeAddress}");
            Console.WriteLine($"Job Title: {foundEmployee.EmployeeJobTitle}");
            Console.WriteLine($"Department: {foundEmployee.EmployeeDepartment}");
            Console.WriteLine($"Salary: {foundEmployee.EmployeeSalary}");
            Console.WriteLine("------------------------------");
        }
    }
}
