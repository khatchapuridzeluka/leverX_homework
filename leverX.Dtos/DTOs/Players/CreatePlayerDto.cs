using FluentValidation;
using leverX.Dtos.DTOs.Players;

namespace leverX.DTOs.Players
{
    public record CreatePlayerDto : BasePlayerDto
    {
        public class Validator : AbstractValidator<CreatePlayerDto>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
              .NotEmpty().WithMessage("Name is required.")
              .MaximumLength(50);

                RuleFor(x => x.LastName)
                    .NotEmpty().WithMessage("Last name is required.")
                    .MaximumLength(50);

                RuleFor(x => x.FideRating)
                    .InclusiveBetween(100, 3000).WithMessage("FIDE rating must be between 100 and 3000.");

                RuleFor(x => x.Nationality)
                    .IsInEnum().WithMessage("Invalid nationality.");

                RuleFor(x => x.Sex)
                    .IsInEnum().WithMessage("Invalid sex.");

                RuleFor(x => x.Title)
                    .IsInEnum().WithMessage("Invalid title.");
            }
        }
    }

}
