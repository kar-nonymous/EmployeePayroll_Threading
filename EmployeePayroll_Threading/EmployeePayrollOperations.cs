using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayroll_Threading
{
    public class EmployeePayrollOperations
    {
        /// <summary>
        /// Creating the list
        /// </summary>
        public List<EmployeeDetails> employeePayrollDetailList = new List<EmployeeDetails>();
        /// <summary>
        /// UC 1:
        /// Adding employee details to the list
        /// </summary>
        /// <param name="employeePayrollDataList"></param>
        public void addEmployeeToPayroll(List<EmployeeDetails> employeePayrollDataList)
        {
            employeePayrollDataList.ForEach(employeeData =>
            {
                Console.WriteLine(" Employee being added: " + employeeData.EmpName);
                this.addEmployeePayroll(employeeData);
                Console.WriteLine(" Employee added: " + employeeData.EmpName);
            });
            Console.WriteLine(this.employeePayrollDetailList.Count);
            Console.WriteLine();
        }
        public void addEmployeePayroll(EmployeeDetails emp)
        {
            employeePayrollDetailList.Add(emp);
        }
    }
}
