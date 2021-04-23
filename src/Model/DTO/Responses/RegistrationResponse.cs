using taskAPI.Domain;
using Microsoft.AspNetCore.Identity;

namespace taskAPI.Model.DTO.Responses
{
    public class RegistrationResponse : AuthResult
    {
        public IdentityUser User {get;set;}
    }
}