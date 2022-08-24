namespace TMDT.Models.CustomModel
{
    public class UserInfo
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }
      
        public int RoleID { get; set; }

        public string RoleName { get; set; }

        public System.DateTime CreatedDate { get; set; }
    }
}