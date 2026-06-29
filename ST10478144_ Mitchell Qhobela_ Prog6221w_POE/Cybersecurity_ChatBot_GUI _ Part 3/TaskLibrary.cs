using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cybersecurity_ChatBot_GUI
{
    public class TaskLibrary
    {

        private readonly string _connectionString;// This means it can only be accessed inside this class and assigned once

        // The following methods are to create, read, update, and delete (CRUD) the MySQL database 
        public TaskLibrary(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddTask(TaskItem task)

        {

            string query = @"INSERT INTO Tasks

(TaskTitle, Description, ReminderDate, Username)

VALUES (@title, @description, @reminderDate, @username)";

            try

            {

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

            catch (Exception ex)

            {

                MessageBox.Show(ex.ToString());

                throw;

            }

        }

        public void DeleteTask(int taskId)
        {
            string query = "DELETE FROM Tasks WHERE Id = @id";

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", taskId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void CompleteTask(int taskId)
        {
            string query = "UPDATE Tasks SET IsCompleted = TRUE WHERE Id = @id";

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", taskId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateTitle(int taskId, string newTitle)
        {
            string query = @"UPDATE Tasks
                     SET TaskTitle = @title
                     WHERE Id = @id";

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@title", newTitle);
                cmd.Parameters.AddWithValue("@id", taskId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateDescription(int taskId, string newDescription)
        {
            string query = @"UPDATE Tasks
                     SET Description = @description
                     WHERE Id = @id";

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@description", newDescription);
                cmd.Parameters.AddWithValue("@id", taskId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateReminder(int taskId, DateTime newReminder)
        {
            string query = @"UPDATE Tasks
                     SET ReminderDate = @reminder
                     WHERE Id = @id";

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", taskId);
                cmd.Parameters.AddWithValue("@reminder", newReminder);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        // Method to get and display the task data from the database
        public List<TaskItem> GetTasksByUser(string username)
        {
            List<TaskItem> tasks = new List<TaskItem>();

            string query = @"SELECT Id, TaskTitle, Description, ReminderDate, Username,IsCompleted
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
                            Username = reader.GetString("Username"),
                            IsCompleted = reader.GetBoolean("IsCompleted")
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
        public string Username { get; set; }
        public bool IsCompleted { get; set; }
    }
}
