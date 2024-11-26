using MediatR;
using Microsoft.EntityFrameworkCore;
using Telehealth.UI.Features.Practices.Locations;
using Telehealth.UI.Infrastructure;

namespace Telehealth.UI.Features.Practices;

public static class Practice
{
	public const string TableName = nameof(Practice);
	public record Id(int Value);

	public sealed class Details
	{
		public Id? PracticeId { get; init; }
		public required string Name { get; init; }
		public string? Description { get; init; }
		public bool IsVirtual { get; init; }

		public IReadOnlyList<Location.Details>? Locations { get; init; }

		private Details() { }
		public static Details Create(string name, string description, bool isVirtual)
		{
			return new()
			{
				Name = name,
				Description = description,
				IsVirtual = isVirtual
			};
		}
	}

	public class Create : IRequest<Details>
	{
		public required Details NewPractice { get; init; }
	}

	public class CreatePracticeHandler(TelehealthDbContext context) : IRequestHandler<Create, Details>
	{
		public async Task<Details> Handle(Create request, CancellationToken cancellationToken)
		{
			await context
				.Set<Details>()
				.AddAsync(request.NewPractice, cancellationToken);

			await context.SaveChangesAsync(cancellationToken);

			return request.NewPractice;
		}
	}

	public class GetBy : IRequest<Details>
	{
		public required Id PracticeId { get; init; }
	}

	public class GetByPracticeIdHandler(TelehealthDbContext context) : IRequestHandler<GetBy, Details>
	{
		public async Task<Details> Handle(GetBy request, CancellationToken cancellationToken)
		{
			return await context
				.Set<Details>()
				.Where(p => p.PracticeId == request.PracticeId)
				.Include(p => p.Locations)
				.FirstAsync(cancellationToken);
		}
	}
}