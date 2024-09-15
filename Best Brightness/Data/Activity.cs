using System;

namespace BestBrightness.Data
{
    public class Activity
    {
        public int Id { get; set; } // Primary key
        public int EmployeeId { get; set; } // Foreign key
        public DateTime LoginDateTime { get; set; }
        public DateTime? LogoutDateTime { get; set; }

        // Navigation property
        public Employee Employee { get; set; }
    }
}