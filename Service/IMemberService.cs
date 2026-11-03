namespace LibraryManagementSystem.Service;

using LibraryManagementSystem.Model;

public interface IMemberService
{
    string RegisterMember(string name, MemberType type);
    IReadOnlyList<Member> GetAllMembers();
    Member? GetMemberById(int id);
}

