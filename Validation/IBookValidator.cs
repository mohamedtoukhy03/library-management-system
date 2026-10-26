namespace LibraryManagementSystem.Validation;

public interface IBookValidator
{
    ValidationResult Validate(string title, string author);
}

