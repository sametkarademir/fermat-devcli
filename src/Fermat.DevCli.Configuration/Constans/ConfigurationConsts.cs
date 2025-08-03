namespace Fermat.DevCli.Configuration.Constans;

public static class ConfigurationConsts
{
    public const string DirectoryPath = "config";
    public const string PasswordConfigFileName = "password_config.json";

    //Password Configuration Keys
    private const string PasswordPrefix = "password.";

    public const string PasswordLength = PasswordPrefix + "length";
    public const string PasswordIncludeUppercase = PasswordPrefix + "includeUppercase";
    public const string PasswordUppercaseChars = PasswordPrefix + "uppercaseChars";
    public const string PasswordIncludeLowercase = PasswordPrefix + "includeLowercase";
    public const string PasswordLowercaseChars = PasswordPrefix + "lowercaseChars";
    public const string PasswordIncludeNumbers = PasswordPrefix + "includeNumbers";
    public const string PasswordNumberChars = PasswordPrefix + "numberChars";
    public const string PasswordIncludeSpecialCharacters = PasswordPrefix + "includeSpecialCharacters";
    public const string PasswordSpecialChars = PasswordPrefix + "specialChars";
    public const string PasswordExcludeChars = PasswordPrefix + "excludeChars";

    public static string[] AllKeys = new[]
    {
        PasswordLength,
        PasswordIncludeUppercase,
        PasswordUppercaseChars,
        PasswordIncludeLowercase,
        PasswordLowercaseChars,
        PasswordIncludeNumbers,
        PasswordNumberChars,
        PasswordIncludeSpecialCharacters,
        PasswordSpecialChars,
        PasswordExcludeChars
    };

    // Validation
    public static bool IsValidKey(string key)
    {
        return AllKeys.Contains(key);
    }
}