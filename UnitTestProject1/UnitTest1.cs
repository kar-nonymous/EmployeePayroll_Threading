using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using EmployeePayroll_Threading;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        /// UC 1:
        /// Adding without thread
        public void Given4Employee_WhenAddedTOList_ShouldMatchEmployeeEntries()
        {
            List<EmployeeDetails> employeeDetails = new List<EmployeeDetails>();
            employeeDetails.Add(new EmployeeDetails { EmpID = 1, EmpName = "Kumar Kartikeya", PhoneNo = "7206183244", Address = "Tec", Department = "IT", Gender = "Male" });
            employeeDetails.Add(new EmployeeDetails { EmpID = 2, EmpName = "Robert Lewandowski", PhoneNo = "8521479630", Address = "Bayern", Department = "Football", Gender = "Male" });
            employeeDetails.Add(new EmployeeDetails { EmpID = 3, EmpName = "Douglas Costa", PhoneNo = "8745219630", Address = "Bayern", Department = "Football", Gender = "Male" });
            employeeDetails.Add(new EmployeeDetails { EmpID = 4, EmpName = "Meg Lanning", PhoneNo = "9632587410", Address = "Australia", Department = "Cricket", Gender = "Female" });
            EmployeePayrollOperations employeePayrolloperations = new EmployeePayrollOperations();
            DateTime startDateTime = DateTime.Now;
            employeePayrolloperations.addEmployeeToPayroll(employeeDetails);
            DateTime stopDateTime = DateTime.Now;
            Console.WriteLine("Duration without thread: " + (stopDateTime - startDateTime));
        }
    }
}
