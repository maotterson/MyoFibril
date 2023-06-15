namespace MyoFibril.Domain.Utils;

using MyoFibril.Domain.Common;
using System.Net.Mail;
public static class CredentialsValidator
{

    public static bool ValidateUsername(string username)
    {
        if (string.IsNullOrEmpty(username)) return false;
        if (username.Length < CredentialsRules.MAX_USERNAME_LENGTH) return false;
        if (username.Length > CredentialsRules.MAX_USERNAME_LENGTH) return false;
        return true;
    }

    public static bool ValidatePasswordAndConfirmPassword(string password, string confirmPassword)
    {
        if (string.IsNullOrEmpty(password)) return false;
        if (password.Length < CredentialsRules.MIN_PASSWORD_LENGTH) return false;
        if (password.Length > CredentialsRules.MAX_PASSWORD_LENGTH) return false;
        if (password != confirmPassword) return false;
        return true;
    }

    public static bool ValidateEmail(string email)
    {
        if (string.IsNullOrEmpty(email)) return false;
        if (!IsValidEmail(email)) return false;
        return true;
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