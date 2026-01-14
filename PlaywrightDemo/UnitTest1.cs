using Microsoft.Playwright;
using PlaywrightDemo.Config;
using PlaywrightDemo.Driver;

namespace PlaywrightDemo
{
    public class Tests
    {
        private PlaywrightDriver _driver;
        
        [SetUp]
        public async Task Setup()
        {
            TestSettings testSettings = new TestSettings
            {
                // Channel = "chrome",
                DevTools = true,
                Headless = true,
                SlowMo = 1500,
                DriverType = DriverType.Firefox
            };
          
            _driver = new PlaywrightDriver(testSettings);
            await _driver.Page.GotoAsync("http://eaapp.somee.com");
        }

        [Test]
        public async Task Test1()
        {
            
            
            await _driver.Page.ClickAsync("text=Login");

        }

        [Test]
        public async Task LoginTest()
        {
            await _driver.Page.ClickAsync("text=Login");
            await _driver.Page.GetByLabel("Username").FillAsync("admin");
            await _driver.Page.GetByLabel("Password").FillAsync("password");


        }

        // [Test]
        // public async Task LaunchingBrowserInAnoBtherOption()
        // {
            

        //      // create Playwright
        //     using var playwright = await Playwright.CreateAsync();

        //     // launch browser (bundled Chromium)
        //     await using var browser = await playwright.Chromium.LaunchAsync(
        //         new BrowserTypeLaunchOptions
        //         {
        //             Headless = true
        //         });

        //     // create context + page
        //     var context = await browser.NewContextAsync();
        //     var page = await context.NewPageAsync();

        //     // navigate & assert
        //     await page.GotoAsync("https://example.com");
            

        // }

        [TearDown] 
        public async Task TearDown()
        {
            await _driver.Browser.CloseAsync();
            await _driver.Browser.DisposeAsync();
        }
    }
}