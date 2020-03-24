namespace YTS.WebApi
{
    public interface IUserService
    {
        bool IsValid(LoginRequestDTO req);
    }
}
