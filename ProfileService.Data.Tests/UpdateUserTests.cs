using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ProfileService.Data.Contexts;
using ProfileService.Data.Mapping;
using ProfileService.Data.UnitOfWork;
using ProfileService.Domain.Exceptions;
using ProfileService.Domain.Models;
using ProfileService.Domain.Requests;
using Profile = ProfileService.Domain.Models.Profile;

namespace ProfileService.Data.Tests
{
    public class UpdateUserTests
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
        public async Task Given_An_Existing_Profile__When_Updating_Single_Value__Update_Is_Done_Correctly()
        {
            // Assert
            var profile = await this.CreateTestProfile();
            var mutate = new MutateProfileRequest { Bio = "Updated Bio" };
            
            // Act
            profile.Update(mutate);
            var updatedProfiel = await this.unitOfWork.Profiles.Update(profile);

            // Assert
            Assert.AreEqual(profile.Username, updatedProfiel.Username);
            Assert.AreEqual(profile.DisplayName, updatedProfiel.DisplayName);
            Assert.AreEqual(mutate.Bio, updatedProfiel.Bio);
            Assert.AreEqual(profile.ImageUrl, updatedProfiel.ImageUrl);
        }
        
        [Test]
        public async Task Given_An_Existing_Profile__When_Updating_Multiple_Values__Update_Is_Done_Correctly()
        {
            // Arrange
            var profile = await this.CreateTestProfile();
            var mutate = new MutateProfileRequest { Bio = "Updated Bio", DisplayName = "Updated Display name" };
            
            // Act
            profile.Update(mutate);
            var updatedProfiel = await this.unitOfWork.Profiles.Update(profile);

            // Assert
            Assert.AreEqual(profile.Username, updatedProfiel.Username);
            Assert.AreEqual(mutate.DisplayName, updatedProfiel.DisplayName);
            Assert.AreEqual(mutate.Bio, updatedProfiel.Bio);
            Assert.AreEqual(profile.ImageUrl, updatedProfiel.ImageUrl);
        }

        [Test]
        public async Task Given_A_Non_Existing_Profile__When_Updating_Profile__Throws_Profile_Not_Found_Exception()
        {
            // Arrange
            var profile = Profile.Create(new CreateProfileRequest { Username = "Non-existing profile" });
            
            // Act + Assert
            Assert.That(async () => await this.unitOfWork.Profiles.Update(profile), Throws.InstanceOf<ProfileNotFoundException>());
        }
        
        private async Task<Profile> CreateTestProfile()
        {
            var profile = Profile.Create(new CreateProfileRequest
            {
                UserId = userId,
                Username = "tester",
                Bio = "bio",
                DisplayName = "Test Profile",
                ImageUrl = "ImageUrl"
            });

            await this.unitOfWork.Profiles.Create(profile);
            await this.unitOfWork.SaveAsync();

            return profile;
        }
    }
}