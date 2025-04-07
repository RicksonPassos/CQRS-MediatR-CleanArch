using CleanArch.Application.Members.Commands.Notifications;
using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using FluentValidation;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace CleanArch.Application.Members.Commands
{
    public sealed class CreateMemberCommand : MemberCommandBase
    {
        public class CreateMemeberCommandHandler : IRequestHandler<CreateMemberCommand, Member>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IValidator<CreateMemberCommand> _validator;
            private readonly IMediator _mediator;

            public CreateMemeberCommandHandler(IUnitOfWork unitOfWork, IValidator<CreateMemberCommand> validator, IMediator mediator)
            {
                _unitOfWork = unitOfWork;
                _validator = validator;
                _mediator = mediator;
            }

            public async Task<Member> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
            {
                _validator.ValidateAndThrow(request);

                var newMember = new Member(request.FirstName, request.LastName, request.Gender, request.Email, request.IsActive);
                await _unitOfWork.MemberRepository.AddMember(newMember);
                await _unitOfWork.CommitAsync();

                await _mediator.Publish(new MemberCreatedNotification(newMember), cancellationToken);

                return newMember;
            }
        }
    }

}
