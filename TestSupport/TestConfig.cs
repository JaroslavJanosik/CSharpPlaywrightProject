using Microsoft.Extensions.Configuration;

namespace SendEmailProject.TestSupport
{
    public class TestConfig
    {
        public string? BaseUrl { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? UserEmail { get; set; }
        public string? RecipientEmail { get; set; }
        
        public TestConfig AddConfig(string path)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(path).Build();

            return config.Get<TestConfig>()!;
        }
    }
}