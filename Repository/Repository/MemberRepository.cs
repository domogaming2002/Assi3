using AutoMapper;
using BusinessObject.Models;
using DataAccess;
using DTO.DTO.Request.Member;
using DTO.DTO.Response.Member;
using Repository.IRepository;

namespace Repository.Repository
{
    public class MemberRepository : IMemberRepository
    {
        MemberDAO _memberDAO;
        IMapper _mapper;

        public MemberRepository(MyDBContext context, IMapper mapper)
        {
            _memberDAO = new MemberDAO(context);
            _mapper = mapper;
        }

        public bool AddMember(MemberAddDTO m)
        {
            return _memberDAO.Create(_mapper.Map<Member>(m));
        }

        public bool DeleteMember(MemberUpdateDTO m)
        {
            return _memberDAO.Delete(_mapper.Map<Member>(m));
        }

        public MemberResponseDTO? GetMemberById(int id)
        {
            return _mapper.Map<MemberResponseDTO?>(_memberDAO.GetById(id));
        }

        public List<MemberResponseDTO> GetMembers()
        {
            return _mapper.Map<List<MemberResponseDTO>>(_memberDAO.GetMembers());
        }

        public MemberResponseDTO LoginMember(MemberLogin m)
        {
            return _mapper.Map<MemberResponseDTO>(_memberDAO.GetLogin(_mapper.Map<Member>(m)));
        }

        public bool UpdateMember(MemberUpdateDTO m)
        {
            return _memberDAO.Update(_mapper.Map<Member>(m));
        }
    }
}
