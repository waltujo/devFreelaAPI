using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllSkillsCommandHandlerTests
    {
        [Fact]
        public async Task IfExistSkill_Executed_ReturnSkillOrNull()
        {
            //Arrange

            var skillList = new List<SkillDTO>
            {
                new SkillDTO { Id = 1, Description = "Descrição Skill One"},
                new SkillDTO { Id = 2, Description = "Descrição Skill Two"},
                new SkillDTO { Id = 3, Description = "Descrição Skill Three"},
            };

            var skillRepositoryMock = new Mock<ISkillRepository>();
            skillRepositoryMock.Setup(pr => pr.GetAllAsync().Result).Returns(skillList);

            var getAllSkillsQuery = new GetAllSkillsQuery();
            var getAllSkillQueryHandler = new GetAllSkillsQueryHandler(skillRepositoryMock.Object);
            //Action
            var skills = await getAllSkillQueryHandler.Handle(getAllSkillsQuery, new CancellationToken());
            //Assert
            Assert.NotNull(skillList);
            Assert.NotEmpty(skillList);
            Assert.Equal(skillList.Count, skills.Count);

            skillRepositoryMock.Verify(pr => pr.GetAllAsync().Result, Times.Once);
        }   
    }
}
