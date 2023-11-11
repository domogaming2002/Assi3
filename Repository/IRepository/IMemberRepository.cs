using DTO.DTO.Request.Member;
using DTO.DTO.Response.Member;

namespace Repository.IRepository
{
    public interface IMemberRepository
    {
        bool AddMember(MemberAddDTO m);
        MemberResponseDTO? GetMemberById(int id);
        bool DeleteMember(MemberUpdateDTO m);
        bool UpdateMember(MemberUpdateDTO m);
        List<MemberResponseDTO> GetMembers();
        MemberResponseDTO LoginMember(MemberLogin m);

    }
}
