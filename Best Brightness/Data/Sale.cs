using BestBrightness.Data;
using System.Collections.Generic;
using System;

public class Sale
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal Change { get; set; }

    // Navigation properties
    public Employee Employee { get; set; }
    public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
}
