using FluentValidation;
using Domain;

public class WatchListValidator : AbstractValidator<WatchListItemDetails>
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
        RuleFor(itemDetails => itemDetails.mostRecentSnapshot).NotEmpty();
        When(itemDetails => itemDetails.mostRecentSnapshot != null, () => {
            RuleFor(itemDetails => itemDetails.mostRecentSnapshot).NotEmpty();
            RuleFor(itemDetails => itemDetails.mostRecentSnapshot.high).NotEmpty();
            RuleFor(itemDetails => itemDetails.mostRecentSnapshot.highTime).NotEmpty();
            RuleFor(itemDetails => itemDetails.mostRecentSnapshot.Id).NotEmpty();
            RuleFor(itemDetails => itemDetails.mostRecentSnapshot.low).NotEmpty();
            RuleFor(itemDetails => itemDetails.mostRecentSnapshot.lowTime).NotEmpty();
        });
    }
}