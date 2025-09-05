﻿namespace EMS.DAL.Models
{
    public class RefreshToken : BaseTable
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public Guid UserId { get; set; }
    }


}
