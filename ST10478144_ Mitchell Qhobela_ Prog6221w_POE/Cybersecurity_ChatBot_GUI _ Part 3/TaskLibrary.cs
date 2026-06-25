using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_ChatBot_GUI
{
    public class TaskLibrary
    {
        private readonly string _connectionString;

        public TaskLibrary(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddTask(TaskItem task)
        {
            string query = @"INSERT INTO Tasks (TaskTitle, Description, ReminderDate, Username)
                         VALUES (@title, @description, @reminderDate, @username)";

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@title", task.TaskTitle);
                cmd.Parameters.AddWithValue("@description", task.Description);
                cmd.Parameters.AddWithValue("@reminderDate", task.ReminderDate);
                cmd.Parameters.AddWithValue("@username", task.Username);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<TaskItem> GetTasksByUser(string username)
        {
            List<TaskItem> tasks = new List<TaskItem>();

            string query = @"SELECT Id, TaskTitle, Description, ReminderDate, Username
                         FROM Tasks
                         WHERE Username = @username
                         ORDER BY ReminderDate";

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);

                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new TaskItem
                        {
                            Id = reader.GetInt32("Id"),
                            TaskTitle = reader.GetString("TaskTitle"),
                            Description = reader.GetString("Description"),
                            ReminderDate = reader.GetDateTime("ReminderDate"),
                            Username = reader.GetString("Username")
                        });
                    }
                }
            }

            return tasks;
        }
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string TaskTitle { get; set; }
        public string Description { get; set; }
        public DateTime ReminderDate { get; set; }
        public string Username { get; set; }   // optional if tasks belong to a user
    }
}
