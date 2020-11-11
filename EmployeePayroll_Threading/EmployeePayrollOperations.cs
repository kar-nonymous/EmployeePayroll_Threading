using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        /// Setting up the connection with the database
        /// </summary>
        public static string connectionString = "Data Source=KARTIKEYA;Initial Catalog=payroll_service;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection connection = new SqlConnection(connectionString);
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
                this.AddEmployeePayroll(employeeData);
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
                    this.AddEmployeePayroll(employeeData);
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
                      this.AddEmployeePayroll(employeeData);
                      Console.WriteLine("Employee added:" + employeeData.EmpName);
                      Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                      mutex.ReleaseMutex();
                  });
                thread.Start();
            });
        }
        public void AddEmployeePayroll(EmployeeDetails emp)
        {
            employeePayrollDetailList.Add(emp);
        }
        /// <summary>
        /// UC 5:
        /// Adding employee payroll data to multiple tables 
        /// </summary>
        /// <param name="employeeDetails"></param>
        /// <returns></returns>
        public bool AddMultipleEmployeesToDatabase(EmployeeDetails employeeDetails)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("dbo.spAddMultipleEmployees", this.connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CompanyId", employeeDetails.CompanyId);
                    command.Parameters.AddWithValue("@EmpId", employeeDetails.EmpID);
                    command.Parameters.AddWithValue("@EmpName", employeeDetails.EmpName);
                    command.Parameters.AddWithValue("@Gender", employeeDetails.Gender);
                    command.Parameters.AddWithValue("@PhoneNo", employeeDetails.PhoneNo);
                    command.Parameters.AddWithValue("@Address", employeeDetails.Address);
                    command.Parameters.AddWithValue("@StartDate", employeeDetails.StartDate);
                    command.Parameters.AddWithValue("@BasicPay", employeeDetails.BasicPay);
                    command.Parameters.AddWithValue("@Deductions", employeeDetails.Deductions);
                    command.Parameters.AddWithValue("@TaxablePay", employeeDetails.TaxablePay);
                    command.Parameters.AddWithValue("@IncomeTax", employeeDetails.Tax);
                    command.Parameters.AddWithValue("@NetPay", employeeDetails.NetPay);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// UC 6:
        /// Updates salary with given employee name and id.
        /// </summary>
        /// <param name="employeeDetails"></param>
        /// <returns></returns>
        public bool UpdateEmployees(EmployeeDetails employeeDetails)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("dbo.spUpdateSalary", this.connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", employeeDetails.EmpID);
                    command.Parameters.AddWithValue("@name", employeeDetails.EmpName);
                    command.Parameters.AddWithValue("@salary", employeeDetails.BasicPay);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
