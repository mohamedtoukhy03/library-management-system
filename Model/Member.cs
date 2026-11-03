namespace LibraryManagementSystem.Model;

public class Member
{
    public int Id { get; set; }
    public string Name { get; set; }
    public MemberType Type { get; set; }

    public int MaxLoans => Type switch
    {
        MemberType.Regular => 3,
        MemberType.Premium => 5,
        MemberType.VIP => 10,
        _ => 3
    };

    public Member(int id, string name, MemberType type)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Id = id;
        Name = name.Trim();
        Type = type;
    }
}
