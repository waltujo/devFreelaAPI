using System;

namespace DevFreela.Application.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public ProjectDetailsViewModel(int id, string description, string title, decimal totalCost, DateTime? startedAt, DateTime? finishedAt, string clientFullName, string freelancerFullName)
        {
            Id = id;
            Description = description;
            Title = title;
            TotalCost = totalCost;
            StartedAt = startedAt;
            FinishedAt = finishedAt;
            ClientFullName = clientFullName;
            FreelancerFullName = freelancerFullName;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public string Title { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? FinishedAt { get; private set; }
        public decimal TotalCost { get; private set; }
        public string ClientFullName { get; private set; }
        public string FreelancerFullName { get; private set; }
    }
}
