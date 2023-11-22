namespace Tournament_.NET_2023.Interface;

public interface IUserManagementService
{
    void ChangePassword(string userId, string newPassword);
    void ChangeEmail(string userId, string newEmail);
    void DeleteUser(string userId);
    void DeactivateUser(string userId);
    void ReactivateUser(string userId);
}