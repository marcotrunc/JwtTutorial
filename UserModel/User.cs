using System.ComponentModel.DataAnnotations;

namespace JwtTutorial.UserModel
{
    public class User
    {
        [Key]
        public string Username { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public Employee? Employee { get; set; } = UserModel.Employee.Customer;


        //refreshToken part
        public string RefreshToken { get; set; } = String.Empty;

        public DateTime TokenCreated { get; set; }

        public DateTime TokenExpires { get; set; }

    }


}
