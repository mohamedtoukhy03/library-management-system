namespace LibraryManagementSystem.Service;

using LibraryManagementSystem.Model;

public interface IMemberService
{
    Member RegisterMember(string name, MemberType type);
    IReadOnlyList<Member> GetAllMembers();
    Member? GetMemberById(int id);
}

