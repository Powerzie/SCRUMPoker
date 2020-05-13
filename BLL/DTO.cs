using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsUserOnline { get; set; }
        public string Password { get; set; }
   
    }
    public class RoomDTO
    {
        public int Id { get; set; }
        public int RoomCode { get; set; }
        public int OwnerId { get; set; }
        public string NameOfRoom { get; set; }

    }

}
