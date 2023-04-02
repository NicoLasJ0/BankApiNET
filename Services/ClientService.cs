using Microsoft.EntityFrameworkCore;
using BankApi.Data;
using BankApi.Data.BankModels;
namespace BankApi.Services;

public class ClientService
{
    private readonly BankContext _context;
    public ClientService(BankContext context)
    {
        this._context = context;
    }
    public async Task<IEnumerable<Client>> GetAll()
    {
        return await _context.Clients.ToListAsync();
    }
    public async Task<Client?> GetById(int id)
    {
        return await _context.Clients.FindAsync(id);
    }
    public async Task<Client> Create(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return client;
    }
    public async Task Update(int id, Client client)
    {
        var clientFind = await _context.Clients.FindAsync(id);
        clientFind.Name = client.Name;
        clientFind.PhoneNumber = client.PhoneNumber;
        clientFind.Email = client.Email;
        await _context.SaveChangesAsync();
    }
    public async Task Delete(int id)
    {
        var client =  await _context.Clients.FindAsync(id);
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }
}