using System.Reflection;
using DbUp;
using DbUp.Engine;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;
using WeddingShare.Enums;
using WeddingShare.Helpers.Database;
using WeddingShare.Models.Database;

namespace WeddingShare.Helpers.Dbup
{
    public class DbupHelper
    {
        private readonly ILogger _logger;

        public DbupHelper(ILogger<DbupHelper> logger)
        {
            _logger = logger;
        }

        public DatabaseUpgradeResult UpgradeDatabase(string connectionString)
        {
            var upgrader = DeployChanges.To
                .PostgresqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();
            
            if (!result.Successful)
            {
                _logger.LogError("Database upgrade failed: {Error}", result.Error);
            }
            else
            {
                _logger.LogInformation("Database upgrade successful.");
            }
            
            return result;
        }
    }

    public sealed class DbupMigrator : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IDatabaseHelper _database;
        private readonly ILoggerFactory _loggerFactory;

        public DbupMigrator(IConfiguration configuration, IDatabaseHelper database, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _database = database;
            _loggerFactory = loggerFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var logger = _loggerFactory.CreateLogger<DbupMigrator>();

            var connString = _configuration.GetConnectionString("DefaultConnection");
            if (!string.IsNullOrWhiteSpace(connString))
            {
                var dbupLogger = _loggerFactory.CreateLogger<DbupHelper>();
                DatabaseUpgradeResult dbupResult = new DbupHelper(dbupLogger).UpgradeDatabase(connString);
                
                if (!dbupResult.Successful)
                {
                    logger.LogWarning("DBUP failed with error: '{Error}'", dbupResult.Error?.Message);
                }

                var adminAccount = new UserModel()
                {
                    Username = _configuration["Settings:Account:Admin:Username"] ?? "admin",
                    Password = _configuration["Settings:Account:Admin:Password"] ?? "admin"
                };
                await _database.InitAdminAccount(adminAccount);
                
                if (_configuration.GetValue("Settings:Account:Admin:Log_Password", true))
                {
                    logger.LogInformation("Password: {Password}", adminAccount.Password);
                }

                if (_configuration.GetValue("Security:2FA:Reset_To_Default", false))
                {
                    await _database.ResetMultiFactorToDefault();
                }
            }
            else
            {
                logger.LogError("DBUP failed: Connection string was null or empty");
                throw new ArgumentNullException("Database connection string is required.");
            }
        }
    }
}