using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Members.Queries
{
    public class GetMemberByIdQuery : IRequest<Member>
    {
        public int Id { get; set; }

        public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, Member>
        {
            private readonly IMemberRepository _memberRepository;
            public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
            {
                _memberRepository = memberRepository;
            }
            public async Task<Member> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
            {
                var member = await _memberRepository.GetMemberById(request.Id);
                return member;
            }
        }
    }
}
