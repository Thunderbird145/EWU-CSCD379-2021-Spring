using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlaywrightSharp;
using System.Linq;

namespace SecretSanta.Web.Tests
{
    [TestClass]
    public class EndToEndTests
    {
        private static WebHostServerFixture<Web.Startup, SecretSanta.Api.Startup> Server;

        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            Server = new();
        }

        [TestMethod]
        public async Task LaunchHomepage()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            var headerContent = await page.GetTextContentAsync("body > header > div > a");
            Assert.AreEqual("Secret Santa", headerContent);
        }

        [TestMethod]
        public async Task NavigateToUsers()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await page.ClickAsync("text=Users");

            await page.ScreenshotAsync("users.png");
        }

        [TestMethod]
        public async Task NavigateToGifts()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await page.ClickAsync("text=Gifts");

            await page.ScreenshotAsync("gifts.png");
        }

        [TestMethod]
        public async Task NavigateToGroups()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await page.ClickAsync("text=Groups");

            await page.ScreenshotAsync("groups.png");
        }

        [TestMethod]
        public async Task CreateGift() {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await page.ClickAsync("text=Gifts");

            var gifts = await page.QuerySelectorAllAsync("body > section > section > section");
            int giftCount = gifts.Count();

            await page.ClickAsync("text=Create");

            await page.TypeAsync("input#Title", "New Purse");
            await page.TypeAsync("input#Description", "A nice new purse to hold all of my rat poison");
            await page.TypeAsync("input#Url", "https://amazon.com");
            await page.TypeAsync("input#Priority", "1");
            await page.SelectOptionAsync("select#UserId", "1");

            await page.ClickAsync("text=Create");

            gifts = await page.QuerySelectorAllAsync("body > section > section > section");
            Assert.AreEqual(giftCount + 1, gifts.Count());

            await page.ScreenshotAsync("create a gift.png");
        }

        [TestMethod]
        public async Task EditGift() {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await page.ClickAsync("text=Gifts");

            await page.ClickAsync("text=Rat Poison");

            await page.ScreenshotAsync("edit a gift.png");
            
            await page.ClickAsync("input#Title", clickCount: 3);
            await page.TypeAsync("input#Title", "Different Rat Poison");

            await page.ClickAsync("input#Description", clickCount: 3);
            await page.TypeAsync("input#Description", "Could kill 30 rats");

            await page.ClickAsync("input#Url", clickCount: 3);
            await page.TypeAsync("input#Url", "https://RatPoisonSurpluss.com");

            await page.ClickAsync("input#Priority", clickCount: 3);
            await page.TypeAsync("input#Priority", "1");

            await page.SelectOptionAsync("select#UserId", "1");

            await page.ScreenshotAsync("editted a gift.png");

            await page.ClickAsync("text=Update");

            Assert.AreEqual("Different Rat Poison", await page.GetTextContentAsync("body > section > section > section:first-child > a > section > div "));
        }

        [TestMethod]
        public async Task DeleteGift()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = false,
                SlowMo = 120
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await page.ClickAsync("text=Gifts");

            // make sure we have 10 + 1 speakers here
            var gifts = await page.QuerySelectorAllAsync("body > section > section > section");
            int giftCount = gifts.Count();

            page.Dialog += (_, args) => args.Dialog.AcceptAsync();

            await page.ClickAsync("body > section > section > section:first-child > a > section form > button");

            // make sure we have 10 speakers here
            gifts = await page.QuerySelectorAllAsync("body > section > section > section");
            Assert.AreEqual(giftCount - 1, gifts.Count());
        }
    }
}