namespace BankApi.Services;
using Microsoft.EntityFrameworkCore;
using BankApi.Data.BankModels;
using BankApi.Data;
using BankApi.Data.DTOs;

public class LoginService
{
    private readonly BankContext _context;
    public LoginService(BankContext context)
    {
        _context = context;
    }
    public async Task<Administrator> GetAdmin(AdminDto adminDto)
    {
        return await _context.Administrators.
            SingleOrDefaultAsync(x=> x.Email.Equals(adminDto.Email) && x.Pwd.Equals(adminDto.Pwd));
    }
}