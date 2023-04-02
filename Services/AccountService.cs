namespace BankApi.Services;
using Microsoft.EntityFrameworkCore;
using BankApi.Data.BankModels;
using BankApi.Data;
using BankApi.Data.DTOs;

public class AccountService
{
    private readonly BankContext _context;
    public AccountService(BankContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<AccountDtoOut>> GetAccounts()
    {
        return await _context.Accounts.Select(a => new AccountDtoOut
        {
            Id= a.Id,
            AccountName= a.AccountTypeNavigation.Name,
            ClientName= a.Client.Name,
            Balance= a.Balance,
            RegDate= a.RegDate
            
        }).ToListAsync();
    }
    public async Task<AccountDtoOut> GetAccountById(int id)
    {
        var client = await _context.Accounts.Where(a => a.Id== id).Select(a => new AccountDtoOut
        {
            Id= a.Id,
            AccountName= a.AccountTypeNavigation.Name,
            ClientName= a.Client.Name,
            Balance= a.Balance,
            RegDate= a.RegDate
            
        }).SingleOrDefaultAsync();

        return client;
    }
    public async Task CreateAccount(AccountDtoIn account)
    {
        var newAccount = new Account();
        newAccount.AccountType = account.AccountType;
        newAccount.ClientId = account.ClientId;
        newAccount.Balance = account.Balance;

        _context.Accounts.Add(newAccount);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAccount(int id, AccountDtoIn account)
    {
        var newAccount = _context.Accounts.Find(id);
        newAccount.AccountType = account.AccountType;
        newAccount.ClientId = account.ClientId;
        newAccount.Balance = account.Balance;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAccount(int id)
    {
        var account = await _context.Accounts.FindAsync(id);
        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
    }

}