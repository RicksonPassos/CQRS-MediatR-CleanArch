using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Members.Commands
{
    public sealed class CreateMemberCommand : MemberCommandBase
    {
        public class CreateMemeberCommandHandler : IRequestHandler<CreateMemberCommand, Member>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CreateMemeberCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Member> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
            {
                var newMember = new Member(request.FirstName, request.LastName, request.Gender, request.Email, request.IsActive);

                await _unitOfWork.MemberRepository.AddMember(newMember);
                await _unitOfWork.CommitAsync();
                return newMember;
            }
        }
    }

}
