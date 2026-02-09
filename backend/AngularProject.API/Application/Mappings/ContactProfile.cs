using AutoMapper;
using Application.DTOs.Contacts;
using AngularProject.API.Domain.Entities;

namespace Application.Mappings;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<Contact, ContactReadDto>();
        
        CreateMap<ContactCreateDto, Contact>();

        CreateMap<ContactUpdateDto, Contact>();
    }
}
