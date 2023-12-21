using jwt.Models;

namespace jwt.Repositories;

public interface IUserRepository
{
    User Create(User user);
    User GetByPhone(string phone);
    User GetById(int id);
}
