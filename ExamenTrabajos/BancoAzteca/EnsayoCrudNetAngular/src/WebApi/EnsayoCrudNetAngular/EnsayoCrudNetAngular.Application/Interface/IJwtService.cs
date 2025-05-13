namespace EnsayoCrudNetAngular.Application.Interface
{    public interface IJwtService
    {
        string GenerateToken(string userId, string role);
    }
}
