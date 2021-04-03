namespace ProfileService.Data.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using ProfileService.Data.Entities;

    public class ProfileContext : DbContext
    {
        public DbSet<ProfileEntity> Profiles;
    }
}