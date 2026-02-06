using FluentValidation;
using System.Linq.Expressions;

namespace Application.Validators.Contacts;

public abstract class ContactBaseValidator<T>
    : AbstractValidator<T>
{
    protected void AddCommonRules(
        Expression<Func<T, string>> firstName,
        Expression<Func<T, string>> lastName,
        Expression<Func<T, string>> email,
        Expression<Func<T, string>> phoneNumber,
        Expression<Func<T, string>> address,
        Expression<Func<T, string>> city,
        Expression<Func<T, string>> state,
        Expression<Func<T, string>> country,
        Expression<Func<T, string>> postalCode)
    {
        RuleFor(firstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(lastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(email)
            .NotEmpty()
            .MaximumLength(150)
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            .WithMessage("Email must be a valid email address.");

        RuleFor(phoneNumber)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(address)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(city)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(state)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(country)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(postalCode)
            .NotEmpty()
            .MaximumLength(20);
    }
}
