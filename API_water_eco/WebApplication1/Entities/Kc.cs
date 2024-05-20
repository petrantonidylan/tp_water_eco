using System;
using System.Collections.Generic;

namespace WebApplication1.Entities;

public partial class Kc
{
    public int KcId { get; set; }

    public string KcName { get; set; } = null!;

    public decimal KcNeed { get; set; }
}
