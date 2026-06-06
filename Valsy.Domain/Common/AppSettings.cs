namespace BuildingBlocks.Domain.Entities
{
    public class AppSettings
    {
        public AzureSettings Azure { get; set; }
        public ConnectionStringsSettings ConnectionStrings { get; set; }
        public AzureAdB2CSettings AzureAdB2C { get; set; }
        public MicrosoftGraphSettings MicrosoftGraph { get; set; }
        public HangfireSettings Hangfire { get; set; }
        public NDCMessagingSettings NDCMessaging { get; set; }
        public NDCCoreSettings NDCCore { get; set; }
        public PipelineConfigSettings PipelineConfig { get; set; }
        public Emails Emails { get; set; }
        public SMS SMS { get; set; }
        public string ApplySeeds { get; set; }
        public string TimeOut { get; set; }
        public string FStoreDirectoryPath { get; set; }
        public NewRelic NewRelic { get; set; }
        public LogHelperConfigurations LogHelperConfigurations { get; set; }
        public string AzureSyncCompletionDelayInSeconds { get; set; }
    }

    public class LogHelperConfigurations
    {
        public string LogEventLevel { get; set; }
    }
  public class NewRelic
    {
        public string EndPointUrl { get; set; }
        public string ApplicationName { get; set; }
        public string LicenseKey { get; set; }
        public string PeriodInMinutes { get; set; }
    }
    public class Emails
    {
        public string EmailForNewAdmins { get; set; }
        public string EmailForReceipt { get; set; }
        public string EmailForNewCollector { get; set; }
        public string EmailForOTP { get; set; }
        public string EmailForResetCollectorPassword { get; set; }
        public string EmailForForgetPassword { get; set; }

    }
    public class SMS
    {
        public string OTPSMS { get; set; }
    }
    public class AzureSettings
    {
        public string ConnectionString { get; set; }
        public string defaultContainerName { get; set; }
        public string UrlDefault { get; set; }
    }

    public class ConnectionStringsSettings
    {
        public string DefaultConnectionString { get; set; }
        public string Hangfire { get; set; }
    }

    public class AzureAdB2CSettings
    {
        public string Instance { get; set; }
        public string ClientId { get; set; }
        public string Domain { get; set; }
        public string SignUpSignInPolicyId { get; set; }
        public string Scopes { get; set; }
    }

    public class MicrosoftGraphSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string B2cExtensionAppClientId { get; set; }
    }

    public class HangfireSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class NDCMessagingSettings
    {
        public string ApiKey { get; set; }
        public string ClientId { get; set; }
        public string BaseUrl { get; set; }
        public MessagingAction Action { get; set; }
    }

    public class MessagingAction
    {
        public string MessagingOrchestration { get; set; }
        public string MessagingOrchestrationValidate { get; set; }

    }
    public class NDCCoreSettings
    {
        public string ConnectionStringsMester { get; set; }
        public string ConnectionStringsSlave { get; set; }
        public string UserIdNDCCore { get; set; }
        public string IsFeatureEnabled { get; set; }
    }

    public class PipelineConfigSettings
    {
        public CorsSettings Cors { get; set; }
        public RateLimitingSettings RateLimiting { get; set; }
        public RetryConfigSettings RetryConfig { get; set; }
    }

    public class CorsSettings
    {
        public List<string> AllowedOrigins { get; set; }
        public List<string> AllowedMethods { get; set; }
        public List<string> AllowedHeaders { get; set; }
        public string MobileApiKey { get; set; }
    }

    public class RateLimitingSettings
    {
        public string FixedWindowtime { get; set; }
        public string FixedWindowPermitLimit { get; set; }
        public string FixedWindowQueueLimit { get; set; }

    }

    public class RetryConfigSettings
    {
        public string RetryCount { get; set; }
        public string SleepDuration { get; set; }
    }

}