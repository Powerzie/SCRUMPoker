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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace SPWPF.MVVM.ViewModel.MainWindowViewModel
{
    public delegate void VoidDelegate();
    class CallbackHandler : IService1Callback
    {
  
        static public event VoidDelegate UpdateChatMembers;

        void IService1Callback.UpdateChatMembers()
        {
            UpdateChatMembers();
        }
    }
    public partial class MainWindowViewModel
    {
        #region PROPERTIES
        #region PRIVATE
        private ObservableCollection<RoomMember> _listOfChatMembers { get; set; }


        #endregion

        #region PUBLIC 

        public ObservableCollection<RoomMember> ListOfChatMembers
        {
            get { return _listOfChatMembers; }
            set { _listOfChatMembers = value; OnPropertyChanged(nameof(ListOfChatMembers)); }
        }

            #endregion

            #endregion

            #region COMMANDS&METHODS
       
        public void LoadRoomMembers()
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() => { 
            ListOfChatMembers.Clear();
                ListOfChatMembers.Add(new RoomMember(Service.GetRoomOwner(CurrentJoinedRoom.RoomCode), true));
                foreach (var it in Service.GetChatMembersInRoom(CurrentJoinedRoom.RoomCode))
                {
                    if (it.Id != CurrentJoinedRoom.OwnerId)
                        ListOfChatMembers.Add(new RoomMember(it, false));
                }
            }));
        }
        public ICommand JoinButtonClick
        {
            get
            {
                return new DelegateClickCommand((obj) =>
                {
                   CurrentJoinedRoom= Service.JoinRoom(CurrentLoginedUser.Id,int.Parse(JoinWindowEnteredCode));
                  //  LoadRoomMembers();
                    CodeHintText = CurrentJoinedRoom.RoomCode.ToString();
                    OpenGameMenu();

                });
            }
        }

        public ICommand CreateNewRoom
                {
                     get
                     {
                return new DelegateClickCommand((roomName) =>
                {
                    Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => {
                                IsProgressRunning = true;
                             
                                    CurrentJoinedRoom = Service.CreateNewRoom(CurrentLoginedUser.Id, (roomName as TextBox).Text);
                                    Service.JoinRoom(CurrentLoginedUser.Id, CurrentJoinedRoom.RoomCode);
                                //    LoadRoomMembers();
                                    CodeHintText = CurrentJoinedRoom.RoomCode.ToString();
                                IsProgressRunning = false;
                                OpenGameMenu();
                    }));
                });
                     }
                }
        
            #endregion
    }

}
