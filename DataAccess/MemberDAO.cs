using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        MyDBContext _context;

        public MemberDAO(MyDBContext context)
        {
            _context = context;
        }

        public List<Member> GetMembers()
        {
            return _context.Users.ToList();
        }

        public Member? GetById(int memberId)
        {
            //return _context.Users.FirstOrDefault(m => m.MemberId == memberId);
            return null;
        }

        public Member? GetLogin(Member member)
        {
            var p = _context.Users.FirstOrDefault(m => m.Email == member.Email);
            return p;
        }

        public Boolean Create(Member member)
        {
            try
            {
                _context.Users.Add(member);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Boolean Delete(Member member)
        {
            try
            {
                _context.Users.Remove(member);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Boolean Update(Member member)
        {
            //try
            //{
            //    Member? memberUpdate = GetById(member.UserName);
            //    if (memberUpdate != null)
            //    {
            //        memberUpdate.Email = member.Email;
            //        memberUpdate.CompanyName = member.CompanyName;
            //        memberUpdate.City = member.City;
            //        memberUpdate.Country = member.Country;
            //        memberUpdate.Password = member.Password;
            //        _context.Members.Update(memberUpdate);
            //        _context.SaveChanges();
            //        return true;
            //    }
            //    return false;
            //}
            //catch (Exception e)
            //{
            //    return false;
            //}
            return false;
        }
    }
}
