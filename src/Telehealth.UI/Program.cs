using Telehealth.UI.Contracts;
using Telehealth.UI.Features.Scheduling;

namespace Telehealth.UI;

public static class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddRazorComponents()
			.AddInteractiveServerComponents();

		builder.Services.AddDevExpressBlazor(configure => configure.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5);
		builder.Services.AddScoped<IPracticeProvider, PracticeProvider>();
		builder.Services.AddScoped<IScheduleProvider, FakeScheduleProvider>();
		builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));

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
