using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ProfileService.Data.Contexts;
using ProfileService.Data.Mapping;
using ProfileService.Data.UnitOfWork;
using ProfileService.Domain.Exceptions;
using Profile = ProfileService.Domain.Models.Profile;

namespace ProfileService.Data.Tests
{
    public class DeleteUserTests
    {
        private IUnitOfWork unitOfWork;
        
        private readonly Guid userId = Guid.Parse("7ca08783-cb34-4489-9e48-068b12eb2b44");

        [SetUp]
        public void Setup()
        {
            // Create context.
            var database = new DbContextOptionsBuilder<ProfileContext>().UseInMemoryDatabase(databaseName: "ProfileDb");
            var context = new ProfileContext(database.Options);
            
            // Start each test with a new database.
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            
            // Create an instance of automapper
            var config = new MapperConfiguration(options => options.AddProfile<ProfileMapping>());
            var mapper = config.CreateMapper();
            
            // Setup unit of work object.
            this.unitOfWork = new UnitOfWork.UnitOfWork(context, mapper);
        }

        [Test]
        public async Task Given_An_Existing_Profile__When_Deleting_Profile__Profile_Is_Removed()
        {
            // Arrange
            var profile = await this.CreateTestProfile();
            
            // Act
            await this.unitOfWork.Profiles.Delete(profile);
            await this.unitOfWork.SaveAsync();
            
            // Assert
            Assert.That(async () => await this.unitOfWork.Profiles.Read(profile.Username), Throws.InstanceOf<ProfileNotFoundException>());
        }

        [Test]
        public async Task Given_A_Non_Existing_Profile__When_Deleting_Profile__Throws_Profile_Not_Found_Exception()
        {
            // Arrange
            var profile = new Profile() { Username = "Non-existing profile" };
            
            // Act + Assert
            Assert.That(async () => await this.unitOfWork.Profiles.Read(profile.Username), Throws.InstanceOf<ProfileNotFoundException>());
        }
        
        private async Task<Profile> CreateTestProfile()
        {
            var profile = new Profile
            {
                Username = "tester",
                Bio = "bio",
                DisplayName = "Test Profile",
                ImageLink = "imageLink"
            };

            await this.unitOfWork.Profiles.Create(profile, userId);
            await this.unitOfWork.SaveAsync();

            return profile;
        }
    }
}