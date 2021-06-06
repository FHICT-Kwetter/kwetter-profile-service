using ProfileService.Domain.Exceptions.Base;

namespace ProfileService.Domain.Exceptions
{
    public class ProfileCreationException : BaseException
    {
        public ProfileCreationException(string message) : base(message)
        {
        }
    }
}