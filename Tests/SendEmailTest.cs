using NUnit.Allure.Core;
using SendEmailProject.TestSupport;
using SendEmailProject.PageObjects;

namespace SendEmailProject.Tests;

[TestFixture]
[AllureNUnit]
public class SendEmailTest : BaseTest
{
    private HomePage? _homePage;
    private LoginPage? _loginPage;
    private readonly string _emailSubject = $"Test Email {DateTime.UtcNow:yyyymmddhhmmss}";
    private readonly string _fileUploadPath = Path.GetFullPath("TestData/attachment.txt");
    private const string EmailBody = "Hi,\n\nthis is a test email.\n\nKind Regards\n\nJaroslav";

    [SetUp]
    public async Task TestSetUp()
    {
        _homePage = new HomePage(Driver!.Page.Result);
        _loginPage = new LoginPage(Driver.Page.Result);
        await _loginPage.Open(TestConfig!.BaseUrl!);
    }

    [Test]
    public async Task SendEmail()
    {
        await _loginPage!.CheckThatLoginPageIsLoaded();
        await _loginPage.LoginToEmail(TestConfig!.Username!, TestConfig.Password!);
        await _homePage!.CheckThatHomePageIsLoaded();
        await _homePage.SendEmail(Recipient!, _emailSubject, EmailBody, _fileUploadPath);
        await _homePage.CheckThatEmailWasSent(Recipient!, _emailSubject);
        await GmailClient!.CheckThatEmailWasReceivedAsync(TestConfig.UserEmail!, _emailSubject,
            30);
        await _homePage.LogOut();
    }
}