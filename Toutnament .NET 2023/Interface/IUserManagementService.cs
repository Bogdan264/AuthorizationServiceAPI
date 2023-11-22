using Microsoft.AspNetCore.Mvc;
using static Toutnament_.NET_2023.Models.Management;

namespace Tournament_.NET_2023.Interface;

public interface IUserManagementService
{
    Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model);
    Task<IActionResult> ChangeEmail([FromBody] ChangeEmailModel model);
    Task<IActionResult> DeleteUser([FromBody] DeleteUserModel model);
    //void DeactivateUser(string userId);
    //void ReactivateUser(string userId);
}