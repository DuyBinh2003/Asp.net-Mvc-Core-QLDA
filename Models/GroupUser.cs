namespace DoAn.Models
{
    public class GroupUser
    {
        String id {  get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String UserId { get; set; }
        public String UserName { get; set; }
        public String UserEmail { get; set; }
        public String UserPassword { get; set; }
        
        GroupUser()
        {

        }
        GroupUser(string id, string name, string email, string password, string userId, string userName, string userEmail, string userPassword)
        {
            this.id = id;
            Name = name;
            Email = email;
            Password = password;
            UserId = userId;
            UserName = userName;
            UserEmail = userEmail;
            UserPassword = userPassword;
        }
    }
}
