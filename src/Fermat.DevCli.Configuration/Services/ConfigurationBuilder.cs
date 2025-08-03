using Fermat.DevCli.Configuration.Constans;
using Fermat.DevCli.Configuration.Interfaces;
using Fermat.DevCli.Configuration.Services.Strategies;

namespace Fermat.DevCli.Configuration.Services;

public class ConfigurationBuilder
{
    private readonly Dictionary<string, IConfigurationStrategy> _handlers = new()
    {
        { ConfigurationConsts.PasswordLength, new LengthStrategy() },
        { ConfigurationConsts.PasswordIncludeUppercase, new IncludeUppercaseStrategy() },
        { ConfigurationConsts.PasswordUppercaseChars, new UppercaseCharsStrategy() },
        { ConfigurationConsts.PasswordIncludeLowercase, new IncludeLowercaseStrategy() },
        { ConfigurationConsts.PasswordLowercaseChars, new LowercaseCharsStrategy() },
        { ConfigurationConsts.PasswordIncludeNumbers, new IncludeNumbersStrategy() },
        { ConfigurationConsts.PasswordNumberChars, new NumberCharsStrategy() },
        { ConfigurationConsts.PasswordIncludeSpecialCharacters, new IncludeSpecialCharactersStrategy() },
        { ConfigurationConsts.PasswordSpecialChars, new SpecialCharsStrategy() },
        { ConfigurationConsts.PasswordExcludeChars, new ExcludeCharsStrategy() },
    };

    public IConfigurationStrategy GetStrategy(string key)
    {
        if (_handlers.TryGetValue(key, out var strategy))
        {
            return strategy;
        }

        throw new KeyNotFoundException($"No configuration strategy found for key: {key}");
    }

    public async Task SetHandlerAsync(string key, string value)
    {
        if (_handlers.TryGetValue(key, out var strategy))
        {
            await strategy.SetHandlerAsync(key, value);
        }
        else
        {
            throw new KeyNotFoundException($"No configuration strategy found for key: {key}");
        }
    }

    public async Task<T> GetHandlerAsync<T>(string key)
    {
        if (_handlers.TryGetValue(key, out var strategy))
        {
            return await strategy.GetHandlerAsync<T>(key);
        }

        throw new KeyNotFoundException($"No configuration strategy found for key: {key}");
    }
}