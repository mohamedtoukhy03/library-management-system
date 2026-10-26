namespace LibraryManagementSystem.Repository;

using LibraryManagementSystem.Model;

public class InMemoryMemberRepository : IMemberRepository
{
    private readonly List<Member> _members = [];

    public void Add(Member member)
    {
        _members.Add(member);
    }

    public Member? GetById(int id) => _members.FirstOrDefault(member => member.Id == id);

    public IReadOnlyList<Member> GetAll() => _members.AsReadOnly();
}

