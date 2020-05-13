using EASendMail;
using SPWPF.CustomControls.RoomMember;
using SPWPF.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        #region PROPERTIES
        #region PRIVATE
         private ObservableCollection<RoomMember> _listOfChatMembers { get; set; }


        #endregion

        #region PUBLIC 

        public ObservableCollection<RoomMember> ListOfChatMembers { get { return _listOfChatMembers; }
            set { _listOfChatMembers = value;OnPropertyChanged(nameof(ListOfChatMembers)); }

        #endregion

        #endregion

        #region COMMANDS&METHODS

        #endregion
    }

}
