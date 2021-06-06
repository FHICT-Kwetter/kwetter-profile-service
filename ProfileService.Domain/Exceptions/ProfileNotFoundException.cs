using ProfileService.Domain.Exceptions.Base;

namespace ProfileService.Domain.Exceptions
{
    public class ProfileNotFoundException : BaseException
    {
        public ProfileNotFoundException(string message) : base(message)
        {
        }
    }
}