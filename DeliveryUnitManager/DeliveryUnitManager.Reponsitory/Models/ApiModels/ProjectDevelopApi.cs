using DeliveryUnitManager.Reponsitory.Models.BankingQuestionInterview;

namespace DeliveryUnitManager.Reponsitory.Models.ApiModels
{
    public class ProjectDevelopApi
    {
        public long Id { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Technology { get; set; }
        public ProjectDevelopApi(ProjectDevelop project)
        {
            Id = project.Id;
            Code = project.Code;
            Name = project.Name;
            Description = project.Description;
            Technology = project.Technology;
        }
    }
}
