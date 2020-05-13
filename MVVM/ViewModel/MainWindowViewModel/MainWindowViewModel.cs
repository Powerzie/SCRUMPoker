using SPWPF.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using SPWPF.Hashing;


namespace SPWPF.MVVM.ViewModel.MainWindowViewModel
{
    class DelegateClickCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public DelegateClickCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {
        
        private Service1Client Service { get; set; }
        private Hasher hasher;
        public MainWindowViewModel()
        {
            EmailTextBoxForegroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DD000000"));
            LoginTextBoxForegroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DD000000"));
            OpenLoginAndRegisterMenu();
            currentWindowState = WindowStates.MainWindow;
            ChangeMainWindowState(currentWindowState);
            hasher = new Hasher();
            Service = new Service1Client();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #region PROPERTIES

        #region PRIVATE

        #endregion

        #region PUBLIC 

        #endregion

        #endregion

        #region COMMANDS&METHODS
        private void CloseAllWindows()
        {
            EmailCodeConfirmGrid = Visibility.Hidden;
            RegisterGridVisability = Visibility.Hidden;
        }
        private void OpenLoginAndRegisterMenu  ()
        {
            LoginRegisterVisability = Visibility.Visible;
            LoginWindowVisability = Visibility.Visible;
            EmailCodeConfirmGrid = Visibility.Collapsed;
            RegisterGridVisability = Visibility.Collapsed;
        }
        private void OpenRegistrationWindow()
        {
            CloseAllWindows();
            RegisterGridVisability = Visibility.Visible;
        }
        private void OpenMailVerificationWindow()
        {
            CloseAllWindows();
            EmailCodeConfirmGrid = Visibility.Visible;
        }

        public ICommand MainWindow_SendMessageButtonClick
        {
            get
            {
                return new DelegateClickCommand((obj) =>
                {
                   
                });
            }
        }
        #endregion
    }
}
