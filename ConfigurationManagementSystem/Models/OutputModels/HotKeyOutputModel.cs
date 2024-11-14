using ConfigurationManagementSystem.Models.InputModels;

namespace ConfigurationManagementSystem.Models.OutputModels;

public class HotKeyOutputModel
{
    public Guid Id { get; set; }

    public required string Hex { get; set; }

    public required CommandOutputModel Command { get; set; }
}