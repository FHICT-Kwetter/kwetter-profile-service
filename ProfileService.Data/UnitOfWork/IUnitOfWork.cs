// <copyright file="IUnitOfWork.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

using AutoMapper;

namespace ProfileService.Data.UnitOfWork
{
    using System.Threading.Tasks;
    using ProfileService.Data.Contexts;
    using ProfileService.Data.Repositories;

    /// <summary>
    /// Defines the Unit of Work interface. The only API through which the database communication should be handled.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the profile repository.
        /// </summary>
        IProfileRepository Profiles { get; }

        /// <summary>
        /// Saves all changes made to the database in a transactional way.
        /// </summary>
        /// <returns>An awaitable task which returns the rows affected in the database.</returns>
        Task<int> SaveAsync();
    }

    /// <summary>
    /// Defines the implementation of the unit of work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly IProfileContext context;

        /// <inheritdoc/>
        public IProfileRepository Profiles { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="mapper">The automapper.</param>
        public UnitOfWork(IProfileContext context, IMapper mapper)
        {
            this.context = context;

            this.Profiles = new ProfileRepository(context, mapper);
        }

        /// <inheritdoc/>
        public async Task<int> SaveAsync()
        {
            return await this.context.SaveChangesAsync();
        }
    }
}