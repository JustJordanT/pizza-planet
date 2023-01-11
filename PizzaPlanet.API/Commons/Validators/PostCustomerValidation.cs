using FluentValidation;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Commons.Validators;

public class PostCustomerValidation : AbstractValidator<CreateCustomer>
{
    public PostCustomerValidation()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name must not be empty");
        RuleFor(c => c.Email).NotEmpty().EmailAddress().WithMessage("Must not be empty and must be a valid email address");
        RuleFor(c => c.Password).NotEmpty().WithMessage("Password must not be empty");
    }
}