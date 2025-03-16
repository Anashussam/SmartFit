using System;
using System.Data.SQLite;
using System.IO;

namespace FitnessApp.DAL
{
    public class DatabaseHelper
    {
        private static string databasePath = "fitness.db";
        private static string connectionString = $"Data Source={databasePath};Version=3;";

        // إنشاء اتصال بقاعدة البيانات
        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        // تهيئة الجداول عند التشغيل الأول
        public static void InitializeDatabase()
        {
            if (!File.Exists(databasePath))
            {
                SQLiteConnection.CreateFile(databasePath);
                using (var conn = GetConnection())
                {
                    conn.Open();
                    CreateAllTables(conn);
                }
            }
        }

        private static void CreateAllTables(SQLiteConnection conn)
        {
            // جدول المستخدمين
            ExecuteNonQuery(conn, @"
                CREATE TABLE IF NOT EXISTS Users (
                    UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Age INTEGER NOT NULL CHECK(Age >= 18),
                    Weight REAL NOT NULL CHECK(Weight > 0),
                    Height REAL NOT NULL CHECK(Height > 0),
                    Gender TEXT NOT NULL CHECK(Gender IN ('Male', 'Female')),
                    ActivityLevel TEXT NOT NULL CHECK(ActivityLevel IN ('low', 'middle', 'High'))
                );");

            // جدول خطط التمارين
            ExecuteNonQuery(conn, @"
                CREATE TABLE IF NOT EXISTS WorkoutPlans (
                    PlanID INTEGER PRIMARY KEY AUTOINCREMENT,
                    UserID INTEGER NOT NULL,
                    PlanName TEXT NOT NULL,
                    PlanDetails TEXT,
                    CreatedAt TEXT DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
                );");

            // جدول التمارين الفردية
            ExecuteNonQuery(conn, @"
                CREATE TABLE IF NOT EXISTS Exercises (
                    ExerciseID INTEGER PRIMARY KEY AUTOINCREMENT,
                    PlanID INTEGER NOT NULL,
                    ExerciseName TEXT NOT NULL,
                    Repetitions INTEGER CHECK(Repetitions > 0),
                    Sets INTEGER CHECK(Sets > 0),
                    Duration INTEGER CHECK(Duration > 0),
                    CaloriesBurned REAL,
                    FOREIGN KEY (PlanID) REFERENCES WorkoutPlans(PlanID) ON DELETE CASCADE
                );");

            // جدول خطط التغذية
            ExecuteNonQuery(conn, @"
                CREATE TABLE IF NOT EXISTS NutritionPlans (
                    PlanID INTEGER PRIMARY KEY AUTOINCREMENT,
                    UserID INTEGER NOT NULL,
                    PlanDetails TEXT NOT NULL,
                    CreatedAt TEXT DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
                );");

            // جدول النشاط اليومي
            ExecuteNonQuery(conn, @"
                CREATE TABLE IF NOT EXISTS UserActivities (
                    ActivityID INTEGER PRIMARY KEY AUTOINCREMENT,
                    UserID INTEGER NOT NULL,
                    Date TEXT DEFAULT CURRENT_DATE,
                    Steps INTEGER DEFAULT 0 CHECK(Steps >= 0),
                    CaloriesBurned REAL DEFAULT 0 CHECK(CaloriesBurned >= 0),
                    WorkoutDuration INTEGER DEFAULT 0 CHECK(WorkoutDuration >= 0),
                    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
                );");
        }

        // دالة مساعدة لتنفيذ الاستعلامات
        private static void ExecuteNonQuery(SQLiteConnection conn, string query)
        {
            using (var cmd = new SQLiteCommand(query, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        // دالة لإضافة بيانات مع البارامترات مثال
        public static int AddExercise(Exercise exercise)
        {
            string query = @"
                INSERT INTO Exercises 
                (PlanID, ExerciseName, Repetitions, Sets, Duration, CaloriesBurned)
                VALUES (@PlanID, @Name, @Reps, @Sets, @Duration, @Calories)";

            SQLiteParameter[] parameters = {
                new SQLiteParameter("@PlanID", exercise.PlanID),
                new SQLiteParameter("@Name", exercise.ExerciseName),
                new SQLiteParameter("@Reps", exercise.Repetitions),
                new SQLiteParameter("@Sets", exercise.Sets),
                new SQLiteParameter("@Duration", exercise.Duration),
                new SQLiteParameter("@Calories", exercise.CaloriesBurned)
            };

            return ExecuteParametrizedQuery(query, parameters);
        }

        // دالة عامة لتنفيذ الاستعلامات مع البارامترات
        public static int ExecuteParametrizedQuery(string query, SQLiteParameter[] parameters)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }

 
    public class Exercise
    {
        public int PlanID { get; set; }
        public string ExerciseName { get; set; }
        public int Repetitions { get; set; }
        public int Sets { get; set; }
        public int Duration { get; set; }
        public double? CaloriesBurned { get; set; }
    }
}