namespace DevFreela.Core.Service
{
    public interface IAuthService
    {
        string GenerateJWTToken(string email, string role);
        string ComputeSha256Hash(string password);
    }
}
