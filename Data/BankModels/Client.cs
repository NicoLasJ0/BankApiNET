﻿using System;
using System.Collections.Generic;

namespace BankApi.Data.BankModels;

public partial class Client
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public DateTime? RegDate { get; set; }

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
