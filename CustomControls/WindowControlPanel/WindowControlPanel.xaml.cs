using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace StepChat.StepChatUI.CustomUIElement.WindowControlPanel
{
    public partial class WindowControlPanel : UserControl
    {
        public delegate void ButtonEnter(object sender, RoutedEventArgs e);

        public event ButtonEnter ButtonClose_MouseClick_Handler;
        public event ButtonEnter ButtonMaximize_MouseClick_Handler;
        public event ButtonEnter ButtonMinimize_MouseClick_Handler;
        public WindowControlPanel()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ButtonClose_MouseClick_Handler?.Invoke(sender, e);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ButtonMinimize_MouseClick_Handler?.Invoke(sender, e);
        }
    }
}
