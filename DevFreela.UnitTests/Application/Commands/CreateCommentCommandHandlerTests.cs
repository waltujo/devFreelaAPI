using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateCommentCommandHandlerTests
    {
        [Fact]
        public async Task InputCommentIsOk_Executed_ReturnComment()
        {
            //Arrange
            var projectRepositoryMock = new Mock<IProjectRepository>();

            var comentario = new CreateCommentCommand
            {
                Content = "Meu comentário é este aqui.",
                IdUser = 1,
                IdProject = 1
            };

            var createCommentCommandHandler = new CreateCommentCommandHandler(projectRepositoryMock.Object);
            
            //Act
            await createCommentCommandHandler.Handle(new CreateCommentCommand(), new CancellationToken());
            
            //Assert

            projectRepositoryMock.Verify(pr => pr.AddCommentAsync(It.IsAny<ProjectComment>()), Times.Once);
        }
    }
}
