using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Telehealth.UI.Extensions;

namespace Telehealth.UI.Features.Profiles;

public sealed class ProfileConfiguration : IEntityTypeConfiguration<Profile.Details>
{
	public void Configure(EntityTypeBuilder<Profile.Details> builder)
	{
		builder
			.ToTable(Profile.TableName)
			.HasKey(x => x.ProfileId)
			.IsClustered();

		builder
			.Property(x => x.ProfileId)
			.UseIdentityColumn()
			.HasConversion(profileId => profileId!.Value, value => new Profile.Id(value))
			.IsRequired();

		builder
			.Property(x => x.DateOfBirth)
			.HasPrecision(0)
			.IsRequired();

		builder
			.Property(x => x.Email)
			.HasMaxLength(75)
			.IsRequired();

		builder
			.Property(x => x.FirstName)
			.HasMaxLength(75)
			.IsRequired();

		builder
			.Property(x => x.LastName)
			.HasMaxLength(75)
			.IsRequired();

		builder
			.Property(x => x.PhoneNumber)
			.HasMaxLength(25)
			.IsRequired();

		builder
			.Property(x => x.ProfileType)
			.IsRequired();

		builder
			.HasIndex(x => x.ProfileType);
	}
}

public sealed class ProfileFaker : Faker<Profile.Details>
{
	public ProfileFaker()
	{
		//UseSeed(500)
		this.UsePrivateConstructor()
			.RuleFor(p => p.DateOfBirth, f => DateOnly.FromDateTime(f.Person.DateOfBirth))
			.RuleFor(p => p.Email, f => f.Person.Email)
			.RuleFor(p => p.FirstName, f => f.Person.FirstName)
			.RuleFor(p => p.LastName, f => f.Person.LastName)
			.RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber());
	}

	public ProfileFaker WithProfileType(Profile.ProfileType? profileType)
	{
		RuleFor(p => p.ProfileType, f => profileType ?? f.PickRandom<Profile.ProfileType>());
		return this;
	}

	public ProfileFaker WithIds()
	{
		RuleFor(p => p.ProfileId, f => new(f.IndexFaker));
		return this;
	}
}