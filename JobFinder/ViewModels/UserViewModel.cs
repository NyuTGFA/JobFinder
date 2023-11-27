using JobFinder.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobFinder.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        HttpClient client;
        JsonSerializerOptions _serializerOptions;
        string baseURL = "http://www.empleo.somee.com/api/User";

        private int _resetCode;
        public int ResetCode
        {
            get { return _resetCode; }
            set
            {
                if (_resetCode != value)
                {
                    _resetCode = value;
                    OnPropertyChanged(nameof(ResetCode));
                }
            }
        }

        private User _user = new User();
        public User User
        {
            get { return _user; }
            set
            {
                if (_user != value)
                {
                    _user = value;
                    OnPropertyChanged(nameof(User));
                }
            }
        }

        private UserVm _userVm = new UserVm();
        public UserVm UserVm
        {
            get { return _userVm; }
            set
            {
                if (_userVm != value)
                {
                    _userVm = value;
                    OnPropertyChanged(nameof(UserVm));
                }
            }
        }

        public UserViewModel()
        {
            client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
        }

        public ICommand Login => new Command<MainPage>(async (MainPage mainPage) =>
        {
            string url = baseURL;
            if (string.IsNullOrEmpty(User.phoneNumber))
            {
                mainPage.Error("Phone Number is required");
                return;
            }

            if (string.IsNullOrEmpty(User.password))
            {
                mainPage.Error("Password is required");
                return;
            }
            User newUser = new User
            {
                phoneNumber = User.phoneNumber,
                password = User.password
            };


            string json = JsonSerializer.Serialize<User>(newUser, _serializerOptions);
            StringContent contenido = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{url}/login", contenido);
            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    User = await JsonSerializer.DeserializeAsync<User>(responseStream, _serializerOptions);
                    OnPropertyChanged(nameof(User));
                    mainPage.GoToFeed(User._id);
                }
            }
            else
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    ErrorResponse data = await JsonSerializer.DeserializeAsync<ErrorResponse>(responseStream, _serializerOptions);
                    mainPage.Error(data.message);
                }
            }
        });

        public ICommand ResetPassword => new Command<ResetPassword>(async (ResetPassword resetPasswordPage) =>
        {
            string url = baseURL;
            if (string.IsNullOrEmpty(UserVm.password))
            {
                resetPasswordPage.Error("Phone Number is required");
                return;
            }

            if (string.IsNullOrEmpty(UserVm.confirmPassword))
            {
                resetPasswordPage.Error("Password is required");
                return;
            }

            if (UserVm.password != UserVm.confirmPassword)
            {
                resetPasswordPage.Error("Password must coincide");
                return;
            }

            if (UserVm.resetCode != ResetCode)
            {
                resetPasswordPage.Error("Invalid reset code");
                return;
            }

            User user = new User
            {
                phoneNumber = UserVm.phoneNumber,
                password = UserVm.password
            };

            string json = JsonSerializer.Serialize<User>(user, _serializerOptions);
            StringContent contenido = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{url}/resetPassword", contenido);
            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    User = await JsonSerializer.DeserializeAsync<User>(responseStream, _serializerOptions);
                    OnPropertyChanged(nameof(User));
                    resetPasswordPage.GoToLogin();
                }
            }
            else
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    ErrorResponse data = await JsonSerializer.DeserializeAsync<ErrorResponse>(responseStream, _serializerOptions);
                    resetPasswordPage.Error(data.message);
                }
            }
        });

        public ICommand RequestResetPassword => new Command<ForgotPassword>(async (ForgotPassword forgotPasswordPage) =>
        {
            Random random = new Random();
            int randomCode = random.Next(10000, 100000);
            ResetCode = randomCode;
            OnPropertyChanged(nameof(ResetCode));
            forgotPasswordPage.RequestResetPassword(randomCode);
        });

        public ICommand Register => new Command<RegisterPage>(async (RegisterPage registerPage) =>
        {
            string url = baseURL;

            if (string.IsNullOrEmpty(UserVm.name))
            {
                registerPage.Error("Name is required");
                return;
            }

            if (string.IsNullOrEmpty(UserVm.phoneNumber))
            {
                registerPage.Error("Phone Number is required");
                return;
            }

            if (string.IsNullOrEmpty(UserVm.introduction))
            {
                registerPage.Error("Introduction is required");
                return;
            }


            if (string.IsNullOrEmpty(UserVm.password))
            {
                registerPage.Error("Password is required");
                return;
            }

            if (string.IsNullOrEmpty(UserVm.confirmPassword))
            {
                registerPage.Error("Password is required");
                return;
            }

            if (UserVm.password != UserVm.confirmPassword)
            {
                registerPage.Error("Password must coincide");
                return;
            }

            User newUser = new User
            {
                name = UserVm.name,
                phoneNumber = UserVm.phoneNumber,
                introduction = UserVm.introduction,
                password = UserVm.password
            };

            string json = JsonSerializer.Serialize<User>(newUser, _serializerOptions);
            StringContent contenido = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{url}/register", contenido);
            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    User = await JsonSerializer.DeserializeAsync<User>(responseStream, _serializerOptions);
                    OnPropertyChanged(nameof(User));
                    registerPage.GoToLogin();
                }
            }
            else
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    ErrorResponse data = await JsonSerializer.DeserializeAsync<ErrorResponse>(responseStream, _serializerOptions);
                    registerPage.Error(data.message);
                }
            }
        });

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
