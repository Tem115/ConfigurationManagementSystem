namespace ConfigurationManagementSystem.Models.OutputModels
{
    public class VersionOutputModel
    {
        public Guid Id { get; set; }

        public Guid ConfigurationId { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
