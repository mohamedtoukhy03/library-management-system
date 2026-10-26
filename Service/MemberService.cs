namespace LibraryManagementSystem.Service;

using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository;
using LibraryManagementSystem.Validation;

public class MemberService(IMemberRepository repository, IMemberValidator validator) : IMemberService
{
    private readonly IMemberRepository _repository = repository;
    private readonly IMemberValidator _validator = validator;
    private int _nextMemberId = 1;

    public Member RegisterMember(string name, MemberType type)
    {
        var validation = _validator.Validate(name, type);
        if (!validation.IsValid)
            throw new ArgumentException(validation.ErrorMessage);

        var member = new Member(_nextMemberId++, name.Trim(), type);
        _repository.Add(member);
        return member;
    }

    public IReadOnlyList<Member> GetAllMembers() => _repository.GetAll();

    public Member? GetMemberById(int id) => _repository.GetById(id);
}
