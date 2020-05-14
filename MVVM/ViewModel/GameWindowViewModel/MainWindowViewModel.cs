using EASendMail;
using SPWPF.CustomControls.ChatMessage;
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
    public delegate void MessageDelegate(string message, string from);
    class CallbackHandler : IService1Callback
    {
  
        static public event VoidDelegate UpdateChatMembers;
        static public event MessageDelegate ReciveMessage;
        public void UpdateChatMessages(string message, string from)
        {
            ReciveMessage(message, from);
        }

        public void UpdateRoomMembers()
        {
            UpdateChatMembers();
        }

   
    }
    public partial class MainWindowViewModel
    {
        #region PROPERTIES
        #region PRIVATE
        private ObservableCollection<RoomMember> _listOfChatMembers { get; set; }
        private ObservableCollection<ChatMessage> _messagesList { get; set; }
        
        private string _enteredMessage { get; set; }

        #endregion

        #region PUBLIC 

        public ObservableCollection<RoomMember> ListOfChatMembers
        {
            get { return _listOfChatMembers; }
            set { _listOfChatMembers = value; OnPropertyChanged(nameof(ListOfChatMembers)); }
        }
        public ObservableCollection<ChatMessage> MessagesList
        {
            get { return _messagesList; }
            set { _messagesList = value; OnPropertyChanged(nameof(MessagesList)); }
        }
        public string EnteredMessage { get { return _enteredMessage; } set { _enteredMessage = value; OnPropertyChanged(nameof(EnteredMessage)); } }

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
                    if(CurrentJoinedRoom==null)
                    {
                        ExceptionHelperText = "No room with this code";
                        return;
                    }
                  //  LoadRoomMembers();
                    CodeHintText = CurrentJoinedRoom.RoomCode.ToString();
                    ExceptionHelperText = "";
                    OpenGameMenu();

                });
            }
        }
        public ICommand SendButtonClick
        {
            get
            {
                return new DelegateClickCommand((obj) =>
                {
                    Service.SendMessage(CurrentJoinedRoom.Id, EnteredMessage, CurrentLoginedUser.Id);

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
                        if (CurrentJoinedRoom == null)
                        {
                            ExceptionHelperText = "This name is alredy used";
                            return;
                        }
                        ExceptionHelperText = "";
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
