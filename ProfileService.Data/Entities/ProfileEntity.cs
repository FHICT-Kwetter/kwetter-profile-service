namespace ProfileService.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("profiles", Schema = "profiles")]
    public class ProfileEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Username { get; set; }

        public string Bio { get; set; }

        public string ImageUrl { get; set; }
    }
}