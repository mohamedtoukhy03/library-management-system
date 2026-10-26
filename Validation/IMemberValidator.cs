namespace LibraryManagementSystem.Validation;

using LibraryManagementSystem.Model;

public interface IMemberValidator
{
    ValidationResult Validate(string name, MemberType type);
}

