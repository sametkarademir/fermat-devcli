using Fermat.DevCli.Password.Models;

namespace Fermat.DevCli.Password.Interfaces;

/// <summary>
/// Interface for password generation service.
/// </summary>
/// <remarks>
/// This interface defines a contract for generating passwords based on specified options.
///</remarks>
public interface IPasswordGeneratorService
{
    /// <summary>
    /// Generates a password based on the provided options.
    /// </summary>
    /// <param name="options">The options to use for generating the password.</param>
    /// <returns>A string representing the generated password.</returns>
    /// <remarks>
    /// This method takes a <see cref="PasswordOptions"/> object that specifies the criteria for the password,
    /// such as length and character types. It returns a string that meets the specified criteria.
    /// </remarks>
    Task<string> GeneratePassword(PasswordOptions options);
}