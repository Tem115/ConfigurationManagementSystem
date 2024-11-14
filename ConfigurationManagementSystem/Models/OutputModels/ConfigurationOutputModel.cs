namespace ConfigurationManagementSystem.Models.OutputModels;

public class ConfigurationOutputModel
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public DateTime CreateDate { get; set; }

    public List<HotKeyOutputModel> HotKeys { get; set; } = [];
}
