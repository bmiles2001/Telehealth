using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Telehealth.UI.Extensions;

namespace Telehealth.UI.Features.Practices.Locations;

public class LocationConfiguration : IEntityTypeConfiguration<Location.Details>
{
	public void Configure(EntityTypeBuilder<Location.Details> builder)
	{
		builder
			.ToTable(Location.TableName)
			.HasKey(x => x.LocationId)
			.IsClustered();

		builder
			.Property(s => s.LocationId)
			.UseIdentityColumn()
			.HasConversion(locationId => locationId!.Value, value => new Location.Id(value))
			.IsRequired();

		builder
			.Property(location => location.Name)
			.HasMaxLength(250)
			.IsRequired();

		builder
			.HasOne(location => location.Practice)
			.WithMany()
			.HasForeignKey(location => location.PracticeId)
			.OnDelete(DeleteBehavior.NoAction)
			.IsRequired();

		builder.ComplexProperty(p => p.OperatingHours, hoursBuilder =>
		{
			hoursBuilder
				.Property(t => t.OpeningTime)
				.HasPrecision(0)
				.HasColumnName(nameof(Location.Details.OperatingHours.OpeningTime))
				.IsRequired();

			hoursBuilder
				.Property(t => t.ClosingTime)
				.HasPrecision(0)
				.HasColumnName(nameof(Location.Details.OperatingHours.ClosingTime))
				.IsRequired();
		});

		builder
			.Property(x => x.StreetAddress)
			.HasMaxLength(150)
			.IsRequired();

		builder
			.Property(x => x.City)
			.HasMaxLength(50)
			.IsRequired();

		builder
			.Property(x => x.State)
			.HasMaxLength(25)
			.IsRequired();

		builder
			.Property(x => x.Zip)
			.HasMaxLength(15)
			.IsRequired();
	}
}

public sealed class LocationFaker : Faker<Location.Details>
{
	public LocationFaker()
	{
		//UseSeed(375)
		this.UsePrivateConstructor()
			.RuleFor(x => x.Name, f => $"{f.Address.CardinalDirection()} {f.Address.StreetName()}")
			.RuleFor(x => x.StreetAddress, f => f.Address.StreetAddress())
			.RuleFor(x => x.OperatingHours, _ => new(TimeSpan.FromHours(8), TimeSpan.FromHours(17)))
			.RuleFor(x => x.City, f => f.Address.City())
			.RuleFor(x => x.State, f => f.Address.State())
			.RuleFor(x => x.Zip, f => f.Address.ZipCode());
	}

	public LocationFaker ForPractice(Practice.Id practiceId)
	{
		RuleFor(x => x.PracticeId, _ => practiceId);
		return this;
	}

	public LocationFaker WithIds()
	{
		RuleFor(x => x.LocationId, f => new(f.IndexFaker));
		return this;
	}
}