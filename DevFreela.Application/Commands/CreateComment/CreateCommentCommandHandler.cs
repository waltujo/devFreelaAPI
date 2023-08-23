using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Unit>
    {
        private readonly IProjectCommentRepository _projectCommentRepository;
        public CreateCommentCommandHandler(IProjectCommentRepository projectCommentRepository)
        {
            _projectCommentRepository = projectCommentRepository;
        }

        public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);

            await _projectCommentRepository.CreateCommentAsync(comment);

            return Unit.Value;
        }
    }
}
