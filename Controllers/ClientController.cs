using Microsoft.AspNetCore.Mvc;
using BankApi.Data;
using BankApi.Services;
using BankApi.Data.BankModels;
using Microsoft.AspNetCore.Authorization;

namespace BankApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly ClientService _service;
    public ClientController(ClientService service)
    {
        this._service = service;
    }
    [HttpGet]
    public async Task<IEnumerable<Client>> Get()
    {
        return await _service.GetAll();
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Client>> GetById(int id)
    {
        var client = await _service.GetById(id);
        if (client == null)
        {
            return ClientNotFound(id);
        }
        return client;
    }
    [Authorize(Policy ="SuperAdmin")]
    [HttpPost]
    public async Task<IActionResult> CreateClients(Client client)
    {
        await _service.Create(client);
        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Client client)
    {
        if (id != client.Id)
        {
            return BadRequest();
        }
        var clientFind = await _service.GetById(id);
        if (clientFind == null)
        {
            return ClientNotFound(id);
        }
        await _service.Update(id, client);

        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var client = await _service.GetById(id);
        if (id != client.Id)
        {
            return BadRequest();
        }
        if (client == null)
        {
            return ClientNotFound(id);
        }
        await _service.Delete(id);

        return NoContent();
    }
    public NotFoundObjectResult ClientNotFound(int id)
    {
        return NotFound(new { message= $"El cliente con el id {id} no existe" });
    }
}