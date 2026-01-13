using Microsoft.Playwright;
using PlaywrightDemo.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightDemo.Driver
{
    public class PlaywrightDriver
    {



        public async Task<IPage> InitalizePlaywrightAsync(TestSettings testSettings)
        {

            var browser = await GetBrowserAsync(testSettings);
            var browserContext = await browser.NewContextAsync();
            var page = await browserContext.NewPageAsync();

            await page.GotoAsync("http://eaapp.somee.com");

            return page;
        }

        public async Task<IBrowser> GetBrowserAsync(TestSettings testSettings)
        {
            var playwrightDriver = await Playwright.CreateAsync();

            var browserOption = new BrowserTypeLaunchOptions
            {
                Headless = testSettings.Headless,
                Devtools = testSettings.DevTools,
                SlowMo = testSettings.SlowMo,
                Channel = testSettings.Channel
            };

            return testSettings.DriverType switch
            {
                DriverType.Chromium => await playwrightDriver.Chromium.LaunchAsync(browserOption),
                DriverType.Chrome => await playwrightDriver["chrome"].LaunchAsync(browserOption),
                DriverType.Edge => await playwrightDriver.Chromium.LaunchAsync(browserOption),
                DriverType.Firefox => await playwrightDriver.Firefox.LaunchAsync(browserOption),
                _ => await playwrightDriver.Chromium.LaunchAsync(browserOption)
            };
        }
    }
}
