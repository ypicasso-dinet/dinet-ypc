using Toshi.Backend.Domain.DTO.Auth;

namespace Toshi.Backend.Domain
{
    public class SessionStorage
    {
        private UserContainerDTO? _user;

        public UserContainerDTO? GetUser()
        {
            return _user;
        }

        public void SetUser(UserContainerDTO user)
        {
            _user = user;
        }
    }
}
