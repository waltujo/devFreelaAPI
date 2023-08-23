using System;

namespace DevFreela.Application.ViewModels
{
    public class ProjectViewModel
    {
        public ProjectViewModel(int Id, string title, DateTime createdAt)
        {
            Id = Id;
            Title = title;
            CreatedAt = createdAt;
        }

        public int Id { get; set; }
        public string Title { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}
