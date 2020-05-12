using System.Windows;

namespace SPWPF.MVVM.VIew
{
    public partial class MainWindow : Window
    {
        private ServiceReference1.Service1Client Service;

       

        private enum WindowStates {MainWindow,GameWindow }

        private WindowStates currentWindowState;

        private int minHeightRange;
        private int maxHeightRange;

        private int minWidthRange;
        private int maxWidthRange;

        public MainWindow()
        {
            InitializeComponent();
            Service = new ServiceReference1.Service1Client();

            windowControlPanel.ButtonClose_MouseClick_Handler += ((obj , obj2) => { this.Close(); });
            windowControlPanel.ButtonMinimize_MouseClick_Handler += ((obj, obj2) => { this.WindowState = WindowState.Minimized; });

            currentWindowState = WindowStates.MainWindow;

            minHeightRange = 30;
            maxHeightRange = 30;

            minWidthRange = 30;
            maxWidthRange = 30;


        }
        private void ChangeMainWindowState(WindowStates state)
        {
            currentWindowState = state;
            switch (currentWindowState)
            {
                case WindowStates.MainWindow:

                    Window.Height = 505;
                    Window.MaxHeight = Window.Height + minHeightRange;
                    Window.MinHeight = Window.Height - maxHeightRange;

                    Window.Width = 365;
                    Window.MaxWidth = Window.Height + minWidthRange;
                    Window.MinWidth = Window.Height - maxWidthRange;


                    break;
                case WindowStates.GameWindow:


                    break;
                default:
                    break;
            }
        }
        private void OpenRegistrationMenu()
        {
            RegisterGrid.Visibility = Visibility.Visible;
            LoginGrid.Visibility = Visibility.Collapsed;
        }
        private void OpenLoginMenu()
        {
            LoginGrid.Visibility = Visibility.Visible;
            RegisterGrid.Visibility = Visibility.Collapsed;
        }
        private void OpenMainWindowMenu()
            {
            spLoginRegistrationWindow.Visibility = Visibility.Visible;
            RegisterGrid.Visibility = Visibility.Collapsed;
            LoginGrid.Visibility = Visibility.Visible;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenRegistrationMenu();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenMainWindowMenu();
        }

        private void PassChanged(object sender, RoutedEventArgs e)
        {
            if (RegisterPasswordBox1.Password != RegisterRepeatedPasswordBox2.Password)
            {
                ExceptionHint.Content = "Password is not repeated";
                AcceptButton.Opacity = 0.5;
                AcceptButton.IsEnabled = false;
            }
            else
            {
                AcceptButton.Opacity = 1;
                ExceptionHint.Content = "";
                AcceptButton.IsEnabled = true;
                  
            }
        }
    }
}
