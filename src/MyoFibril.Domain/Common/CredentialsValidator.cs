namespace MyoFibril.Domain.Common;
using System.Net.Mail;
public static class CredentialsValidator
{
    private static string INVALID_USERNAME_LENGTH_ERROR = $"Invalid username. Username must be between {CredentialsRules.MIN_USERNAME_LENGTH} and {CredentialsRules.MAX_USERNAME_LENGTH} characters.";
    private static string INVALID_PASSWORD_LENGTH_ERROR = $"Invalid password. Password must be between {CredentialsRules.MIN_PASSWORD_LENGTH} and {CredentialsRules.MAX_PASSWORD_LENGTH} characters.";
    private static string INVALID_EMAIL_ERROR = $"Invalid email. Please enter a valid email address.";
    private static string INVALID_CONFIRM_PASSWORD = $"Invalid password. Password and confirm password do not match.";

    public static (bool isValid, string? errorMessage) ValidateUsername(string username)
    {
        if (string.IsNullOrEmpty(username)) return (false, INVALID_USERNAME_LENGTH_ERROR);
        if (username.Length < CredentialsRules.MIN_USERNAME_LENGTH) return (false, INVALID_USERNAME_LENGTH_ERROR);
        if (username.Length > CredentialsRules.MAX_USERNAME_LENGTH) return (false, INVALID_USERNAME_LENGTH_ERROR);
        return (true, null);
    }

    public static (bool isValid, string? errorMessage) ValidatePasswordAndConfirmPassword(string password, string confirmPassword)
    {
        if (string.IsNullOrEmpty(password)) return (false, INVALID_PASSWORD_LENGTH_ERROR);
        if (password.Length < CredentialsRules.MIN_PASSWORD_LENGTH) return (false, INVALID_PASSWORD_LENGTH_ERROR);
        if (password.Length > CredentialsRules.MAX_PASSWORD_LENGTH) return (false, INVALID_PASSWORD_LENGTH_ERROR);
        if (password != confirmPassword) return (false, INVALID_CONFIRM_PASSWORD);
        return (true, null);
    }

    public static (bool isValid, string? errorMessage) ValidateEmail(string email)
    {
        if (string.IsNullOrEmpty(email)) return (false, INVALID_EMAIL_ERROR);
        if (!IsValidEmail(email)) return (false, INVALID_EMAIL_ERROR);
        return (true, null);
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var mailAddress = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}