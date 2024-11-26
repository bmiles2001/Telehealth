using Telehealth.UI.Extensions;

namespace Telehealth.UI;

public static class Program
{
	public static readonly System.Reflection.Assembly ProgramAssembly = typeof(Program).Assembly;
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddRazorComponents()
			.AddInteractiveServerComponents();

		builder.AddDatabaseContext();

		builder.Services
			.AddMediatR(config => config.RegisterServicesFromAssembly(ProgramAssembly))
			.AddDevExpressBlazor(configure => configure.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5);

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Error");
		}

		app.UseStaticFiles();
		app.UseAntiforgery();

		app.MapRazorComponents<Components.App>()
			.AddInteractiveServerRenderMode();

		await app.RunAsync();
	}
}
