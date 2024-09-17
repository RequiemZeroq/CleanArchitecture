namespace ApplicationServices.Interfaces
{
    public interface ISecurityService
    {
        bool IsCurrenUserAdmin { get; }
        string[] CurrentUserPermissions { get; }
    }
}
