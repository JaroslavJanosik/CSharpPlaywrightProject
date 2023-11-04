using Allure.Net.Commons;
using Microsoft.Playwright;
using NUnit.Framework.Interfaces;

namespace SendEmailProject.TestSupport
{
    public class BaseTest
    {
        protected TestConfig? TestConfig;
        private PlaywrightConfig? _playwrightConfig; 
        protected GmailClient? GmailClient;
        protected string? Recipient;
        protected Driver? Driver;
        private DriverInit? _driverInit;
        private readonly string _traceName = TestContext.CurrentContext.Test.Name.Replace(" ", "_");

        [SetUp]
        public async Task SetUp()
        {
            TestConfig = new TestConfig().AddConfig("test.settings.json");
            _playwrightConfig = new PlaywrightConfig().AddConfig("pw.settings.json");
            Recipient = TestConfig.RecipientEmail!;
            GmailClient = new GmailClient();
            _driverInit = new DriverInit();
            Driver = new Driver(_driverInit, _playwrightConfig);
            
            var tracing = await Driver.Tracing;
            await tracing.StartAsync(new TracingStartOptions
            {
                Name = _traceName,
                Screenshots = true,
                Snapshots = true
            });
        }

        [TearDown]
        public async Task TearDown()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            
            var tracing = await Driver!.Tracing;
            await tracing.StopAsync(new TracingStopOptions()
            {
                Path = $"Traces/{_traceName}.zip"
            });

            if (testStatus == TestStatus.Failed)
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                var page = await Driver!.Page;
                var file = await Helpers.TakeScreenshot(page);
                var fullPath = Path.Combine(currentDirectory, file);
                AllureLifecycle.Instance.AddAttachment(fullPath);
            }

            await Driver.DisposeAsync();
        }
    }
}