using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Entities.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string Category { get; set; } = null!;
        public int Quentity { get; set; } = 0;
        public DateTime EntryTime { get; set; }
        public DateTime LastUpdated { get; set; }
        public ICollection<LogTable> Logs { get; set; }
    }
}
