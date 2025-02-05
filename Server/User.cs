﻿using System;
using System.Collections.Generic;

namespace TodoApi;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
