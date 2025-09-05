using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.DAL.Models
{
    public class Promotion : BaseTable
    {
      
        public string NewTitle { get; set; }
        public decimal NewSalary { get; set; }
        public DateTime EffectiveDate { get; set; }
        public bool IsActive { get; set; } = true;
       
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }


}
