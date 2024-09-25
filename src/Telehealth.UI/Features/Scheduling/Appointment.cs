namespace Telehealth.UI.Features.Scheduling;

public static class Appointment
{
	public enum State { Confirmed, Unconfirmed, Cancelled }

	public class Response
	{
		public required string For { get; set; }
		public State State { get; set; } = State.Unconfirmed;
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string? Place { get; set; }
		public int? ProviderId { get; set; }
	}
}