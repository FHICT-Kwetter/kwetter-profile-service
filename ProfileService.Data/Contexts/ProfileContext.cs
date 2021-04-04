// <copyright file="ProfileContext.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Data.Contexts
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using ProfileService.Data.Entities;

    public interface IProfileContext
    {
        DbSet<ProfileEntity> Profiles { get; set; }

        EntityEntry<TEntity> Update<TEntity>([NotNull] TEntity entity) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Defines the database context for the application.
    /// </summary>
    public class ProfileContext : DbContext, IProfileContext
    {
        /// <summary>
        /// Gets or sets the profile entity db set.
        /// </summary>
        public DbSet<ProfileEntity> Profiles { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileContext"/> class.
        /// </summary>
        /// <param name="options">The database options.</param>
        public ProfileContext(DbContextOptions options) : base(options)
        {
        }
    }
}