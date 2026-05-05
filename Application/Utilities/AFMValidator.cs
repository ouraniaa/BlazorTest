using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Utilities;

public static class AFMValidator
{
    public static string? ValidateVatNumber(string? vat)
    {
        if (string.IsNullOrWhiteSpace(vat))
            return "Το ΑΦΜ είναι υποχρεωτικό.";

        if (vat.Length != 9 || !vat.All(char.IsDigit))
            return "Το ΑΦΜ πρέπει να αποτελείται από 9 ψηφία.";

        int sum = 0;
        for (int i = 0; i < 8; i++)
        {
            sum += (int)char.GetNumericValue(vat[i]) << (8 - i);
        }

        int remainder = sum % 11;
        int checkDigit = remainder % 10;

        if (checkDigit != (int)char.GetNumericValue(vat[8]))
            return "Το ΑΦΜ δεν είναι έγκυρο.";

        return null;
    }
}