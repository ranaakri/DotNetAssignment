using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Entities.Models
{
    public class LogTable
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Action { get; set; } = null!;
        public DateTime ActionTime { get; set; }
        public Product Product { get; set; }
    }
}
