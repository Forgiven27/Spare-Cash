using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataBank {
    public class RecordEntity
    {
        public int id;
        public int summa;
        public int income;  //  0 - расходы ; 1 - доходы
        public string date;
        public int id_cat;
        public int id_acc;
        public int id_user;

        public RecordEntity(int id, int summa, int income, string date, int id_cat, int id_acc, int id_user)
        {
            this.id = id;
            this.summa = summa;
            this.income = income;
            this.date = date;
            this.id_cat = id_cat;
            this.id_acc = id_acc;
            this.id_user = id_user;
        }

    }
}