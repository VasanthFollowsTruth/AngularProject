using AngularProject.API.Application.Interfaces;
using AngularProject.API.Domain.Entities;
using AngularProject.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Common;

namespace AngularProject.API.Infrastructure.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly AppDbContext _context;

    public ContactRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Contact>> GetAllAsync()
    {
        return await _context.Contacts
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Contact?> GetByIdAsync(int id)
    {
        return await _context.Contacts
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Contact contact)
    {
        await _context.Contacts.AddAsync(contact);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Contact contact)
    {
        _context.Contacts.Update(contact);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Contact contact)
    {
        contact.IsDeleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Contacts
            .AnyAsync(x => x.Id == id);
    }

    public async Task<PagedResult<Contact>> GetPagedAsync(
    int page,
    int pageSize,
    string? sortBy,
    bool ascending)
    {
        var query = _context.Contacts.AsQueryable();

        // Sorting
        query = sortBy?.ToLower() switch
        {
            "firstname" => ascending
                ? query.OrderBy(x => x.FirstName)
                : query.OrderByDescending(x => x.FirstName),

            "lastname" => ascending
                ? query.OrderBy(x => x.LastName)
                : query.OrderByDescending(x => x.LastName),

            "email" => ascending
                ? query.OrderBy(x => x.Email)
                : query.OrderByDescending(x => x.Email),

            "phonenumber" => ascending
                ? query.OrderBy(x => x.PhoneNumber)
                : query.OrderByDescending(x => x.PhoneNumber),

            "address" => ascending
                ? query.OrderBy(x => x.Address)
                : query.OrderByDescending(x => x.Address),

            "city" => ascending
                ? query.OrderBy(x => x.City)
                : query.OrderByDescending(x => x.City),

            "state" => ascending
                ? query.OrderBy(x => x.State)
                : query.OrderByDescending(x => x.State),

            "country" => ascending
                ? query.OrderBy(x => x.Country)
                : query.OrderByDescending(x => x.Country),

            "postalcode" => ascending
                ? query.OrderBy(x => x.PostalCode)
                : query.OrderByDescending(x => x.PostalCode),

            // Default: newest first
            _ => query.OrderByDescending(x => x.CreatedAt)
        };

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Contact>
        {
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            Items = items
        };
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Contacts
            .AnyAsync(x => x.Email == email);
    }
}