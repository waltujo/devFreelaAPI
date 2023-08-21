using DevFreela.Application.Commands.CreateUser;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
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
    public class CreateUserCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnUser()
        {
            //Arrange
            var projectRepositoryMock = new Mock<IProjectRepository>();
            var authServiceMock = new Mock<IAuthService>();

            var usuario = new CreateUserCommand
            {
                FullName = "Nome Completo",
                Email = "email@gmail.com",
                BirthDate = DateTime.Now,
                Role = "Client",
                Password = "password"

            };

            var createUserCommandHandler = new CreateUserCommandHandler(projectRepositoryMock.Object, authServiceMock.Object);

            //Act
            var user = await createUserCommandHandler.Handle(usuario, new CancellationToken());
            //Asset

            Assert.True(user >= 0);

            projectRepositoryMock.Verify(pr => pr.AddUserAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
