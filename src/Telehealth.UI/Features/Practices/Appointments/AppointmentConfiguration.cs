using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Telehealth.UI.Extensions;
using Telehealth.UI.Features.Practices.Locations;
using Telehealth.UI.Features.Practices.Providers;
using Telehealth.UI.Features.Profiles;

namespace Telehealth.UI.Features.Practices.Appointments;

public sealed class AppointmentConfiguration : IEntityTypeConfiguration<Appointment.Details>
{
	public void Configure(EntityTypeBuilder<Appointment.Details> builder)
	{
		builder
			.ToTable(Appointment.TableName)
			.HasKey(x => x.AppointmentId)
			.IsClustered();

		builder
			.Property(x => x.AppointmentId)
			.UseIdentityColumn()
			.HasConversion(appointmentId => appointmentId!.Value, value => new Appointment.Id(value))
			.IsRequired();

		builder
			.Property(x => x.State)
			.IsRequired();

		builder
			.HasOne(appointment => appointment.Patient)
			.WithMany()
			.HasForeignKey(appointment => appointment.PatientId)
			.OnDelete(DeleteBehavior.NoAction)
			.IsRequired();

		builder
			.HasOne(appointment => appointment.Provider)
			.WithMany()
			.HasForeignKey(appointment => appointment.ProviderId)
			.OnDelete(DeleteBehavior.NoAction);

		builder
			.HasOne(appointment => appointment.Location)
			.WithMany()
			.HasForeignKey(appointment => appointment.LocationId)
			.OnDelete(DeleteBehavior.NoAction)
			.IsRequired();

		builder
			.Property(x => x.AppointmentTime)
			.HasPrecision(0)
			.IsRequired();

		builder
			.Property(x => x.AppointmentType)
			.IsRequired();

		builder
			.Property(x => x.Duration)
			.HasPrecision(0)
			.IsRequired();

		builder
			.Property(a => a.Notes);
	}
}

public sealed class AppointmentFaker : Faker<Appointment.Details>
{
	public AppointmentFaker()
	{
		//UseSeed(750)
		this.UsePrivateConstructor()
			.RuleFor(x => x.State, f => f.PickRandom<Appointment.AppointmentState>())
			.RuleFor(x => x.AppointmentType, f => f.Commerce.Categories(1)[0])
			.RuleFor(x => x.Duration, f => TimeSpan.FromMinutes(f.PickRandom(15, 30, 60)))
			.RuleFor(x => x.Notes, f => f.Lorem.Paragraph(1).OrNull(f));
	}

	public AppointmentFaker WithIds()
	{
		RuleFor(x => x.AppointmentId, f => new(f.IndexFaker));
		return this;
	}

	public AppointmentFaker WithLocation(IEnumerable<Location.Details> location)
	{
		RuleFor(x => x.Location, f => f.PickRandom(location))
		.RuleFor(x => x.LocationId, (_, p) => p.Location?.LocationId)
		.RuleFor(x => x.AppointmentTime, (f, p) => WithHoursBetween(f, p.Location?.OperatingHours!));
		return this;
	}

	public AppointmentFaker WithProvider(IEnumerable<Provider.Details> provider)
	{
		RuleFor(x => x.Provider, f => f.PickRandom(provider))
		.RuleFor(x => x.ProviderId, (_, p) => p.Provider?.ProviderId);
		return this;
	}

	public AppointmentFaker WithPatient(IEnumerable<Profile.Details> patient)
	{
		RuleFor(x => x.Patient, f => f.PickRandom(patient))
		.RuleFor(x => x.PatientId, (_, p) => p.Patient?.ProfileId);
		return this;
	}

	private DateTimeOffset WithHoursBetween(Faker faker, Location.OperatingHours operatingHours)
	{
		var recent = DateTime.Today.AddDays(-2).Add(operatingHours.OpeningTime);
		var soon = DateTime.Today.AddDays(2).Add(operatingHours.ClosingTime);

		return faker.Date.Between(recent, soon);
	}
}