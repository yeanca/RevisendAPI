using System;

namespace RevisendAPI.Data.BindingModel
{
    public class UserDTO
    {
        public UserDTO(string userId, long appId, string userName, string firstName, string lastName, string email, string role, string token)
        {
            UserId = userId;
            AppId = appId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            //DateCreated = dateCreated;
            Role = role;
            Token = token;
            //Team = team;
            //TeamId = teamId;
        }

        public string UserId { get; set; }
        public long AppId { get;set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //public DateTime DateCreated { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        //public string Team { get; set; }
        //public int TeamId { get; set; }
    }
}
