using System;
using System.Collections.Generic;

namespace WebApplication1.Entities;

public partial class Watervolume
{
    public int WatId { get; set; }

    public string WatName { get; set; } = null!;

    public decimal WatMaxVolume { get; set; }

    public decimal WatCurrentVolume { get; set; }

    public string WatUnit { get; set; } = null!;

    public int WatInsee { get; set; }

    public virtual ICollection<Surfaceculture> Surfacecultures { get; } = new List<Surfaceculture>();
}
