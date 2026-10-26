namespace LibraryManagementSystem.Validation;

using LibraryManagementSystem.Model;

public class MemberValidator : IMemberValidator
{
    public ValidationResult Validate(string name, MemberType type)
    {
        if (string.IsNullOrWhiteSpace(name))
            return ValidationResult.Failure("Member name cannot be empty or whitespace.");

        return ValidationResult.Success();
    }
}

