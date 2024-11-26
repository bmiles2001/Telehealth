using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Telehealth.UI.Extensions;
using Telehealth.UI.Features.Practices.Locations;

namespace Telehealth.UI.Features.Practices;

public sealed class PracticeConfiguration : IEntityTypeConfiguration<Practice.Details>
{
	public void Configure(EntityTypeBuilder<Practice.Details> builder)
	{
		builder
			.ToTable(Practice.TableName)
			.HasKey(p => p.PracticeId)
			.IsClustered();

		builder
			.Property(x => x.PracticeId)
			.UseIdentityColumn()
			.HasConversion(practiceId => practiceId!.Value, value => new Practice.Id(value))
			.IsRequired();

		builder
			.Property(p => p.Name)
			.HasMaxLength(100)
			.IsRequired();

		builder.Property(p => p.Description);

		builder
			.Property(p => p.IsVirtual)
			.IsRequired();

		builder
			.HasMany(p => p.Locations)
			.WithOne(location => location.Practice)
			.HasForeignKey(location => location.PracticeId)
			.IsRequired();
	}
}

public sealed class PracticeFaker : Faker<Practice.Details>
{
	private readonly LocationFaker _locationFaker = new();

	public PracticeFaker()
	{
		//UseSeed(250)
		this.UsePrivateConstructor()
			.RuleFor(p => p.Name, f => f.Company.CompanyName())
			.RuleFor(p => p.Description, f => f.Company.CatchPhrase())
			.RuleFor(p => p.IsVirtual, f => f.Random.Bool())
			.RuleFor(x => x.Locations, (_, l) => _locationFaker.ForPractice(l.PracticeId!).Generate(10));
	}

	public PracticeFaker WithIds()
	{
		RuleFor(x => x.PracticeId, f => new(f.IndexFaker));
		return this;
	}
}