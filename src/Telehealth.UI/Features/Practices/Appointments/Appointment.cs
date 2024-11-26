using Telehealth.UI.Features.Practices.Locations;
using Telehealth.UI.Features.Practices.Providers;
using Telehealth.UI.Features.Profiles;

namespace Telehealth.UI.Features.Practices.Appointments;

public static partial class Appointment
{
	public const string TableName = nameof(Appointment);

	public record Id(int Value);
	public enum AppointmentState { Confirmed, Unconfirmed, Cancelled };
	public record DateRange(DateTimeOffset Start, DateTimeOffset End);
	public class Details
	{
		public Id? AppointmentId { get; init; }
		public required AppointmentState State { get; init; }
		public required Profile.Id PatientId { get; init; }
		public Provider.Id? ProviderId { get; init; }
		public required Location.Id LocationId { get; init; }
		public required DateTimeOffset AppointmentTime { get; init; }
		public required string AppointmentType { get; init; }
		public required TimeSpan Duration { get; init; }
		public string? Notes { get; init; }

		public Profile.Details? Patient { get; init; }
		public Provider.Details? Provider { get; init; }
		public Location.Details? Location { get; init; }

		public static Details Create(
			Profile.Id patientId,
			Provider.Id? providerId,
			Location.Id locationId,
			DateTimeOffset appointmentTime,
			string appointmentType,
			TimeSpan duration,
			string? notes = null)
		{
			return new Details
			{
				State = AppointmentState.Unconfirmed,
				PatientId = patientId,
				ProviderId = providerId,
				LocationId = locationId,
				AppointmentTime = appointmentTime,
				AppointmentType = appointmentType,
				Duration = duration,
				Notes = notes
			};
		}
	}
}