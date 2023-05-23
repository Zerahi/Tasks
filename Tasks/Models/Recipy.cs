using System;
using System.Collections.Generic;

namespace Tasks;

public partial class Recipy
{
    public int Id { get; set; }

    public int OutputCount { get; set; }

    public virtual ICollection<Part> Parts { get; } = new List<Part>();

    public virtual ICollection<RecipyPart> RecipyParts { get; } = new List<RecipyPart>();
}
