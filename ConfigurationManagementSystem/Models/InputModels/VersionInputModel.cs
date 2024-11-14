namespace ConfigurationManagementSystem.Models.InputModels
{
    public class VersionInputModel
    {
        public Guid? Id { get; set; }

        public Guid ConfigurationId { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
