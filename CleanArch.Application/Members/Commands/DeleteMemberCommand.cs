using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Members.Commands
{
    public sealed class DeleteMemberCommand : IRequest<Member>
    {
        public int Id { get; set; }

        public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, Member>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteMemberCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<Member> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
            {
                var existingMember = await _unitOfWork.MemberRepository.GetMemberById(request.Id);
                if (existingMember == null)
                {
                    throw new Exception("Member not found");
                }
                var deleteMember = await _unitOfWork.MemberRepository.DeleteMember(existingMember.Id);
                await _unitOfWork.CommitAsync();
                return existingMember;
            }
        }
    }
}
