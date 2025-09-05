using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace EMS.DAL.Models
{
    public class Appreciation : BaseTable
    {
        public string Note { get; set; } = string.Empty;

        public DateTime DateGiven { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }






}
