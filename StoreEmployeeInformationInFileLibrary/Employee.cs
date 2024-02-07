using System;

namespace StoreEmployeeInformationInFileLibrary
{
    [Serializable()]
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePhoneNumber { get; set; }
        public string EmployeeAddress { get; set; }
        public string EmployeeJobTitle { get; set; }
        public string EmployeeDepartment { get; set; }
        public int EmployeeSalary { get; set; }
    }
}
