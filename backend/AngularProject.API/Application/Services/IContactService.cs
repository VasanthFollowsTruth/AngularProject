using AngularProject.API.Domain.Entities;

namespace AngularProject.API.Application.Services
{
    public interface IContactService
    {
        Task<List<Contact>> GetAllAsync();

        Task<Contact?> GetByIdAsync(int id);

        Task<Contact> CreateAsync(Contact contact);

        Task<bool> UpdateAsync(int id, Contact contact);

        Task<bool> DeleteAsync(int id);
    }
}

