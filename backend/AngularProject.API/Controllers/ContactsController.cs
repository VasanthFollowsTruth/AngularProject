using AngularProject.API.Application.Services;
using AngularProject.API.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Contacts;
using AutoMapper;
using Application.Common;
using Microsoft.AspNetCore.Authorization;

namespace AngularProject.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IContactService _service;
    private readonly IMapper _mapper;

    public ContactsController(
    IContactService service,
    IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // GET: api/contacts
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var contacts = await _service.GetAllAsync();

        var result = _mapper.Map<IEnumerable<ContactReadDto>>(contacts);

        return Ok(result);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged(
    int page = 1,
    int pageSize = 10,
    string? sortBy = "Id",
    string order = "asc")
    {
        if (page <= 0 || pageSize <= 0)
            return BadRequest("Page and PageSize must be greater than 0.");

        var ascending = order.ToLower() == "asc";

        var result = await _service.GetPagedAsync(
            page,
            pageSize,
            sortBy,
            ascending);

        var dtoResult = new PagedResult<ContactReadDto>
        {
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount,
            Items = _mapper.Map<List<ContactReadDto>>(result.Items)
        };

        return Ok(dtoResult);
    }

    // GET: api/contacts/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var contact = await _service.GetByIdAsync(id);

        if (contact == null)
            return NotFound();

        var dto = _mapper.Map<ContactReadDto>(contact);

        return Ok(dto);
    }

    // POST: api/contacts
    [HttpPost]
    public async Task<IActionResult> Create(ContactCreateDto dto)
    {
        var contact = _mapper.Map<Contact>(dto);

        await _service.CreateAsync(contact);

        return Ok();
    }

    // PUT: api/contacts/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ContactUpdateDto dto)
    {
        var existing = await _service.GetByIdAsync(id);

        if (existing == null)
            return NotFound($"Contact with ID {id} not found.");

        _mapper.Map(dto, existing);

        existing.UpdatedAt = DateTime.UtcNow;

        var updated = await _service.UpdateAsync(id, existing);

        if (!updated)
            return NotFound($"Contact with ID {id} not found.");

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