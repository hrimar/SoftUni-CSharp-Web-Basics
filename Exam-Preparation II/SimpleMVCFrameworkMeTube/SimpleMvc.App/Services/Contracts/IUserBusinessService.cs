namespace SimpleMvc.App.Services.Contracts
{
    public interface IUserBusinessService
    {
        bool Validate(string username, string password, string confirmPassword, string email);

        bool Register(string username, string password, string confirmPassword, string email);

        bool Login(string username, string password);

        object GetUserbyUssername(string username);
    }
}
