using ProjekatASP.Domain;

namespace ProjekatASP.Api.Core
{
    public class JwtUser : IUser
    {
        public string Identity { get; set; }

        public int Id { get; set; }
        public IEnumerable<int> UseCaseIds { get; set; } = new List<int>();
        public string Email { get; set; }
    }

    public class AnonimousUser : IUser
    {
        public string Identity => "Anonymous";

        public int Id => 0;

        public IEnumerable<int> UseCaseIds => new List<int> { 18,5,7 };

        public string Email => "anonimous@gmail.com";
    }
}
