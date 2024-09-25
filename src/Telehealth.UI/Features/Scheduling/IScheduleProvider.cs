using Bogus;
using Telehealth.UI.Contracts;

namespace Telehealth.UI.Features.Scheduling;

public interface IScheduleProvider
{
	Task<IEnumerable<Appointment.Response>> GetAppointments(DateTime day, IEnumerable<Practice.Provider> providers);
}

public class FakeScheduleProvider(IPracticeProvider PracticeProvider) : IScheduleProvider
{
	const int LoadingDelay = 500;
	const int SessionDuration = 30;

	public async Task<IEnumerable<Appointment.Response>> GetAppointments(DateTime day, IEnumerable<Practice.Provider> providers)
	{
		await Task.Delay(TimeSpan.FromMilliseconds(LoadingDelay));
		List<Appointment.Response> _appointments = [];

		foreach (Practice.Provider provider in providers)
		{
			var lastDate = day;

			var fake = new Faker<Appointment.Response>()
				.RuleFor(p => p.For, fake => fake.Name.FullName())
				.RuleFor(p => p.State, fake => fake.PickRandom<Appointment.State>())
				.RuleFor(p => p.Place, fake => fake.Address.City())
				.RuleFor(p => p.ProviderId, _ => provider.Id)
				.RuleFor(p => p.StartTime, _ =>
				{
					// Simulate closure for lunch hour
					if (lastDate.Hour == 12)
					{
						lastDate = lastDate.AddHours(1);
					}

					if (Random.Shared.Next(1, 5) == 4)
					{
						lastDate = lastDate.AddMinutes(SessionDuration);
					}

					return lastDate;
				})
				.RuleFor(p => p.EndTime, _ =>
				{
					if (lastDate.Hour >= PracticeProvider.Configuration.ClosingTime.Hours)
					{
						return lastDate.AddHours(lastDate.Hour - PracticeProvider.Configuration.ClosingTime.Hours);
					}

					lastDate = lastDate.AddMinutes(SessionDuration);
					return lastDate;
				})
				;

			_appointments.AddRange(fake.GenerateBetween(12, 18));
		}

		return _appointments;
	}
}