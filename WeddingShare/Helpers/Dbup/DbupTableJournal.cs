using System.Data;
using DbUp.Engine;
using DbUp.Engine.Output;
using DbUp.Engine.Transactions;
using DbUp.Support;
using DbUp.Postgresql;

namespace WeddingShare.Helpers.Dbup
{
    public class DbupTableJournal : PostgresqlTableJournal
    {
        public DbupTableJournal(Func<IConnectionManager> connectionManager, Func<IUpgradeLog> logger, string schema, string table)
            : base(connectionManager, logger, schema, table)
        {
        }

        public override void StoreExecutedScript(SqlScript script, Func<IDbCommand> dbCommandFactory)
        {
            var scriptName = script.Name;

            try
            {
                var parts = script.Name.Split('.', StringSplitOptions.RemoveEmptyEntries);
                if (parts != null && parts.Length >= 2)
                {
                    scriptName = string.Join(".", parts.Skip(parts.Length - 2).Take(2));
                }
            }
            catch { }

            base.StoreExecutedScript(new SqlScript(scriptName, script.Contents), dbCommandFactory);
        }
    }
}