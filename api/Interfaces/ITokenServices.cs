using api.Model;

namespace api.Interfaces
{
    public interface ITokenServices
    {
         string CreateToken(AppUser appUser);
    }
}