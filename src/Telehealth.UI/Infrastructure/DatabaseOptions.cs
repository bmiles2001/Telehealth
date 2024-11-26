using Microsoft.Extensions.Options;

namespace Telehealth.UI.Infrastructure;

public class DatabaseOptions
{
	public string ConnectionString { get; set; } = string.Empty;
	public int MaxRetryCount { get; set; } = 5;
}

public class DatabaseOptionsSetup(IConfiguration configuration) : IConfigureOptions<DatabaseOptions>
{
	public const string ConnectionStringName = "Telehealth";
	public const string DatabaseConnectionStringKey = $"ConnectionStrings:{ConnectionStringName}";
	public void Configure(DatabaseOptions options)
	{
		options.ConnectionString = configuration.GetConnectionString(ConnectionStringName)!;

		configuration.GetSection(DatabaseConnectionStringKey).Bind(options);
	}
}