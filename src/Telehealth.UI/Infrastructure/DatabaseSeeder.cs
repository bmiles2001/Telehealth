using Bogus;
using Microsoft.EntityFrameworkCore;
using Telehealth.UI.Features.Practices;
using Telehealth.UI.Features.Practices.Appointments;
using Telehealth.UI.Features.Practices.Providers;
using Telehealth.UI.Features.Profiles;

namespace Telehealth.UI.Infrastructure;

public static class Seed
{
	private static readonly PracticeFaker PracticeFaker = new();
	private static readonly ProfileFaker PatientFaker = new ProfileFaker().WithProfileType(Profile.ProfileType.Patient);
	private static readonly ProviderFaker ProviderFaker = new();
	private static readonly AppointmentFaker AppointmentFaker = new();

	public static void WithFakeData(DbContext context, bool _)
	{
		var practices = Process(PracticeFaker, 1, context);
		var providers = Process(ProviderFaker, 3, context);
		var patients = Process(PatientFaker, 15, context);
		var locations = practices.SelectMany(x => x.Locations!);

		Process(
			AppointmentFaker
			.WithLocation(locations)
			.WithProvider(providers)
			.WithPatient(patients)
			, 150, context);
	}

	public static async Task WithFakeDataAsync(DbContext context, bool _, CancellationToken cancellationToken)
	{
		var practices = await Process(PracticeFaker, 1, context, cancellationToken);
		var providers = await Process(ProviderFaker, 3, context, cancellationToken);
		var patients = await Process(PatientFaker, 15, context, cancellationToken);
		var locations = practices.SelectMany(x => x.Locations!);

		await Process(AppointmentFaker
			.WithLocation(locations)
			.WithProvider(providers)
			.WithPatient(patients)
			, 150, context, cancellationToken);
	}

	private static List<TModel> Process<TModel>(Faker<TModel> faker, int numberOfRecords, DbContext db) where TModel : class
	{
		var source = faker.Generate(numberOfRecords);
		var target = db.Set<TModel>();
		var newRecords = source.Except(target);

		if (newRecords.Any())
		{
			db.Set<TModel>().AddRange(newRecords);
			db.SaveChanges();
		}

		return source;
	}

	private static async Task<List<TModel>> Process<TModel>(Faker<TModel> faker, int numberOfRecords, DbContext db, CancellationToken cancellationToken) where TModel : class
	{
		var source = faker.Generate(numberOfRecords);
		var target = db.Set<TModel>();
		var newRecords = source.Except(target);

		if (newRecords.Any())
		{
			db.Set<TModel>().AddRange(newRecords);
			await db.SaveChangesAsync(cancellationToken);
		}

		return source;
	}
}