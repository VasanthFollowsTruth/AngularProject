using AutoMapper;
using Application.DTOs.Contacts;
using AngularProject.API.Domain.Entities;

namespace Application.Mappings;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
        // Entity → Read DTO
        CreateMap<Contact, ContactReadDto>();

        // Create DTO → Entity
        CreateMap<ContactCreateDto, Contact>();

        // Update DTO → Entity
        CreateMap<ContactUpdateDto, Contact>();
    }
}
