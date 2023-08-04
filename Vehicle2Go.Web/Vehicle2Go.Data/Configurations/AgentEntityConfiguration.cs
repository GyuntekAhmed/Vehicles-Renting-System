namespace Vehicle2Go.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Agent;

    public class AgentEntityConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            // Uncomment before Seeding DB
            //builder.HasData(this.GenerateAgent());
        }

        private Agent GenerateAgent()
        {
            Agent agent = new Agent
            {
                Id = Guid.Parse("6FC60999-8FC8-46B6-A131-897EDD45A5F0"),
                PhoneNumber = "+359893794549",
                Address = "Silistra",
                UserId = Guid.Parse("389A42BB-6250-4E01-A7C6-29632BF524FA"),
            };

            return agent;
        }
    }
}
