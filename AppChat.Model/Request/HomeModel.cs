using System;

namespace AppChat.Model.Request
{
    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class AddUserRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string nickname { get; set; }
        public bool sex { get; set; }
    }

    public class UserInitRequest
    {
        public Guid userid { get; set; }
    }
}
