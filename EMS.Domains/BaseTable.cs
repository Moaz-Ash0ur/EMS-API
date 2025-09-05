namespace EMS.DAL.Models
{
    public class BaseTable
    {
        public int Id { get; set; }

        public string? UpdatedBy { get; set; }

        public int CurrentState { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

    }






}
