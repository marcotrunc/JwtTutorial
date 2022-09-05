namespace JwtTutorial
{
    public class User
    {
        public string Username { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;

        //refreshToken part
        public string RefreshToken { get; set; } = String.Empty;

        public DateTime TokenCreated { get; set; }

        public DateTime TokenExpires { get; set; }

    }
}
