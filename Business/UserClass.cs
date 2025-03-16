using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using FitnessApp.DAL;

namespace Business
{
    public class clsUser
    {
        public enum enMode { AddNew = 0 , Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID {  get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public string Gender { get; set; }
        public string ActivityLevel { get; set; }

       

        public clsUser ()
        {
            this.UserID = -1;
            this.UserName = "";
            this.Age = 0;
            this.Weight = 0;
            this.Height = 0;
            this.Gender = "";
            this.ActivityLevel = "";

            Mode = enMode.AddNew;

        }

        private clsUser(int UserID, string UserName, int Age, float Weight, float Height,
            string Gender, string ActivityLevel)
        {
            this.UserID = UserID;
            this.UserName = UserName;
            this.Age = Age;
            this.Weight = Weight;
            this.Height = Height;
            this.Gender = Gender;
            this.ActivityLevel = ActivityLevel;

            Mode = enMode.Update;
        }

        private bool _AddNewUser ()
        {
            this.UserID = DatabaseHelper.AddUser(this.UserName, this.Age, this.Weight, this.Height,
                this.Gender, this.ActivityLevel);

            return (this.UserID != -1);
        }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
            }

            return false;
        }
    }
}
