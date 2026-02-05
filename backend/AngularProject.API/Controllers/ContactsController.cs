using AngularProject.API.Application.Services;
using AngularProject.API.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AngularProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IContactService _service;

    public ContactsController(IContactService service)
    {
        _service = service;
    }

    // GET: api/contacts
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var contacts = await _service.GetAllAsync();

        return Ok(contacts);
    }

    // GET: api/contacts/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var contact = await _service.GetByIdAsync(id);

        if (contact == null)
        {
            return NotFound($"Contact with ID {id} not found.");
        }

        return Ok(contact);
    }

    // POST: api/contacts
    [HttpPost]
    public async Task<IActionResult> Create(Contact contact)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var created = await _service.CreateAsync(contact);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    // PUT: api/contacts/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Contact contact)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updated = await _service.UpdateAsync(id, contact);

        if (!updated)
        {
            return NotFound($"Contact with ID {id} not found.");
        }

        return NoContent();
    }

    // DELETE: api/contacts/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound($"Contact with ID {id} not found.");
        }

        return NoContent();
    }
}