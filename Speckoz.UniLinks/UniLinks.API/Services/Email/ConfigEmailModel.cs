namespace UniLinks.API.Services.Email
{
    public class ConfigEmailModel
    {
        public string Domain { get; set; }
        public int Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}