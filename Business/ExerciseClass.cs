﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessApp.DAL;

namespace Business
{
    public class clsExercise
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ExerciseID {  get; set; }
        public int PlanID { get; set; }
        public string ExerciseName { get; set; }
        public int Repetitions { get; set; }
        public int Sets { get; set; }
        public int Duration { get; set; }
        public float CaloriesBurned { get; set; }

        public clsExercise()
        {
            this.ExerciseID = -1;
            this.PlanID = -1;
            this.ExerciseName = "";
            this.Repetitions = 0;
            this.Sets = 0;
            this.Duration = 0;  
            this.CaloriesBurned = 0;

            Mode = enMode.AddNew;
        }

        private clsExercise (int ExerciseID, int PlanID, string ExerciseName, int Repetitions, int Sets,
            int Duration, float CaloriesBurned)
        {
            this.ExerciseID = ExerciseID;
            this.PlanID = PlanID;
            this.ExerciseName = ExerciseName;
            this.Repetitions = Repetitions;
            this.Sets = Sets;
            this.Duration = Duration;
            this.CaloriesBurned = CaloriesBurned;

            Mode = enMode.Update;
        }

        public static clsExercise FindExercisByPlanID (int PlanID)
        {
            int ExerciseID = -1, Repetitions = -1, Sets = -1, Duration = -1;
            string ExerciseName = "";
            float CaloriesBurned = 0;

            bool isFound = DatabaseHelper.GetExercisesByPlanId(PlanID, ref ExerciseID, ref ExerciseName, ref Repetitions, ref Sets,
                ref Duration, ref CaloriesBurned);

            if (isFound)
                return new clsExercise(ExerciseID, PlanID, ExerciseName, Repetitions, Sets, Duration, CaloriesBurned);
            else
                return null;
        }

        private bool _AddExercise()
        {
            this.ExerciseID = DatabaseHelper.AddExercise(this.PlanID,this.ExerciseName,this.Repetitions,this.Sets,
                this.Duration, this.CaloriesBurned);

            return (this.ExerciseID != -1);
        }

        private bool _UpdateExercise()
        {
            return DatabaseHelper.UpdateExercise(this.ExerciseID,this.ExerciseName, this.Repetitions,this.Sets,
                this.Duration,this.CaloriesBurned);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddExercise())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdateExercise();
            }

            return false;
        }

    }
}
