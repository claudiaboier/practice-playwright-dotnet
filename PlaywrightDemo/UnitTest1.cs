using Microsoft.Playwright;
using PlaywrightDemo.Config;
using PlaywrightDemo.Driver;

namespace PlaywrightDemo
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            TestSettings testSettings = new TestSettings
            {
                Channel = "msedge",
                DevTools = true,
                Headless = false,
                SlowMo = 1500,
                DriverType = DriverType.Edge
            };
          

            PlaywrightDriver driver = new PlaywrightDriver();
            var page = await driver.InitalizePlaywrightAsync(testSettings);

            await page.ClickAsync("text=Login");

        }

        [Test]
        public async Task LaunchingBrowserInAnotherOption()
        {
            var playwrightDriver = await Playwright.CreateAsync();

            var browserOption = new BrowserTypeLaunchOptions();
            browserOption.Headless = false;
            browserOption.Devtools = true;
            browserOption.Channel = "chrome";

            var chromium = await playwrightDriver["firefox"].LaunchAsync(browserOption);
            var browserContext = await chromium.NewContextAsync();
            var page = await browserContext.NewPageAsync();

            await page.GotoAsync("http://eaapp.somee.com");

        }
    }
}