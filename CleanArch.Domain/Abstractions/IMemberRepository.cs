using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Abstractions
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetMembers();
        Task<Member> GetMemberById(int id);
        Task<Member> AddMember(Member member);
        void UpdateMeber(Member member);
        Task<Member> DeleteMember(int memberId);
    }
}
