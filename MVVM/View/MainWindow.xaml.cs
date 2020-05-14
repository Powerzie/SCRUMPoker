using System.Windows;

namespace SPWPF.MVVM.VIew
{
    public partial class MainWindow : Window
    {

     

        public MainWindow()
        {
            InitializeComponent();

            windowControlPanel.ButtonClose_MouseClick_Handler += ((obj , obj2) => { this.Close(); });
            windowControlPanel.ButtonMinimize_MouseClick_Handler += ((obj, obj2) => { this.WindowState = WindowState.Minimized; });

          

         

        }
        private void OpenRegistrationMenu()
        {
            RegisterGrid.Visibility = Visibility.Visible;
            LoginGrid.Visibility = Visibility.Collapsed;
            EmailCodeConfirmGrid.Visibility = Visibility.Collapsed;
        }
        private void OpenLoginMenu()
        {
            LoginGrid.Visibility = Visibility.Visible;
            RegisterGrid.Visibility = Visibility.Collapsed;

        }
        private void OpenMainWindowMenu()
            {
            spLoginRegistrationWindow.Visibility = Visibility.Visible;
            RegisterGrid.Visibility = Visibility.Visible;
            LoginGrid.Visibility = Visibility.Collapsed;
            EmailCodeConfirmGrid.Visibility = Visibility.Collapsed;
           }
        private void OpenEmailVerification()
        {
            EmailCodeConfirmGrid.Visibility = Visibility.Visible;
            RegisterGrid.Visibility = Visibility.Collapsed;
        }
        private void OpenGameMenu()
        {
            
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

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            OpenEmailVerification();
        }

        private void CreateRoom_Click(object sender, RoutedEventArgs e)
        {
            CreateJoinGrid.Visibility = Visibility.Collapsed;
            CreateGrid.Visibility = Visibility.Visible;
        }

        private void JoinRoom_Click(object sender, RoutedEventArgs e)
        {
            CreateJoinGrid.Visibility = Visibility.Collapsed;
            JoinGrid.Visibility = Visibility.Visible;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void JoinRoom_Click2(object sender, RoutedEventArgs e)
        {
            JoinGrid.Visibility = Visibility.Collapsed;
            CreateGrid.Visibility = Visibility.Collapsed;
            CreateJoinGrid.Visibility = Visibility.Visible;
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (RoomNameTextBox.Text.Length == 0)
            {
                CreateButton.Opacity = 0.5;
                CreateButton.IsEnabled = false;
            }
            else
            {
                CreateButton.Opacity = 1;
                CreateButton.IsEnabled = true;
            }
        }

        private void RoomNameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (RoomTextBox.Text.Length == 0)
            {
                JoinButton.Opacity = 0.5;
                JoinButton.IsEnabled = false;
            }
            else
            {
                JoinButton.Opacity = 1;
                JoinButton.IsEnabled = true;
            }
        }
    }
}
