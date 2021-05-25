using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ProfileService.Data.Contexts;
using ProfileService.Data.Mapping;
using ProfileService.Data.UnitOfWork;
using ProfileService.Domain.Exceptions;
using ProfileService.Domain.Requests;
using Profile = ProfileService.Domain.Models.Profile;

namespace ProfileService.Data.Tests
{
    public class CreateUserTests
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
        public async Task Given_A_New_Profile__When_Creating_Profile__UnitOfWork_Returns_Correct_Profile_Data()
        {
            // Arrange
            var profile = Profile.Create(new CreateProfileRequest()
            {
                Username = "Username",
                DisplayName = "Display Name",
                Bio = "Bio",
                ImageUrl = "link"
            });

            // Act
            var createdProfile = await this.unitOfWork.Profiles.Create(profile, userId);
            await this.unitOfWork.SaveAsync();
            
            // Assert
            Assert.AreEqual(profile.Username, createdProfile.Username);
            Assert.AreEqual(profile.Bio, createdProfile.Bio);
            Assert.AreEqual(profile.ImageUrl, createdProfile.ImageUrl);
        }
        
        [Test]
        public async Task Given_A_New_Profile__When_Reading_Profile_By_UserId__UnitOfWork_Returns_Correct_Profile_Data()
        {
            // Arrange
            var profile = await this.CreateTestProfile();
            
            // Act
            var readProfile = await this.unitOfWork.Profiles.Read(userId);
            
            // Assert
            Assert.AreEqual(readProfile.Username, profile.Username);
            Assert.AreEqual(readProfile.Bio, profile.Bio);
            Assert.AreEqual(readProfile.ImageUrl, profile.ImageUrl);
        }
        
        [Test]
        public async Task Given_A_New_Profile__When_Reading_Profile_By_UserName__UnitOfWork_Returns_Correct_Profile_Data()
        {
            // Arrange
            var profile = await this.CreateTestProfile();

            // Act
            var readProfile = await this.unitOfWork.Profiles.Read(profile.Username);
            
            // Assert
            Assert.AreEqual(readProfile.Username, profile.Username);
            Assert.AreEqual(readProfile.Bio, profile.Bio);
            Assert.AreEqual(readProfile.ImageUrl, profile.ImageUrl);
        }

        [Test]
        public async Task Given_An_Existing_Profile__When_Creating_Profile__UnitOfWork_Throws_Exception()
        {
            // Arrange 1 
            var profile = await this.CreateTestProfile();

            // Act + Assert
            Assert.That(async () => await this.unitOfWork.Profiles.Create(profile, this.userId), Throws.InstanceOf<ProfileCreationException>());
        }
        
        private async Task<Profile> CreateTestProfile()
        {
            var profile = Profile.Create(new CreateProfileRequest
            {
                Username = "tester",
                Bio = "bio",
                DisplayName = "Test Profile",
                ImageUrl = "ImageUrl"
            });

            await this.unitOfWork.Profiles.Create(profile, userId);
            await this.unitOfWork.SaveAsync();

            return profile;
        }
    }
}