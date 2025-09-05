using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.DAL.Models
{
    public class Allowance :BaseTable
    {

        public string Type { get; set; }

        public decimal Amount { get; set; }

        public DateTime DateGiven { get; set; }

        public bool IsActive { get; set; } = true;

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
    


    



}
