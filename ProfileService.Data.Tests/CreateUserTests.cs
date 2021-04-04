using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ProfileService.Data.Contexts;
using ProfileService.Data.UnitOfWork;
using ProfileService.Domain.Models;

namespace ProfileService.Data.Tests
{
    public class CreateUserTests
    {
        private IUnitOfWork unitOfWork;
        
        [SetUp]
        public void Setup()
        {
            // Create context.
            var database = new DbContextOptionsBuilder<ProfileContext>().UseInMemoryDatabase(databaseName: "ProfileDb");
            var context = new ProfileContext(database.Options);
            
            // Start each test with a new database.
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            
            // Setup unit of work object.
            this.unitOfWork = new UnitOfWork.UnitOfWork(context);
        }

        [Test]
        public async Task Given_A_New_Profile__When_Creating_Profile__UnitOfWork_Returns_Correct_Profile_Data()
        {
            // Arrange
            var profile = new Profile { Username = "tester", Bio = "bio", ImageLink = "http://www.test.com/image.jpg" };
            var userId = Guid.NewGuid();
            
            // Act
            var createdProfile = await this.unitOfWork.Profiles.Create(profile, userId);
            await this.unitOfWork.SaveAsync();
            
            // Assert
            Assert.AreEqual(profile.Username, createdProfile.Username);
            Assert.AreEqual(profile.Bio, createdProfile.Bio);
            Assert.AreEqual(profile.ImageLink, createdProfile.ImageLink);
        }
        
        [Test]
        public async Task Given_A_New_Profile__When_Reading_Profile_By_UserId__UnitOfWork_Returns_Correct_Profile_Data()
        {
            // Arrange
            var profile = new Profile { Username = "tester", Bio = "bio", ImageLink = "http://www.test.com/image.jpg" };
            var userId = Guid.NewGuid();
            
            // Act
            var createdProfile = await this.unitOfWork.Profiles.Create(profile, userId);
            await this.unitOfWork.SaveAsync();
            
            // Assert
            var readProfile = await this.unitOfWork.Profiles.Read(userId);
            Assert.AreEqual(readProfile.Username, createdProfile.Username);
            Assert.AreEqual(readProfile.Bio, createdProfile.Bio);
            Assert.AreEqual(readProfile.ImageLink, createdProfile.ImageLink);
        }
        
        [Test]
        public async Task Given_A_New_Profile__When_Reading_Profile_By_UserName__UnitOfWork_Returns_Correct_Profile_Data()
        {
            // Arrange
            var profile = new Profile { Username = "tester", Bio = "bio", ImageLink = "http://www.test.com/image.jpg" };
            var userId = Guid.NewGuid();
            
            // Act
            var createdProfile = await this.unitOfWork.Profiles.Create(profile, userId);
            await this.unitOfWork.SaveAsync();
            
            // Assert
            var readProfile = await this.unitOfWork.Profiles.Read(createdProfile.Username);
            Assert.AreEqual(readProfile.Username, createdProfile.Username);
            Assert.AreEqual(readProfile.Bio, createdProfile.Bio);
            Assert.AreEqual(readProfile.ImageLink, createdProfile.ImageLink);
        }

        [Test]
        public async Task Given_An_Existing_Profile__When_Creating_Profile__UnitOfWork_Throws_Exception()
        {
            // Arrange 1 
            var profileA = new Profile { Username = "tester", Bio = "bio", ImageLink = "http://www.test.com/image.jpg" };
            var userIdA = Guid.NewGuid();
            
            // Act
            await this.unitOfWork.Profiles.Create(profileA, userIdA);
            await this.unitOfWork.SaveAsync();
            
            // Assert
            Assert.That(async () => await this.unitOfWork.Profiles.Create(profileA, userIdA), Throws.Exception);
        }
    }
}