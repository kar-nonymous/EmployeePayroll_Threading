using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayroll_Threading
{
    public class EmployeeDetails
    {
        public int CompanyId { get; set; }
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public string Gender { get; set; }
        public double BasicPay { get; set; }
        public double Deductions { get; set; }
        public double TaxablePay { get; set; }
        public double Tax { get; set; }
        public double NetPay { get; set; }
        public DateTime StartDate { get; set; }
    }
}
