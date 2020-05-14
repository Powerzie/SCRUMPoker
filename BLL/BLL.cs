using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace BLL
{
    public class BuisnesLogicLayer
    {
        private DataAccesLayer DAL;
        public BuisnesLogicLayer()
        {
            DAL = new DataAccesLayer();
        }

        #region USER
        public bool AddNewUser(UserDTO user)
        {
            return DAL.AddNewUser(new User()
            {
                Id = user.Id,
                RoomId=user.RoomId,
                UserName = user.UserName,
                Password = user.Password,
                IsUserOnline = user.IsUserOnline
            });
        }
        public UserDTO GetUserById(int id)
        {
            User returnedUser = DAL.GetUserById(id);
            if (returnedUser == null)
                return null;
            else
            {
                return new UserDTO()
                {
                    Id = returnedUser.Id,
                    RoomId = returnedUser.RoomId,
                    UserName = returnedUser.UserName,
                    IsUserOnline = returnedUser.IsUserOnline,
                    Password = returnedUser.Password,
                    Email = returnedUser.EmailUser
                };
            }
        }
        public bool IsUserExistByLogin(string login)
        {
            return DAL.IsUserExistByLogin(login);
        }
        public bool IsUserExistByEmail(string email)
        {
            return DAL.IsUserExistByEmail(email);
        }
        public UserDTO LoginByEmail(string hashedPass, string Email)
        {

            User returnedUser = DAL.LoginByEmail(hashedPass,Email);
            if (returnedUser == null)
                return null;
            else
            {
                return new UserDTO()
                {
                    Id = returnedUser.Id,
                    RoomId = returnedUser.RoomId,
                    UserName = returnedUser.UserName,
                    IsUserOnline = returnedUser.IsUserOnline,
                    Password = returnedUser.Password,
                    Email = returnedUser.EmailUser
                };
            }
        }
        public UserDTO LoginByLogin(string hashedPass, string Login)
        {
            User returnedUser = DAL.LoginByLogin(hashedPass,Login);
            if (returnedUser == null)
                return null;
            else
            {
                return new UserDTO()
                {
                    Id = returnedUser.Id,
                    RoomId = returnedUser.RoomId,
                    UserName = returnedUser.UserName,
                    IsUserOnline = returnedUser.IsUserOnline,
                    Password = returnedUser.Password,
                    Email = returnedUser.EmailUser
                };
            }
        }
        #endregion

        #region ROOMS
        public RoomDTO CreateNewRoom(int ownerID,string roomName)
        {
            Room returnedRoom = DAL.CreateNewRoom(ownerID, roomName);
            return new RoomDTO() { Id = returnedRoom.Id, NameOfRoom = returnedRoom.NameOfRoom, OwnerId = returnedRoom.OwnerId, RoomCode = returnedRoom.RoomCode };

        }
        public bool DeleteRoomById(int id)
        {
            return DAL.DeleteRoomById(id);
        }
        public RoomDTO GetRoomByOwnerId(int id)
        {
           Room returnedRooms = DAL.GetRoomByOwnerId(id);
            if (returnedRooms == null)
                return null;
            else
            {
                return new RoomDTO()
                { Id = returnedRooms.Id, NameOfRoom = returnedRooms.NameOfRoom, OwnerId = returnedRooms.OwnerId, RoomCode = returnedRooms.RoomCode };
            }
        }
        public UserDTO GetRoomOwner(int roomCode)
        {
            User returnedUser = DAL.GetRoomOwner(roomCode);
            if (returnedUser == null)
                return null;
            else
                return new UserDTO()
                { Id = returnedUser.Id, IsUserOnline = returnedUser.IsUserOnline, RoomId = returnedUser.RoomId, Password = returnedUser.Password, UserName = returnedUser.UserName };
        }
        public RoomDTO GetRoomByCode(int code)
        {
            Room returnedRoom = DAL.GetRoomByCode(code);
            if (returnedRoom == null)
                return null;
            else
                return new RoomDTO()
                { Id = returnedRoom.Id, NameOfRoom = returnedRoom.NameOfRoom, OwnerId = returnedRoom.OwnerId, RoomCode = returnedRoom.RoomCode };
        }
        public RoomDTO JoinRoom(int userId, int roomCode)
        {
            Room returnedRoom = DAL.JoinRoom(userId,roomCode);
            if (returnedRoom == null)
                return null;
            else
                return new RoomDTO()
                { Id = returnedRoom.Id, NameOfRoom = returnedRoom.NameOfRoom, OwnerId = returnedRoom.OwnerId, RoomCode = returnedRoom.RoomCode };
        }
        public List<UserDTO> GetChatMembersInRoom(int roomCode)
        {
            List<User> res = DAL.GetChatMembersInRoom(roomCode);
            if (res == null)
                return null;
            List<UserDTO> listToReturn = new List<UserDTO>();
            foreach (var it in res)
            {
                listToReturn.Add(new UserDTO()
                { Id = it.Id, IsUserOnline = it.IsUserOnline, RoomId = it.RoomId, Password = it.Password, UserName = it.UserName });
            }
            return listToReturn;

        }

        #endregion
    }
}
