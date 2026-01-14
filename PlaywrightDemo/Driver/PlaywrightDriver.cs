using Microsoft.Playwright;
using PlaywrightDemo.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightDemo.Driver
{
    public class PlaywrightDriver
    {
        
        private readonly Task<IBrowser> _browser;
        private readonly Task<IBrowserContext> _browserContext;
        private readonly TestSettings _testSettings;

        private readonly Task<IPage> _page;

        public PlaywrightDriver(TestSettings testSettings)
        {
            _testSettings = testSettings;

            PlaywrightDriverInitializer playwrightDriverInitializer = new PlaywrightDriverInitializer();

            _browser = Task.Run(() => InitalizePlaywrightAsync());
            _browserContext = Task.Run(() => CreateBrowserContextAsync());
            _page = Task.Run(() => CreatePageAsync());
        }

        public IPage Page => _page.Result;
        public IBrowser Browser => _browser.Result;
        public IBrowserContext BrowserContext => _browserContext.Result;

        private async Task<IBrowser> InitalizePlaywrightAsync()
        {

            return await GetBrowserAsync(_testSettings);
        }

        private async Task<IBrowserContext> CreateBrowserContextAsync()
        {
            return await Browser.NewContextAsync();
        }

        private async Task<IPage> CreatePageAsync()
        {
            return await BrowserContext.NewPageAsync();
        }

        private async Task<IBrowser> GetBrowserAsync(TestSettings testSettings)
        {
            var playwrightDriver = await Playwright.CreateAsync();

            var browserOption = new BrowserTypeLaunchOptions
            {
                Headless = testSettings.Headless,
                Devtools = testSettings.DevTools,
                SlowMo = testSettings.SlowMo,
                // Channel = testSettings.Channel
            };

            return testSettings.DriverType switch
            {
                // DriverType.Chromium => await playwrightDriver.Chromium.LaunchAsync(browserOption),
                // DriverType.Chrome => await playwrightDriver["chrome"].LaunchAsync(browserOption),
                // DriverType.Edge => await playwrightDriver.Chromium.LaunchAsync(browserOption),
                DriverType.Firefox => await playwrightDriver.Firefox.LaunchAsync(browserOption),
                _ => await playwrightDriver.Chromium.LaunchAsync(browserOption)
            };
        }
    }
}
