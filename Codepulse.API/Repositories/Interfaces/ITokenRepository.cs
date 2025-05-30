using Codepulse.Model;

namespace Codepulse.API.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJwtToken(AuthUser user, List<string> roles);
    }
}
