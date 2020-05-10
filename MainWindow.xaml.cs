using System.Windows;

namespace SPWPF
{
    public partial class MainWindow : Window
    {
        private ServiceReference1.Service1Client Service;
        public MainWindow()
        {
            InitializeComponent();
            Service = new ServiceReference1.Service1Client();

            windowControlPanel.ButtonClose_MouseClick_Handler += ((obj , obj2) => { this.Close(); });
            windowControlPanel.ButtonMinimize_MouseClick_Handler += ((obj, obj2) => { this.WindowState = WindowState.Minimized; });
        }

     
    }
}
