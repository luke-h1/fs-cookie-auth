using api.Models;

namespace api.Data;

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

  public User FindById(int id)
  {
    return _context.Users.FirstOrDefault(u => u.Id == id);
  }

  public User FindByEmail(string email)
  {
    return _context.Users.FirstOrDefault(u => u.Email == email);
  }
}
