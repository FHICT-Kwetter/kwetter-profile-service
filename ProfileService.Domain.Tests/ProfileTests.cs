using NUnit.Framework;
using ProfileService.Domain.Models;
using ProfileService.Domain.Requests;

namespace ProfileService.Domain.Tests
{
    public class ProfileTests
    {
        [Test]
        public void Given_A_Create_Profile_Request__When_Calling_Create__Correct_Profile_Data_Is_Returned()
        {
            // Arrange
            var createRequest = new CreateProfileRequest
            {
                Username = "Username",
                Bio = "Bio",
                DisplayName = "Display Name",
                ImageUrl = "link"
            };
            
            // Act
            var profile = Profile.Create(createRequest);
            
            // Assert
            Assert.AreEqual(createRequest.Username, profile.Username);
            Assert.AreEqual(createRequest.DisplayName, profile.DisplayName);
            Assert.AreEqual(createRequest.Bio, profile.Bio);
            Assert.AreEqual(createRequest.ImageUrl, profile.ImageUrl);
        }

        [Test]
        public void Given_An_Update_Profile_Request__When_Calling_Update__Profile_Data_Is_Correctly_Updated()
        {
            // Arrange
            var profile = CreateProfile();
            
            var mutateRequest = new MutateProfileRequest
            {
                DisplayName = "Updated Display Name",
                Bio = "Updated Bio",
                ImageUrl = "Updated link"
            };
            
            // Act
            profile.Update(mutateRequest);
            
            // Assert
            Assert.AreEqual(profile.Username, profile.Username);
            Assert.AreEqual(mutateRequest.DisplayName, profile.DisplayName);
            Assert.AreEqual(mutateRequest.Bio, profile.Bio);
            Assert.AreEqual(mutateRequest.ImageUrl, profile.ImageUrl);
        }

        private static Profile CreateProfile()
        {
            return Profile.Create(new CreateProfileRequest()
            {
                Username = "Username",
                Bio = "Bio",
                DisplayName = "Display Name",
                ImageUrl = "link"
            });
        }
    }
}