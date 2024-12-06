using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataBank { 
public class AccountEntity 
{
        public int id;
        public int id_user;
        public string name;
        

        public AccountEntity(int id,int id_user, string name)
        {
            this.id = id;
            this.id_user = id_user;
            this.name = name;
        }
    }
}
