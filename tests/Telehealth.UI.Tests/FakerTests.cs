using Telehealth.UI.Features.Practices.Providers;

namespace Telehealth.UI.Tests;

public sealed class FakerTests
{
	[Fact]
	public void Should_Create_Providers()
	{
		var providerFaker = new ProviderFaker();
		var providers = providerFaker.Generate(5);


	}
}
