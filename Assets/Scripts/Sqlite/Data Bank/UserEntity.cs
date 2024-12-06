using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DataBank
{
    public class UserEntity
    {
        public int id;
        public string login;
        public string password;
        public string typeOfUser;


        public UserEntity(int id, string login, string password, string typeOfUser)
        {
            this.id = id;
            this.login = login;
            this.password = password;
            this.typeOfUser = typeOfUser;
        }
    }
}
