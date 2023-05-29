using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class Ingridient
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Amount { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
