namespace BankApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using BankApi.Data.DTOs;
using BankApi.Services;
using BankApi.Data.BankModels;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;
    public AccountController(AccountService service)
    {
        _service = service;
    }
    [HttpGet]
    public async Task<IEnumerable<AccountDtoOut>> Get()
    {
        return await _service.GetAccounts();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<AccountDtoOut>> GetById(int id)
    {
        var account = await _service.GetAccountById(id);
        if (account == null)
        {
            return BadRequest();
        }
        return account;
    }
    [HttpPost]
    public async Task<ActionResult> Create(AccountDtoIn account)
    {
        await _service.CreateAccount(account);
        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, AccountDtoIn account)
    {
        var newAccount = await _service.GetAccountById(id);
        if (id != account.Id)
        {
            return BadRequest();
        }
        if (newAccount == null)
        {
            return NotFound();
        }
        await _service.UpdateAccount(id, account);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var account = await _service.GetAccountById(id);
        if (id != account.Id)
        {
            return BadRequest();
        }
        await _service.DeleteAccount(id);
        return NoContent();
    }
}