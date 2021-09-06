using FluentValidation;
using Domain;

public class WatchListValidator : AbstractValidator<ItemDetails>
{
    public WatchListValidator()
    {
        RuleFor(itemDetails => itemDetails.examine).NotEmpty();
        RuleFor(itemDetails => itemDetails.highalch).NotEmpty();
        RuleFor(itemDetails => itemDetails.icon).NotEmpty();
        RuleFor(itemDetails => itemDetails.Id).NotEmpty();
        RuleFor(itemDetails => itemDetails.limit).NotEmpty();
        RuleFor(itemDetails => itemDetails.lowalch).NotEmpty();
        RuleFor(itemDetails => itemDetails.members).NotEmpty();
        RuleFor(itemDetails => itemDetails.name).NotEmpty();
        RuleFor(itemDetails => itemDetails.value).NotEmpty();
    }
}