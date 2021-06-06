using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ProfileService.Data.Contexts;
using ProfileService.Data.Mapping;
using ProfileService.Data.UnitOfWork;
using ProfileService.Domain.Exceptions;
using ProfileService.Domain.Requests;
using ProfileService.Service.UseCases;
using Profile = ProfileService.Domain.Models.Profile;

namespace ProfileService.Service.Tests.UseCases
{
    public class UpdateProfileTest
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
            this.unitOfWork = new UnitOfWork(context, mapper);
        }

        [Test]
        public async Task Given_An_Existing_Profile__When_Calling_UpdateProfile_Usecase__Returns_Updated_Profile()
        {
            // Arrange
            var profile = await this.CreateTestProfile();
            var request = new UpdateProfile(this.userId, "Bio", "Image", "DisplayName");
            var handler = new UpdateProfileHandler(this.unitOfWork);

            // Act
            var updatedProfile = await handler.Handle(request, CancellationToken.None);
            
            // Assert
            Assert.AreEqual(profile.Username, updatedProfile.Username);
            Assert.AreEqual(request.Bio, updatedProfile.Bio);
            Assert.AreEqual(request.ImageUrl, updatedProfile.ImageUrl);
            Assert.AreEqual(request.DisplayName, updatedProfile.DisplayName);
        }

        [Test]
        public async Task Given_A_Non_Existing_UserId__When_Calling_Update_Profile_Usecase__Throws_Profile_Not_Found_Exception()
        {
            // Arrange
            var request = new UpdateProfile(this.userId, "Bio", "Image", "DisplayName");
            var handler = new UpdateProfileHandler(this.unitOfWork);
            
            // Act + Assert
            Assert.That(async () => await handler.Handle(request, CancellationToken.None), Throws.InstanceOf<ProfileNotFoundException>());
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