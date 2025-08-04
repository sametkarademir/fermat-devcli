using Fermat.DevCli.Password.Enums;

namespace Fermat.DevCli.Password.Models;

public class PasswordOptions
{
    /// <summary>
    /// Type of password to generate
    /// </summary>
    public PasswordOptionTypes Type { get; set; } = PasswordOptionTypes.Default;

    /// <summary>
    /// Password length
    /// </summary>
    public int Length { get; set; } = 16;

    /// <summary>
    /// Uppercase (A-Z)
    /// </summary>
    public bool IncludeUppercase { get; set; } = true;

    /// <summary>
    /// Lowercase (a-z)
    /// </summary>
    public bool IncludeLowercase { get; set; } = true;

    /// <summary>
    /// Numbers (0-9)
    /// </summary>
    public bool IncludeNumbers { get; set; } = true;

    /// <summary>
    /// Special Characters (!, @, #, $, vb.)
    /// </summary>
    public bool IncludeSpecialCharacters { get; set; } = true;
}