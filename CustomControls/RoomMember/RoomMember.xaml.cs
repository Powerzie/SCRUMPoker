using SPWPF.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SPWPF.CustomControls.RoomMember
{
    public partial class RoomMember : UserControl
    {
        private int selectedNumber;
        private int _userId { get; set; }
        public int UserId { get { return _userId; } }
        public RoomMember()
        {
            InitializeComponent();
        }

        public RoomMember(UserDTO user,bool isRoomOwner=false)
        {
            InitializeComponent();
            _userId = user.Id;
            FullNameLabel.Content = user.UserName;
            ShortNameLabel.Content = user.UserName[0];
            if(isRoomOwner) CrownImage.Visibility = Visibility.Visible;
            else CrownImage.Visibility = Visibility.Collapsed;

        }
        public void SetUserSelectedNum(int num)
        {
            selectedNumber = num;
        }
        public void ShowSelectedNumber()
        {
            choisedNum.Content = selectedNumber;
        }
        public void HideSelectedNumber()
        {
            choisedNum.Content = "?";
            choisedNumBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF607D8B"));
        }
        public void HaveBiggestSelectedNumber()
        {
            choisedNumBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF8D0202"));
        }
        public void HaveLowestSelectedNumber()
        {
            choisedNumBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1284C5"));
        }
    }
}
