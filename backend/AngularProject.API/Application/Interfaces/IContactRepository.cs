using AngularProject.API.Domain.Entities;
using Application.Common;

namespace AngularProject.API.Application.Interfaces;

public interface IContactRepository
{
    Task<List<Contact>> GetAllAsync();

    Task<Contact?> GetByIdAsync(int id);

    Task AddAsync(Contact contact);

    Task UpdateAsync(Contact contact);

    Task DeleteAsync(Contact contact);

    Task<bool> ExistsAsync(int id);

    Task<PagedResult<Contact>> GetPagedAsync(
        int page,
        int pageSize,
        string? sortBy,
        bool ascending);

    Task<bool> EmailExistsAsync(string email);
}