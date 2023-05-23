using System;
using System.Collections.Generic;

namespace Tasks;

public partial class RecurringType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Expense> Expenses { get; } = new List<Expense>();
}
