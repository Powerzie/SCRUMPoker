using EASendMail;
using SPWPF.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SPWPF.MVVM.ViewModel.MainWindowViewModel
{

    public partial class MainWindowViewModel
    {
        private UserDTO CurrentLoginedUser;
        private enum WindowStates { MainWindow, GameWindow }

        #region PROPERTIES
        #region PRIVATE
        private string _emailEnterText { get; set; }
        private string _loginEnterText { get; set; }
        private string _exceptionHelperText { get; set; }
        private string _loginWindowLoginEnterText { get; set; }
        private string _verificationCodeTextBox { get; set; }
        private string verCode { get; set; }
        private string passwordHash { get; set; }

        private SolidColorBrush _emailTextBoxForegroundBrush { get; set; }
        private SolidColorBrush _loginTextBoxForegroundBrush { get; set; }

        private Visibility _emailCodeConfirmGrid { get; set; }
        private Visibility _registerGridVisability { get; set; }
        private Visibility _loginRegisterVisability { get; set; }
        private Visibility _gameWindowVisability { get; set; }
        private Visibility _loginWindowVisability { get; set; }
        
        private WindowStates currentWindowState;

        private int _windowWidth { get; set; }
        private int _windowHeight { get; set; }

        private int _minHeightRange { get; set; }
        private int _maxHeightRange { get; set; }

        private int _minWidthRange { get; set; }
        private int _maxWidthRange { get; set; }


        #endregion

        #region PUBLIC 
        public string EmailEnterText
        {
            get { return _emailEnterText; }
            set { _emailEnterText = value; OnPropertyChanged(nameof(EmailEnterText)); }
        }
        public string LoginEnterText
        {
            get { return _loginEnterText; }
            set { _loginEnterText = value; OnPropertyChanged(nameof(LoginEnterText)); }
        }
        public string LoginWindowLoginEnterText
        {
            get { return _loginEnterText; }
            set { _loginEnterText = value; OnPropertyChanged(nameof(LoginWindowLoginEnterText)); }
        }


        public string ExceptionHelperText { get { return _exceptionHelperText; }
            set { _exceptionHelperText = value;OnPropertyChanged(nameof(ExceptionHelperText)); } }
        public string VerificationCodeTextBox
        {
            get { return _verificationCodeTextBox; }
            set { _verificationCodeTextBox = value; OnPropertyChanged(nameof(VerificationCodeTextBox)); }
        }


        
        public SolidColorBrush EmailTextBoxForegroundBrush
        {
            get { return _emailTextBoxForegroundBrush; }
            set { _emailTextBoxForegroundBrush = value; OnPropertyChanged(nameof(EmailTextBoxForegroundBrush)); }
        }
        public SolidColorBrush LoginTextBoxForegroundBrush
        {
            get { return _loginTextBoxForegroundBrush; }
            set { _loginTextBoxForegroundBrush = value; OnPropertyChanged(nameof(LoginTextBoxForegroundBrush)); }
        }

        public Visibility EmailCodeConfirmGrid { get { return _emailCodeConfirmGrid; }
            set { _emailCodeConfirmGrid = value; OnPropertyChanged(nameof(EmailCodeConfirmGrid)); } }
        public Visibility RegisterGridVisability { get { return _registerGridVisability; }
            set { _registerGridVisability = value; OnPropertyChanged(nameof(RegisterGridVisability)); } }
        public Visibility LoginRegisterVisability
        {
            get { return _loginRegisterVisability; }
            set { _loginRegisterVisability = value; OnPropertyChanged(nameof(LoginRegisterVisability)); }
        }
        public Visibility GameWindowVisability
        {
            get { return _gameWindowVisability; }
            set { _gameWindowVisability = value; OnPropertyChanged(nameof(GameWindowVisability)); }
        }
        public Visibility LoginWindowVisability
        {
            get { return _loginWindowVisability; }
            set { _loginWindowVisability = value; OnPropertyChanged(nameof(LoginWindowVisability)); }
        }

        public int WindowHeight { get { return _windowHeight; } set { _windowHeight = value; OnPropertyChanged(nameof(WindowHeight)); } }
        public int WindowWidth { get { return _windowWidth; } set { _windowWidth = value; OnPropertyChanged(nameof(WindowWidth)); } }

        public int MinHeightRange { get { return _minHeightRange; } set { _minHeightRange = value; OnPropertyChanged(nameof(MinHeightRange)); } }
        public int MaxHeightRange { get { return _maxHeightRange; } set { _maxHeightRange = value; OnPropertyChanged(nameof(MaxHeightRange)); } }
        public int MinWidthRange { get { return _minWidthRange; } set { _minWidthRange = value; OnPropertyChanged(nameof(MinWidthRange)); } }
        public int MaxWidthRange { get { return _maxWidthRange; } set { _maxWidthRange = value; OnPropertyChanged(nameof(MaxWidthRange)); } }



        #endregion

        #endregion

        #region COMMANDS&METHODS
        private bool ValidateEmail()
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(EmailEnterText);
            if (!match.Success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void ResetAllColors()
        {
            LoginTextBoxForegroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DD000000"));
            EmailTextBoxForegroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DD000000"));
        }
        private bool SendVerificationCode()
        {
            try
            {
                Random random = new Random();
                int code = random.Next(1000, 99999);

                SmtpServer server = new SmtpServer("smtp.gmail.com")
                {
                    Port = 465,
                    ConnectType = SmtpConnectType.ConnectSSLAuto,
                    User = "scrumpokerver@gmail.com",
                    Password = "bodik20130012"
                };
                SmtpMail message = new SmtpMail("TryIt")
                {
                    From = server.User,
                    To = EmailEnterText,
                    Subject = "Verification code from SPO",
                    TextBody = code.ToString(),
                    Priority = MailPriority.High
                };
                SmtpClient client = new SmtpClient();

                client.SendMail(server, message);
                verCode = hasher.GetHashedString(code.ToString());
            }
            catch
            {
                return false;
            }
            return true;
        }
        private void ChangeMainWindowState(WindowStates state)
        {
            currentWindowState = state;
            switch (currentWindowState)
            {
                case WindowStates.MainWindow:

                    WindowHeight = 505;
                   MaxHeightRange = WindowHeight + 30;
                    MinHeightRange = WindowHeight - 30;

                    WindowWidth = 365;
                    MaxWidthRange = WindowWidth + 30;
                    MinWidthRange = WindowWidth - 30;


                    break;
                case WindowStates.GameWindow:
                    WindowHeight = 800;
                    MaxHeightRange = WindowHeight + 90;
                    MinHeightRange = WindowHeight - 90;

                    WindowWidth = 1500;
                    MaxWidthRange = WindowWidth + 90;
                    MinWidthRange = WindowWidth - 90;

                    break;
                default:
                    break;
            }
        }
        private void OpenGameMenu()
        {
            GameWindowVisability = Visibility.Visible;
            LoginRegisterVisability = Visibility.Collapsed;
            currentWindowState = WindowStates.GameWindow;
            ChangeMainWindowState(currentWindowState);
        }





        public ICommand ConfirmEmailCode
        {
            get
            {
                return new DelegateClickCommand((obj) =>
                {
                    if (hasher.GetHashedString(VerificationCodeTextBox) == verCode)
                    {
                       if( Service.RegisterNewUser(LoginEnterText, EmailEnterText, passwordHash))
                        {
                            OpenLoginAndRegisterMenu();
                        }
                    }
                    else
                    {
                        ExceptionHelperText = "Invalid code";
                    }
                });
            }
        }
        public ICommand RegistrationWindowAcceptButtonClick
        {
            get
            {
                return new DelegateClickCommand((pass) =>
                {
                    passwordHash = hasher.GetHashedString((pass as PasswordBox).Password);
                    pass = null;
                    ResetAllColors();
                    if (!Service.IsUserExistByLogin(LoginEnterText))
                    {

                        if (ValidateEmail())
                        {
                            ExceptionHelperText = "";
                            if (!Service.IsUserExistByLogin(EmailEnterText))
                            {
                             if(   SendVerificationCode())
                                {
                                    OpenMailVerificationWindow();
                                }
                             else
                                {
                                    ExceptionHelperText = "Wrong email";
                                    EmailTextBoxForegroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DDD80000"));
                                }

                            }
                            else
                            {
                                ExceptionHelperText = "User with this email is alredy exist";
                                EmailTextBoxForegroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DDD80000"));
                            }

                        }
                        else
                        {
                            EmailTextBoxForegroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DDD80000"));
                            ExceptionHelperText = "Wrong Email Entered";
                        }
                    }
                    else
                    {
                        ExceptionHelperText = "User with this user name is alredy exist";
                        LoginTextBoxForegroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DDD80000"));
                    }


                });
            }
        }
        public ICommand MainWindowLoginButtonClick
        {
            get
            {
                return new DelegateClickCommand((pass) =>
                {
                    CurrentLoginedUser = Service.LoginByLogin(hasher.GetHashedString((pass as PasswordBox).Password), LoginWindowLoginEnterText);
                    if(CurrentLoginedUser==null)
                    {
                        CurrentLoginedUser = Service.LoginByEmail(hasher.GetHashedString((pass as PasswordBox).Password), LoginWindowLoginEnterText);
                    }
                    if(CurrentLoginedUser==null)
                    {
                        ExceptionHelperText = "Wrong login or password";
                        return;
                    }
                    else
                    {
                        OpenGameMenu();
                    }


                });
            }
        }
        
        #endregion
    }

}
