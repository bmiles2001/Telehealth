using MediatR;
using Microsoft.EntityFrameworkCore;
using Telehealth.UI.Extensions;
using Telehealth.UI.Features.Practices.Appointments;
using Telehealth.UI.Features.Practices.Locations;
using Telehealth.UI.Features.Practices.Providers;
using Telehealth.UI.Features.Profiles;
using Telehealth.UI.Infrastructure;

namespace Telehealth.UI.Features.Scheduling;

public static partial class Schedule
{
	// DevExpress Scheduler throws error with DateTimeOffset
	public sealed class AppointmentDetails
	{
		public required string Title { get; init; }
		public required DateTime StartTime { get; init; }
		public required DateTime EndTime { get; init; }
		public Provider.Id? ProviderId { get; init; }
		public required string LocationName { get; init; }
		public required string AppointmentType { get; init; }
		public required TimeSpan Duration { get; init; }

		private AppointmentDetails() { }
		public static AppointmentDetails Create(string title, DateTimeOffset start, Provider.Id? providerId, Profile.Id? patientId, string locationName, string appointmentType, TimeSpan duration)
		{
			return new AppointmentDetails
			{
				Title = title,
				StartTime = start.LocalDateTime,
				EndTime = start.Add(duration).LocalDateTime,
				ProviderId = providerId,
				LocationName = locationName,
				AppointmentType = appointmentType,
				Duration = duration,
			};
		}

	}

	public class GetAppointments : IRequest<IEnumerable<AppointmentDetails>>
	{
		public required Appointment.DateRange DateRange { get; init; }
		public IEnumerable<Profile.Id>? Patients { get; init; }
		public IEnumerable<Provider.Id>? Providers { get; init; }
		public required Location.Id LocationId { get; init; }
	}

	public class GetAppointmentsHandler(TelehealthDbContext context) : IRequestHandler<GetAppointments, IEnumerable<AppointmentDetails>>
	{
		public async Task<IEnumerable<AppointmentDetails>> Handle(GetAppointments request, CancellationToken cancellationToken)
		{
			return await context
				.Set<Appointment.Details>()
				.Include(a => a.Provider)
				.Where(appointment => appointment.LocationId == request.LocationId)
				.Where(appointment => request.DateRange.Start <= appointment.AppointmentTime && appointment.AppointmentTime <= request.DateRange.End)
				//.Where(appointment => appointment.AppointmentTime.Date >= request.DateRange.Start.Date && appointment.AppointmentTime.Date <= request.DateRange.End.Date.AddDays(1))
				.WhereIf(request.Providers is not null, appointment => request.Providers!.Contains(appointment.ProviderId))
				.WhereIf(request.Patients is not null, appointment => request.Patients!.Contains(appointment.PatientId))
				.Select(a => AppointmentDetails.Create(a.Patient!.FullName, a.AppointmentTime, a.ProviderId, a.PatientId, a.Location!.Name, a.AppointmentType, a.Duration))
				.ToListAsync(cancellationToken);
		}
	}
}
