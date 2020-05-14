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
    public delegate void SelectedNumberDelegate(int userId, int number);
    public delegate void DiffNumberDelegate(int userId);
    class CallbackHandler : IService1Callback
    {
  
        static public event VoidDelegate UpdateChatMembers;
        static public event VoidDelegate ResetAllNums;
        static public event VoidDelegate ShowAllNums;
        static public event DiffNumberDelegate ShowSmallesNumber;
        static public event DiffNumberDelegate ShowLargestNumber;

        static public event MessageDelegate ReciveMessage;
        static public SelectedNumberDelegate UpdateSelectedNumbers;
        public void UpdateChatMessages(string message, string from)
        {
            ReciveMessage(message, from);
        }

        public void UpdateRoomMembers()
        {
            UpdateChatMembers();
        }

        public void UpdateSelectNumbers(int userId, int number)
        {
            UpdateSelectedNumbers(userId, number);
        }

        public void ShowAllNumbersInRoom()
        {
            ShowAllNums();
        }
        public void ResetAllNumbersInRoom()
        {
            ResetAllNums();
        }

        public void ShowLargestSelectedNumber(int userId)
        {
            ShowLargestNumber(userId);
        }

        public void ShowSmallestSelectedNumber(int userId)
        {
            ShowSmallesNumber(userId);
        }
    }
    public partial class MainWindowViewModel
    {
        #region PROPERTIES
        #region PRIVATE
        private ObservableCollection<RoomMember> _listOfChatMembers { get; set; }
        private ObservableCollection<ChatMessage> _messagesList { get; set; }

        private string _enteredMessage { get; set; }
        private string _roomName {get;set;}

        private Visibility _creatorMenuVisability { get; set; }

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
        public string RoomName { get { return _roomName; } set { _roomName = value; OnPropertyChanged(nameof(RoomName)); } }

        public Visibility CreatorMenuVisability { get { return _creatorMenuVisability; } set { _creatorMenuVisability = value; OnPropertyChanged(nameof(CreatorMenuVisability)); } }

        #endregion

        #endregion

        #region COMMANDS&METHODS

        private void LoadRoomMembers()
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
        private void UpdateSelectednumbers(int userId, int number)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() => {
                foreach (var it in ListOfChatMembers)
            {
                if(it.UserId==userId)
                    {
                        it.SetUserSelectedNum(number);
                      //  it.ShowSelectedNumber();
                    }
               

            }
            }));
        }
        private void ShowAllNums()
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() => {
                foreach (var it in ListOfChatMembers)
                {
                    it.ShowSelectedNumber();
                }
            }));
        }
        private void ResetAllNums()
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() => {
                foreach (var it in ListOfChatMembers)
                {
                    it.SetUserSelectedNum(null);
                    it.HideSelectedNumber();
                }
            }));
        }
        private void ShowMaxNumber(int id)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() => {
                ListOfChatMembers.FirstOrDefault(u => u.UserId == id).HaveBiggestSelectedNumber();
             
            }));
        }
        private void ShowMinNumber(int id)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() => {
                ListOfChatMembers.FirstOrDefault(u => u.UserId == id).HaveLowestSelectedNumber();

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
                    EnteredMessage = "";

                });
            }
        }
        public ICommand SelectNumberClick
        {
            get
            {
                return new DelegateClickCommand((num) =>
                {
                    App.Current.Dispatcher.BeginInvoke(new Action(() => {
                        Service.SetSelectedNumberByUserId(CurrentLoginedUser.Id,int.Parse( (num as Button).Content.ToString()));
                    }));
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
                            IsProgressRunning = false;
                            return;
                        }
                        ExceptionHelperText = "";
                        Service.JoinRoom(CurrentLoginedUser.Id, CurrentJoinedRoom.RoomCode);
                                //    LoadRoomMembers();
                                    CodeHintText = CurrentJoinedRoom.RoomCode.ToString();
                                IsProgressRunning = false;
                                OpenGameMenu();
                        RoomName = (roomName as TextBox).Text;
                        CreatorMenuVisability = Visibility.Visible;
                    }));
                });
                     }
                }
        public ICommand ShowAll
        {
            get
            {
                return new DelegateClickCommand((obj) =>
                {
                    Service.SendShowAllNumbersInRoomRequest(CurrentJoinedRoom.Id);
                });
            }
        }
        public ICommand ResetAll
        {
            get
            {
                return new DelegateClickCommand((obj) =>
                {
                    Service.SendResetAllNumbersInRoomRequest(CurrentJoinedRoom.Id);
                });
            }
        }



        #endregion
    }

}
