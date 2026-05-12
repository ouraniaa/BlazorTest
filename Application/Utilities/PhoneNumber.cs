namespace Application.Utilities;

public static class PhoneValidator
{
    public static string? ValidatePhoneNumber(string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return "Το τηλέφωνο είναι υποχρεωτικό.";

        var cleaned = phoneNumber.Trim();

        if (cleaned.Length != 10 || !cleaned.All(char.IsDigit))
            return "Το τηλέφωνο πρέπει να έχει 10 ψηφία.";

        if (!(cleaned.StartsWith("2") || cleaned.StartsWith("6") || cleaned.StartsWith("8")))
            return "Μη έγκυρο πρόθεμα τηλεφώνου.";

        return null;
    }
}