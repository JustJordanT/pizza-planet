using FluentValidation;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Commons.Validators;

public class PostPizzaRequestValidation : AbstractValidator<CreatePizzaModel>
{
    public PostPizzaRequestValidation()
    {
        RuleFor(p => p.Size).NotEmpty().WithMessage("Please ensure that you have entered a valid size");
        RuleFor(p => p.CrustType).NotEmpty().WithMessage("Please ensure that you have entered a valid crust type");;
        RuleFor(p => p.Toppings).NotEmpty().WithMessage("Please ensure that you have added toppings to this collection");
        RuleFor(p => p.Quantity).GreaterThan(0).LessThan(100);
    }
}