using AngularProject.API.Application.Interfaces;
using AngularProject.API.Domain.Entities;
using Application.Common;

namespace AngularProject.API.Application.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _repository;

    public ContactService(IContactRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Contact>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Contact?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Contact> CreateAsync(Contact contact)
    {
        contact.CreatedAt = DateTime.UtcNow;

        if (await _repository.EmailExistsAsync(contact.Email))
        {
            throw new ArgumentException("Email already exists.");
        }

        await _repository.AddAsync(contact);

        return contact;
    }

    public async Task<bool> UpdateAsync(int id, Contact updated)
    {
        var existing = await _repository.GetByIdAsync(id);

        if (existing == null)
        {
            return false;
        }

        existing.FirstName = updated.FirstName;
        existing.LastName = updated.LastName;
        existing.Email = updated.Email;
        existing.PhoneNumber = updated.PhoneNumber;
        existing.Address = updated.Address;
        existing.City = updated.City;
        existing.State = updated.State;
        existing.Country = updated.Country;
        existing.PostalCode = updated.PostalCode;
        existing.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(existing);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var contact = await _repository.GetByIdAsync(id);

        if (contact == null)
            return false;

        await _repository.DeleteAsync(contact);

        return true;
    }

    public async Task<PagedResult<Contact>> GetPagedAsync(
    int page,
    int pageSize,
    string? sortBy,
    bool ascending)
    {
        return await _repository.GetPagedAsync(
            page,
            pageSize,
            sortBy,
            ascending);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _repository.EmailExistsAsync(email);
    }
}