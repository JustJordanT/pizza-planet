using FluentValidation;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Commons.Validators;

public class PostLoginValidation : AbstractValidator<LoginCustomer>
{
    public PostLoginValidation()
    {
        RuleFor(c => c.Email).EmailAddress().WithMessage("Must be a valid email address");
        RuleFor(c => c.Password).NotEmpty().WithMessage("Must be a valid password, or not empty");
    }
}