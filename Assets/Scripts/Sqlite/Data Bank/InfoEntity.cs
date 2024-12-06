using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DataBank
{
    public class InfoEntity
    {
        public string _name_cat;
        public int _percent;
        public string _color;
        
       


        public InfoEntity(string name_cat, int percent, string color)
        {
            _name_cat = name_cat;
            _percent = percent;
            _color = color;
        }
    }
}
