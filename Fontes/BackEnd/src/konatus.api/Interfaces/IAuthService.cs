using System;
using System.Threading.Tasks;
using konatus.api.ViewModels;

namespace konatus.api.Interfaces
{
    public interface IAuthService : IDisposable
    {
        Task<LoginResponseViewModel> RegisterUser(UserRegisterViewModel registerUser);

        Task<LoginResponseViewModel> Login(UserLoginViewModel loginUser);

        Task<bool> RecoverPassword(UserRegisterViewModel registerUser);

        Task<bool> ChangePassword(UserRegisterViewModel registerUser);
    }
}