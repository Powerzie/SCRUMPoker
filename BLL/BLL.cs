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
                    UserName = returnedUser.UserName,
                    IsUserOnline = returnedUser.IsUserOnline,
                    Password = returnedUser.Password,
                    Email = returnedUser.EmailUser
                };
            }
        }
        #endregion

        #region ROOMS
        public bool CreateNewRoom(int ownerID,string roomName)
        {
            return DAL.CreateNewRoom(ownerID, roomName);
        }
        public bool DeleteRoomById(int id)
        {
            return DAL.DeleteRoomById(id);
        }
        public List<RoomDTO> GetRoomsByOwnerId(int id)
        {
            List<Room> returnedRooms = DAL.GetRoomsByOwnerId(id);
            if (returnedRooms == null)
                return null;
            else
            {
                List<RoomDTO> roomsToReturn = new List<RoomDTO>();
                foreach(var it in returnedRooms)
                {
                    roomsToReturn.Add(new RoomDTO()
                    { Id = it.Id, NameOfRoom = it.NameOfRoom, OwnerId = it.OwnerId, RoomCode = it.RoomCode }
                    );
                }
                return roomsToReturn;
            }
        }
        public UserDTO GetRoomOwner(int roomCode)
        {
            User returnedUser = DAL.GetRoomOwner(roomCode);
            if (returnedUser == null)
                return null;
            else
                return new UserDTO()
                { Id = returnedUser.Id, IsUserOnline = returnedUser.IsUserOnline, Password = returnedUser.Password, UserName = returnedUser.Password };
        }
    
        #endregion
    }
}
