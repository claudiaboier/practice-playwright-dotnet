using Microsoft.Playwright;
using PlaywrightDemo.Config;

public class PlaywrightDriverInitializer
{
    private const float DEFAULT_TIMEOUT = 30f;
    public async Task<IBrowser> GetChromeDriverAsync(TestSettings testSettings)
    {
        var options = GetParameters(testSettings.Args, testSettings.Timeout, testSettings.Headless, testSettings.SlowMo);
        
        return await GetBrowserAsync(DriverType.Chromium, options);
    }
    public async Task<IBrowser> GetFirefoxDriverAsync(TestSettings testSettings)
    {
        var options = GetParameters(testSettings.Args, testSettings.Timeout, testSettings.Headless, testSettings.SlowMo);
        
        return await GetBrowserAsync(DriverType.Firefox, options);
    }
    private async Task<IBrowser> GetBrowserAsync(DriverType driverType, BrowserTypeLaunchOptions options)
    {
        var playwright = await Playwright.CreateAsync();
        return await playwright[driverType.ToString().ToLower()].LaunchAsync(options);
    }

    private BrowserTypeLaunchOptions GetParameters(string[]? args, float? timeout = DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null)
    {
        return new BrowserTypeLaunchOptions
        {
            Args = args,
            Timeout = ToMilliseconds(timeout),
            Headless = headless,
            SlowMo = slowmo
        };
    }

    private static float? ToMilliseconds(float? seconds)
    {
        return seconds * 1000;
    }
}