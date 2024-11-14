using Databases.Entities;

namespace ConfigurationManagementSystem.Models.InputModels;

public class ConfigurationInputModel
{
    public Guid? Id { get; set; }

    public required string Name { get; set; }

    public List<HotKeyInputModel> HotKeys { get; set; } = [];
}
