﻿using System;
using System.Collections.Generic;

namespace BankApi.Data.BankModels;

public partial class AccountType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? RegDate { get; set; }

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
