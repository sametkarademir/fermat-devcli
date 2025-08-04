using System.Text;
using System.Text.Json;
using Fermat.DevCli.Configuration.Constans;
using Fermat.DevCli.Configuration.Models;
using Fermat.DevCli.Password.Enums;
using Fermat.DevCli.Password.Interfaces;
using Fermat.DevCli.Password.Models;
using Fermat.DevCli.Shared.Interfaces;

namespace Fermat.DevCli.Password.Services;

public class PasswordGeneratorService(
    IRandomProvider randomProvider,
    IResourceAppService resourceAppService)
    : IPasswordGeneratorService
{
    public async Task<string> GeneratePassword(PasswordOptions options)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        if (options.Length <= 0)
        {
            throw new ArgumentException("Password length must be a positive number.", nameof(options.Length));
        }

        if (options is { IncludeUppercase: false, IncludeLowercase: false, IncludeNumbers: false, IncludeSpecialCharacters: false })
        {
            throw new ArgumentException("At least one character set must be selected.");
        }

        var passwordConfiguration = await resourceAppService.ReadConfiguration<PasswordConfiguration>
        (
            ConfigurationConsts.DirectoryPath,
            ConfigurationConsts.PasswordConfigFileName
        );

        if (passwordConfiguration == null)
        {
            throw new InvalidOperationException("Password configuration is not set up correctly.");
        }

        return options.Type switch
        {
            PasswordOptionTypes.Easy => GenerateEasyPassword(passwordConfiguration),
            PasswordOptionTypes.Medium => GenerateMediumPassword(passwordConfiguration),
            PasswordOptionTypes.Hard => GenerateHardPassword(passwordConfiguration),
            PasswordOptionTypes.Default => GenerateCustomPassword(passwordConfiguration, options),
            _ => throw new ArgumentOutOfRangeException(nameof(options.Type), $"Unsupported password type: {options.Type}")
        };
    }

    private string GenerateEasyPassword(PasswordConfiguration passwordConfiguration)
    {
        var charSets = new List<string>();
        charSets.Add(passwordConfiguration.UppercaseChars);
        charSets.Add(passwordConfiguration.LowercaseChars);
        charSets.Add(passwordConfiguration.NumberChars);

        charSets = charSets.Select(charSet => new string(charSet.Except(passwordConfiguration.ExcludeChars).ToArray())).ToList();

        var allChars = string.Join("", charSets);

        var password = new StringBuilder();
        foreach (var charSet in charSets)
        {
            password.Append(charSet[randomProvider.GetRandomInt(0, charSet.Length)]);
        }

        for (int i = password.Length; i < 8; i++)
        {
            password.Append(allChars[randomProvider.GetRandomInt(0, allChars.Length)]);
        }

        var passwordChars = password.ToString().ToCharArray();
        for (var i = passwordChars.Length - 1; i > 0; i--)
        {
            var j = randomProvider.GetRandomInt(0, i + 1);
            (passwordChars[i], passwordChars[j]) = (passwordChars[j], passwordChars[i]);
        }

        return new string(passwordChars);
    }

    private string GenerateMediumPassword(PasswordConfiguration passwordConfiguration)
    {
        var charSets = new List<string>();
        charSets.Add(passwordConfiguration.UppercaseChars);
        charSets.Add(passwordConfiguration.LowercaseChars);
        charSets.Add(passwordConfiguration.NumberChars);
        charSets.Add(passwordConfiguration.SpecialChars);

        charSets = charSets.Select(charSet => new string(charSet.Except(passwordConfiguration.ExcludeChars).ToArray())).ToList();

        var allChars = string.Join("", charSets);

        var password = new StringBuilder();
        foreach (var charSet in charSets)
        {
            password.Append(charSet[randomProvider.GetRandomInt(0, charSet.Length)]);
        }

        for (int i = password.Length; i < 12; i++)
        {
            password.Append(allChars[randomProvider.GetRandomInt(0, allChars.Length)]);
        }

        var passwordChars = password.ToString().ToCharArray();
        for (var i = passwordChars.Length - 1; i > 0; i--)
        {
            var j = randomProvider.GetRandomInt(0, i + 1);
            (passwordChars[i], passwordChars[j]) = (passwordChars[j], passwordChars[i]);
        }

        return new string(passwordChars);
    }

    private string GenerateHardPassword(PasswordConfiguration passwordConfiguration)
    {
        var charSets = new List<string>();
        charSets.Add(passwordConfiguration.UppercaseChars);
        charSets.Add(passwordConfiguration.LowercaseChars);
        charSets.Add(passwordConfiguration.NumberChars);
        charSets.Add(passwordConfiguration.SpecialChars);

        charSets = charSets.Select(charSet => new string(charSet.Except(passwordConfiguration.ExcludeChars).ToArray())).ToList();

        var allChars = string.Join("", charSets);

        var password = new StringBuilder();
        foreach (var charSet in charSets)
        {
            password.Append(charSet[randomProvider.GetRandomInt(0, charSet.Length)]);
        }

        for (var i = password.Length; i < 16; i++)
        {
            password.Append(allChars[randomProvider.GetRandomInt(0, allChars.Length)]);
        }

        var passwordChars = password.ToString().ToCharArray();
        for (var i = passwordChars.Length - 1; i > 0; i--)
        {
            var j = randomProvider.GetRandomInt(0, i + 1);
            (passwordChars[i], passwordChars[j]) = (passwordChars[j], passwordChars[i]);
        }

        return new string(passwordChars);
    }

    private string GenerateCustomPassword(PasswordConfiguration passwordConfiguration, PasswordOptions options)
    {
        var charSets = new List<string>();
        if (options.IncludeUppercase) charSets.Add(passwordConfiguration.UppercaseChars);
        if (options.IncludeLowercase) charSets.Add(passwordConfiguration.LowercaseChars);
        if (options.IncludeNumbers) charSets.Add(passwordConfiguration.NumberChars);
        if (options.IncludeSpecialCharacters) charSets.Add(passwordConfiguration.SpecialChars);

        charSets = charSets.Select(charSet => new string(charSet.Except(passwordConfiguration.ExcludeChars).ToArray())).ToList();

        var allChars = string.Join("", charSets);

        var password = new StringBuilder();
        foreach (var charSet in charSets)
        {
            password.Append(charSet[randomProvider.GetRandomInt(0, charSet.Length)]);
        }

        for (var i = password.Length; i < options.Length; i++)
        {
            password.Append(allChars[randomProvider.GetRandomInt(0, allChars.Length)]);
        }

        var passwordChars = password.ToString().ToCharArray();
        for (var i = passwordChars.Length - 1; i > 0; i--)
        {
            var j = randomProvider.GetRandomInt(0, i + 1);
            (passwordChars[i], passwordChars[j]) = (passwordChars[j], passwordChars[i]);
        }

        return new string(passwordChars);
    }
}