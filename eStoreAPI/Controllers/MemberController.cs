using DTO.DTO.Request.Member;
using DTO.DTO.Response.Member;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {

        IMemberRepository memberRepository;

        public MemberController(IMemberRepository memberRepository)
        {
            this.memberRepository = memberRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MemberResponseDTO>> GetMembers()
        {
            var memberList = memberRepository.GetMembers();
            return Ok(memberList);
        }

        [HttpGet("{id}")]
        public ActionResult<MemberResponseDTO> GetMemberById(int id)
        {
            var member = memberRepository.GetMemberById(id);
            return Ok(member);
        }

        [HttpPost]
        public IActionResult AddMember(MemberAddDTO memberAddDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = memberRepository.AddMember(memberAddDTO);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id)
        {
            MemberUpdateDTO m = new MemberUpdateDTO()
            {
                MemberId = id,
            };
            var response = memberRepository.DeleteMember(m);
            return Ok(response);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateMember(int id, MemberUpdateDTO memberUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            memberUpdateDTO.MemberId = id;
            var response = memberRepository.UpdateMember(memberUpdateDTO);
            return Ok(response);
        }

       
    }
}
