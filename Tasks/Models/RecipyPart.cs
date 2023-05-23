using System;
using System.Collections.Generic;

namespace Tasks;

public partial class RecipyPart
{
    public int Id { get; set; }

    public int RecipyId { get; set; }

    public int PartId { get; set; }

    public int Count { get; set; }

    public virtual Part Part { get; set; } = null!;

    public virtual Recipy Recipy { get; set; } = null!;
}
