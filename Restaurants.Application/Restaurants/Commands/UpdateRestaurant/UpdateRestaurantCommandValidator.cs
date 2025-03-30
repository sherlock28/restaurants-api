using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
{
	public UpdateRestaurantCommandValidator()
	{
		RuleFor(dto => dto.Name)
			.Length(3, 100)
			.WithMessage("Name field must be between 3 and 100 characters.");

		RuleFor(dto => dto.Description)
			.NotEmpty().WithMessage("Description is required.");
	}
}
