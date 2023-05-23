using System;
using System.Collections.Generic;

namespace Tasks;

public partial class Expense
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Amount { get; set; }

    public int CostTypeId { get; set; }

    public DateTime Date { get; set; }

    public int? RecurringTypeId { get; set; }

    public int? ParentId { get; set; }

    public string? Comment { get; set; }

    public int Importance { get; set; }

    public virtual CostType CostType { get; set; } = null!;

    public virtual RecurringType? RecurringType { get; set; }
}
