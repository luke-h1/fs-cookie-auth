using api.Models;

namespace api.Data;

public interface IUserRepository
{
    User Create (User user);
   
    User FindById (int id);
    
    User FindByEmail (string email);
}
