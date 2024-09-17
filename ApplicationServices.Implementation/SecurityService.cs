using ApplicationServices.Interfaces;

namespace ApplicationServices.Implementation
{
    public class SecurityService : ISecurityService
    {
        public bool IsCurrenUserAdmin { get; }

        public string[] CurrentUserPermissions { get; }
    }
}
