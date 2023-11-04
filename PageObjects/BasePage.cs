using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace SendEmailProject.PageObjects;

public class BasePage : PageTest
{
    protected new IPage Page { get; private set; }

    protected BasePage(IPage page)
    {
        Page = page;
    }
}