using System;
using System.Collections.Generic;

namespace Tasks;

public partial class Part
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int RecipyId { get; set; }

    public virtual Recipy Recipy { get; set; } = null!;

    public virtual ICollection<RecipyPart> RecipyParts { get; } = new List<RecipyPart>();
}
