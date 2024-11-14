namespace ConfigurationManagementSystem.Models.InputModels
{
    public class HotKeyInputModel
    {
        public Guid? Id { get; set; }

        public required string Hex { get; set; }

        public required CommandInputModel Command { get; set; }
    }
}
