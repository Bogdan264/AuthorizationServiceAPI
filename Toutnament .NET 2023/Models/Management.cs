namespace Toutnament_.NET_2023.Models;

public class Management {
    public class ChangePasswordModel
    {
        public string UserId { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangeEmailModel
    {
        public string UserId { get; set; }
        public string NewEmail { get; set; }
    }

    public class DeleteUserModel
    {
        public string UserId { get; set; }
    }
}