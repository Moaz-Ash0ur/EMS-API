using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Models
{
    public class Employee : BaseTable
    { 
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Title { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public decimal Salary { get; set; }

        public DateTime HireDate { get; set; }
    
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        // Navigation Properties
        public ICollection<Allowance> Allowances { get; set; } = new List<Allowance>();
        public ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
        public ICollection<Leave> Leaves { get; set; } = new List<Leave>();
        public ICollection<Appreciation> Appreciations { get; set; } = new List<Appreciation>();
    }






}
