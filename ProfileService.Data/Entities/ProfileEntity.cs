// <copyright file="ProfileEntity.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the ORM for the profile entity.
    /// </summary>
    [Table("Profiles", Schema = "Profiles")]
    public class ProfileEntity
    {
        /// <summary>
        /// Gets or sets the ID, the primary key of the profile table.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the UserId, referencing the user who owns the profile.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the Username, referencing the user who owns the profile.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the Bio text of the profile.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Gets or sets the url of the users profile image.
        /// </summary>
        public string ImageUrl { get; set; }
    }
}