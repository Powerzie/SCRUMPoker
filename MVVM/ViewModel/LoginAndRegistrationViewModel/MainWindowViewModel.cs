using SPWPF.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace SPWPF.MVVM.ViewModel.MainWindowViewModel
{
  
    public partial class MainWindowViewModel
    {
        private Service1Client Service { get; set; }


        #region PROPERTIES
        private string _emailEnterText { get; set; }
        private string _exceptionHelperText { get; set; }

        private SolidColorBrush _emailTextBoxForegroundBrush { get; set; }
        #region PRIVATE

        #endregion

        #region PUBLIC 
        public string EmailEnterText
        {
            get { return _emailEnterText; }
            set { _emailEnterText = value; OnPropertyChanged(nameof(EmailEnterText)); }
        }

        public string ExceptionHelperText { get { return _exceptionHelperText; } set { _exceptionHelperText = value;OnPropertyChanged(nameof(ExceptionHelperText)); } }

        public SolidColorBrush EmailTextBoxForegroundBrush
        {
            get { return _emailTextBoxForegroundBrush; }
            set { _emailTextBoxForegroundBrush = value; OnPropertyChanged(nameof(EmailTextBoxForegroundBrush)); }
        }
        #endregion

        #endregion

        #region COMMANDS&METHODS
        private void OnEmailTextChanged()
        {
         
        }
        private bool ValidateEmail()
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(EmailEnterText);
            if (!match.Success)
            {
                EmailTextBoxForegroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DDD80000"));
                return false;
            }
            else
            {
                EmailTextBoxForegroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DD000000"));
                return true;
            }
        }
    
        public ICommand RegistrationWindowAcceptButtonClick
        {
            
            get
            {
                return new DelegateClickCommand((pass) =>
                {
                    if (ValidateEmail())
                    {
                        ExceptionHelperText = "";
                    }
                    else
                    {
                        ExceptionHelperText = "Wrong Email Entered";
                    }


                });
            }
        }
        #endregion
    }

}
