using System.ComponentModel.DataAnnotations;

namespace EMS.DAL.Models
{
    public class Department : BaseTable
    { 
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }

}
