using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeePayroll_Threading
{
    public class EmployeePayrollOperations
    {
        /// <summary>
        /// Creating the list
        /// </summary>
        public List<EmployeeDetails> employeePayrollDetailList = new List<EmployeeDetails>();
        /// <summary>
        /// Mutex class is defined to loack the syncronization of each thread
        /// </summary>
        public Mutex mutex = new Mutex();
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
        /// <summary>
        /// UC 2 & UC 3:
        /// Adding mutiple employee details to the list with thread
        /// </summary>
        /// <param name="employeePayrollDataList"></param>
        public void AddEmployeeToPayrollWithThread(List<EmployeeDetails> employeePayrollDataList)
        {
            employeePayrollDataList.ForEach(employeeData =>
            {
                Task thread = new Task(() =>
                {
                    Console.WriteLine(" Employee being added: " + employeeData.EmpName);
                    this.addEmployeePayroll(employeeData);
                    Console.WriteLine(" Employee added: " + employeeData.EmpName);
                });
                thread.Start();
            });
            Console.WriteLine(this.employeePayrollDetailList.Count);
        }
        /// <summary>
        /// UC 4:
        /// Synchronises the addition of multiple employees to list using Mutex() class.
        /// </summary>
        /// <param name="employeePayrollDataList"></param>
        public void SynchronizingAddEmployeeWithThread(List<EmployeeDetails> employeePayrollDataList)
        {
            employeePayrollDataList.ForEach(employeeData =>
            {
                Task thread = new Task(() =>
                  {
                      mutex.WaitOne();
                      Console.WriteLine("Employee Being added" + employeeData.EmpName);
                      this.addEmployeePayroll(employeeData);
                      Console.WriteLine("Employee added:" + employeeData.EmpName);
                      Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                      mutex.ReleaseMutex();
                  });
                thread.Start();
            });
        }

        public void addEmployeePayroll(EmployeeDetails emp)
        {
            employeePayrollDetailList.Add(emp);
        }
    }
}
