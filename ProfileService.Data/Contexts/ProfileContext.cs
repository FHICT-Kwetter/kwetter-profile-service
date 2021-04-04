// <copyright file="ProfileContext.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Data.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using ProfileService.Data.Entities;

    /// <summary>
    /// Defines the database context for the application.
    /// </summary>
    public class ProfileContext : DbContext
    {
        /// <summary>
        /// Gets or sets the profile entity db set.
        /// </summary>
        public DbSet<ProfileEntity> Profiles { get; set; }
    }
}