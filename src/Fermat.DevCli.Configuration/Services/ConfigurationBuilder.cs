using Fermat.DevCli.Configuration.Constans;
using Fermat.DevCli.Configuration.Interfaces;
using Fermat.DevCli.Configuration.Services.Strategies;
using Fermat.DevCli.Shared.Interfaces;

namespace Fermat.DevCli.Configuration.Services;

public class ConfigurationBuilder(IResourceAppService resourceAppService)
{
    private readonly Dictionary<string, IConfigurationStrategy> _handlers = new()
    {
        { ConfigurationConsts.PasswordLength, new LengthStrategy(resourceAppService) },
        { ConfigurationConsts.PasswordIncludeUppercase, new IncludeUppercaseStrategy(resourceAppService) },
        { ConfigurationConsts.PasswordUppercaseChars, new UppercaseCharsStrategy(resourceAppService) },
        { ConfigurationConsts.PasswordIncludeLowercase, new IncludeLowercaseStrategy(resourceAppService) },
        { ConfigurationConsts.PasswordLowercaseChars, new LowercaseCharsStrategy(resourceAppService) },
        { ConfigurationConsts.PasswordIncludeNumbers, new IncludeNumbersStrategy(resourceAppService) },
        { ConfigurationConsts.PasswordNumberChars, new NumberCharsStrategy(resourceAppService) },
        { ConfigurationConsts.PasswordIncludeSpecialCharacters, new IncludeSpecialCharactersStrategy(resourceAppService) },
        { ConfigurationConsts.PasswordSpecialChars, new SpecialCharsStrategy(resourceAppService) },
        { ConfigurationConsts.PasswordExcludeChars, new ExcludeCharsStrategy(resourceAppService) },
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

    public async Task<string> GetHandlerAsync(string key)
    {
        if (_handlers.TryGetValue(key, out var strategy))
        {
            return await strategy.GetHandlerAsync(key);
        }

        throw new KeyNotFoundException($"No configuration strategy found for key: {key}");
    }
}