// <copyright file="ProfileMapping.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Data.Mapping
{
    using ProfileService.Data.Entities;

    /// <summary>
    /// Defines the <see cref="ProfileMapping" />.
    /// </summary>
    public class ProfileMapping : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileMapping"/> class.
        /// </summary>
        public ProfileMapping()
        {
            this.CreateMap<Domain.Models.Profile, ProfileEntity>()
                .ForMember(e => e.UserId, expression => expression.MapFrom(d => d.UserId))
                .ForMember(e => e.Username, expression => expression.MapFrom(d => d.Username))
                .ForMember(e => e.Bio, expression => expression.MapFrom(d => d.Bio))
                .ForMember(e => e.DisplayName, expression => expression.MapFrom(d => d.DisplayName))
                .ForMember(e => e.ImageUrl, expression => expression.MapFrom(d => d.ImageUrl))
                .ReverseMap();
        }
    }
}