namespace LibraryManagementSystem.Repository;

using LibraryManagementSystem.Model;

public interface IMemberRepository
{
    void Add(Member member);
    Member? GetById(int id);
    IReadOnlyList<Member> GetAll();
}

