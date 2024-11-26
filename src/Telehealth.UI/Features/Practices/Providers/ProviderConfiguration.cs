using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Telehealth.UI.Extensions;
using Telehealth.UI.Features.Profiles;

namespace Telehealth.UI.Features.Practices.Providers;

public class ProviderConfiguration : IEntityTypeConfiguration<Provider.Details>
{
	public void Configure(EntityTypeBuilder<Provider.Details> builder)
	{
		builder
			.ToTable(Provider.TableName)
			.HasKey(p => p.ProviderId)
			.IsClustered();

		builder
			.Property(p => p.ProviderId)
			.UseIdentityColumn()
			.HasConversion(providerId => providerId!.Value, value => new Provider.Id(value))
			.IsRequired();

		builder
			.HasOne(p => p.Profile)
			.WithMany()
			.HasForeignKey(provider => provider.ProfileId)
			.OnDelete(DeleteBehavior.NoAction)
			.IsRequired();

		builder
			.Property(p => p.Rate)
			.HasPrecision(18, 0)
			.IsRequired();
	}
}

public sealed class ProviderFaker : Faker<Provider.Details>
{
	private readonly ProfileFaker _profileFaker = new ProfileFaker().WithProfileType(Profile.ProfileType.Provider);

	public ProviderFaker()
	{
		//UseSeed(650)
		this.UsePrivateConstructor()
			.RuleFor(x => x.Rate, f => f.Random.Number(75, 300))
			.RuleFor(x => x.Profile, _ => _profileFaker.Generate())
			.RuleFor(x => x.ProfileId, (_, p) => p.Profile!.ProfileId);
	}

	public ProviderFaker WithIds()
	{
		RuleFor(p => p.ProviderId, f => new(f.IndexFaker));
		return this;
	}
}