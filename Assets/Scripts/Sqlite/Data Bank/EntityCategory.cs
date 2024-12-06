using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DataBank
{
    public class EntityCategory
    {
        public int id;
        public string name;
        public string color;
        public int id_user;

        public EntityCategory(int id, string name, string color, int id_user)
        {
            this.id = id;
            this.name = name;
            this.color = color;
            this.id_user = id_user;
        }


    }
}

