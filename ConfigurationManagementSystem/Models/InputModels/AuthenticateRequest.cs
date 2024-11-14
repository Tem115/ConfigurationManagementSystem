namespace ConfigurationManagementSystem.Models.InputModels
{
    public class AuthenticateRequest
    {
        public required string Login { get; set; }

        public required string Password { get; set; }
    }
}
