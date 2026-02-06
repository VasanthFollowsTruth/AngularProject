using Application.DTOs.Contacts;
using FluentValidation;

namespace Application.Validators.Contacts;

public class ContactCreateValidator
    : ContactBaseValidator<ContactCreateDto>
{
    public ContactCreateValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        AddCommonRules(
            x => x.FirstName,
            x => x.LastName,
            x => x.Email,
            x => x.PhoneNumber,
            x => x.Address,
            x => x.City,
            x => x.State,
            x => x.Country,
            x => x.PostalCode
        );
    }
}
