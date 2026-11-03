namespace LibraryManagementSystem.Service;

using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository;

public class MemberService(IMemberRepository repository) : IMemberService
{
    private readonly IMemberRepository _repository = repository;
    private int _nextMemberId = 1;

    public string RegisterMember(string name, MemberType type)
    {
        var member = new Member(_nextMemberId++, name, type);
        _repository.Add(member);
        return $"Member '{member.Name}' registered successfully.";
    }

    public IReadOnlyList<Member> GetAllMembers() => _repository.GetAll();

    public Member? GetMemberById(int id) => _repository.GetById(id);
}
