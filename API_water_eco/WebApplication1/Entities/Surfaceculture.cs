using System;
using System.Collections.Generic;

namespace WebApplication1.Entities;

public partial class Surfaceculture
{
    public int SurId { get; set; }

    public decimal SurValue { get; set; }

    public string SurUnit { get; set; } = null!;

    public int WatId { get; set; }

    public virtual Watervolume Wat { get; set; } = null!;
}
