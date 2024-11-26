using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Telehealth.UI.Infrastructure;

namespace Telehealth.UI.Extensions;

public static class DependencyInjection
{
	public static WebApplicationBuilder AddDatabaseContext(this WebApplicationBuilder builder)
	{
		builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

		builder.Services.AddDbContextFactory<TelehealthDbContext>((provider, options) =>
		{
			var dbOptions = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

			options.UseSqlServer(dbOptions.ConnectionString, sqlServer => sqlServer.EnableRetryOnFailure(dbOptions.MaxRetryCount))
			.EnableDetailedErrors()
			.EnableSensitiveDataLogging()
			.UseSeeding(Seed.WithFakeData)
			.UseAsyncSeeding(Seed.WithFakeDataAsync);
		});

		return builder;
	}
}
