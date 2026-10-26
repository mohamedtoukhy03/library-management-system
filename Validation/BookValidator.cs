namespace LibraryManagementSystem.Validation;

public class BookValidator : IBookValidator
{
    public ValidationResult Validate(string title, string author)
    {
        if (string.IsNullOrWhiteSpace(title))
            return ValidationResult.Failure("Book title cannot be empty or whitespace.");

        if (string.IsNullOrWhiteSpace(author))
            return ValidationResult.Failure("Author name cannot be empty or whitespace.");

        return ValidationResult.Success();
    }
}

