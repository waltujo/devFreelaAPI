using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataOk_Executed_ReturnProjectId()
        {
            var projectRepositoryMock = new Mock<IProjectRepository>();

            var createProjectCommand = new CreateProjectCommand
            {
                Title = "Título do teste",
                Description = "Descrição do teste",
                IdClient = 1,
                IdFreelancer = 2,
                TotalCost = 5000
            };

            var createProjectCommandHandler = new CreateProjectCommandHandler(projectRepositoryMock.Object);

            var id = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());

            Assert.True(id >= 0);

            projectRepositoryMock.Verify(pr => pr.CreateProjectAsync(It.IsAny<Project>()), Times.Once);

        }
    }
}
