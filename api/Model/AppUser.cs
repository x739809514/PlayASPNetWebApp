using Microsoft.AspNetCore.Identity;

namespace api.Model
{
    public class AppUser: IdentityUser
    {
        public List<Portfolio> portfolios {get;set;} = new List<Portfolio>();
        
    }
}