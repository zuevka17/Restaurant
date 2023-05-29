using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public byte IsAdmin { get; set; }

    public virtual ICollection<Ingridient> Ingridients { get; set; } = new List<Ingridient>();

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();
}
