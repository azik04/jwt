using jwt.Models;

namespace jwt.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserContext _context;

    public UserRepository(UserContext context)
    {
        _context = context;
    }

    public User Create(User user)
    {
        _context.Users.Add(user);
        user.Id = _context.SaveChanges();

        return user;
    }

    public User GetByPhone(string phone)
    {
        return _context.Users.FirstOrDefault(u => u.Phone == phone);
    }

    public User GetById(int id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }
}
