using System.Collections.Generic;

namespace BestBrightness.Data
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }

        // Navigation property
        public ICollection<Sale> Sales { get; set; }
        public ICollection<Activity> Activities { get; set; }
    }
}
