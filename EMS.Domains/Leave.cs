using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.DAL.Models
{
    public class Leave : BaseTable
    {
 
        public string Reason { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsAccepted { get; set; }

        public bool IsActive { get; set; } = true;
    
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }




}
