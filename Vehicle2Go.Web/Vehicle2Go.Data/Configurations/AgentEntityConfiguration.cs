namespace Vehicle2Go.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Agent;

    public class AgentEntityConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.HasData(this.GenerateAgent());
        }

        private Agent GenerateAgent()
        {
            Agent agent = new Agent
            {
                Id = Guid.Parse("8af114de-010e-4b30-8920-9064facf5ae1"),
                PhoneNumber = "+359893794549",
                Address = "Silistra",
                UserId = Guid.Parse("e18cd243-7762-4aab-baee-8c8977e6ac83"),
            };

            return agent;
        }
    }
}
