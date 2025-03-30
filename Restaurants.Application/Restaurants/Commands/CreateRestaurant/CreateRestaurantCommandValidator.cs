using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
	private readonly string[] validCategories = { "Fast Food", "Italian", "Chinese", "Mexican", "American", "Japanese", "Indian", "Thai", "Greek", "French", "Vegetarian", "Vegan", "Other" };

	public CreateRestaurantCommandValidator()
	{
		RuleFor(dto => dto.Name)
			.Length(3, 100)
			.WithMessage("Name field must be between 3 and 100 characters.");

		RuleFor(dto => dto.Description)
			.NotEmpty().WithMessage("Description is required.");

		RuleFor(dto => dto.Category)
			.Custom((category, context) =>
			{
				if (!validCategories.Contains(category))
				{
					context.AddFailure("Category", "Invalid category: Please choose from the valid categories.");
				}
			});

		RuleFor(dto => dto.ContactEmail)
			.EmailAddress().WithMessage("Please provide a valid email address.");

		RuleFor(dto => dto.ContactNumber)
			.Matches(@"^\+?[0-9]{3}-?[0-9]{6,12}$")
			.WithMessage("Invalid phone number.");

		RuleFor(dto => dto.PostalCode)
			.Matches(@"^\d{2}-\d{3}$")
			.WithMessage("Please provide a valid postal code (XX-XXX).");
	}
}
