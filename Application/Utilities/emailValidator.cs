using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Utilities;

public static class EmailValidator
{
    public static string? ValidateEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return "Το Email είναι υποχρεωτικό.";

        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        if (!Regex.IsMatch(email, emailPattern))
            return "Η διεύθυνση Email δεν είναι έγκυρη (π.χ. user@example.com).";

        return null;
    }
}