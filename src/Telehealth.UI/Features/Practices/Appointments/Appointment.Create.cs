using MediatR;
using Telehealth.UI.Features.Practices.Locations;
using Telehealth.UI.Features.Practices.Providers;
using Telehealth.UI.Features.Profiles;
using Telehealth.UI.Infrastructure;

namespace Telehealth.UI.Features.Practices.Appointments;

public static partial class Appointment
{
	public class Create : IRequest<Details>
	{
		public required AppointmentState State { get; init; }
		public required Profile.Id PatientId { get; init; }
		public Provider.Id? ProviderId { get; init; }
		public required Location.Id LocationId { get; init; }
		public required DateTimeOffset AppointmentTime { get; init; }
		public required string AppointmentType { get; init; }
		public required TimeSpan Duration { get; init; }
		public string? Notes { get; init; }
	}

	public class CreateAppointmentHandler(TelehealthDbContext context) : IRequestHandler<Create, Details>
	{
		public async Task<Details> Handle(Create request, CancellationToken cancellationToken)
		{
			var newAppointment = Details
				.Create(request.PatientId,
					request.ProviderId,
					request.LocationId,
					request.AppointmentTime,
					request.AppointmentType,
					request.Duration,
					request.Notes);

			await context
				.Set<Details>()
				.AddAsync(newAppointment, cancellationToken);

			await context.SaveChangesAsync(cancellationToken);

			return newAppointment;
		}
	}
}
