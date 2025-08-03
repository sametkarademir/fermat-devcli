namespace Fermat.DevCli.Configuration.Models;

public class PasswordConfiguration
{
    public int Length { get; set; } = 16;

    public bool IncludeUppercase { get; set; } = true;
    public string UppercaseChars { get; set; } = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public bool IncludeLowercase { get; set; } = true;
    public string LowercaseChars { get; set; } = "abcdefghijklmnopqrstuvwxyz";

    public bool IncludeNumbers { get; set; } = true;
    public string NumberChars { get; set; } = "0123456789";

    public bool IncludeSpecialCharacters { get; set; } = true;
    public string SpecialChars { get; set; } = "!@#$%^&*()-_=+[]{}|;:,.<>?/";
    public string ExcludeChars { get; set; } = "0O1lI";
}