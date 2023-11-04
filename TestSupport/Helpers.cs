using Microsoft.Playwright;

namespace SendEmailProject.TestSupport
{
    public static class Helpers
    {
        public static async Task<string> TakeScreenshot(IPage page)
        {
            var date = DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss");
            var title = await page.TitleAsync();
            var path = $"Screenshots/{date}_{title}.png";
            var so = new PageScreenshotOptions()
            {
                Path = path,
                FullPage = true,
            };
            await page.ScreenshotAsync(so);

            return path;
        }
    }
}