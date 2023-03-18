using System;
using System.Collections.Generic;

namespace Tasks;

public partial class Todo
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Notes { get; set; }

    public DateTime DueDate { get; set; }

    public int? DaysBetween { get; set; }

    public int? DaysSkipped { get; set; }

    public int Priority { get; set; }
}
