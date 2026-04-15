using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskManagementModels;

namespace taskManagementDataService
{
    public class taskDBData : ItaskDataService
    {
        private string connectionString
            = "Data Source = localhost\\SQLEXPRESS; Initial Catalog = taskSystem; Integrated Security = True; TrustServerCertificate=True;";

        private SqlConnection sqlConnection;

        public taskDBData() { 
        sqlConnection = new SqlConnection(connectionString);
            AddSeeds();

        }
        private void AddSeeds()
        {
            var existing = GetTasks();
            if (existing.Count == 0)
            {
                taskItem taskid = new taskItem
                {
                    TaskId = Guid.NewGuid(),
                    TaskName = "Coding",
                    IsCompleted = false
                };
                Add(taskid);

            }
        }
          
        public void Add(taskItem taskss)
        {
            var insertStatement = "INSERT INTO tbl_tasks VALUES (@TaskId,@TaskName,@IsCompleted)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@TaskId",taskss.TaskId);
            insertCommand.Parameters.AddWithValue("@TaskName", taskss.TaskName);
            insertCommand.Parameters.AddWithValue("@IsCompleted", taskss.IsCompleted);
            sqlConnection.Open();
            insertCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void UpdateTask(taskItem taskItem){

            sqlConnection.Open();

            var updateStatement = $"UPDATE tbl_tasks SET TaskId = @TaskId, TaskName = @TaskName, IsCompleted = @IsCompleted WHERE TaskId = @TaskId";

            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);
            updateCommand.Parameters.AddWithValue("@TaskId", taskItem.TaskId);
            updateCommand.Parameters.AddWithValue("@TaskName", taskItem.TaskName);
            updateCommand.Parameters.AddWithValue("@IsCompleted", taskItem.IsCompleted);
           
            updateCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
        public void DeleteTask(Guid id)
        {
            sqlConnection.Open();
            var updateStatement = $"DELETE FROM tbl_tasks WHERE TaskId = @TaskId";
             SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);
            updateCommand.Parameters.AddWithValue("@TaskId", id);
           

            updateCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
        public void TaskCompleted(Guid id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var updateStatement = "UPDATE tbl_tasks SET IsCompleted = @IsCompleted WHERE TaskId = @TaskId";

                using (SqlCommand command = new SqlCommand(updateStatement, conn))
                {
                    command.Parameters.AddWithValue("@TaskId", id);
                    command.Parameters.AddWithValue("@IsCompleted", true);

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<taskItem> GetTasks()
        {
           var selectStatement = "SELECT TaskId, TaskName, IsCompleted FROM tbl_tasks";
            SqlCommand command = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();
            SqlDataReader reader = command.ExecuteReader();

            var tasks = new List<taskItem>();
            while (reader.Read())
            {
                taskItem t = new taskItem();
                t.TaskId = Guid.Parse(reader["TaskId"].ToString());
                t.TaskName= reader["TaskName"].ToString();
                t.IsCompleted = Convert.ToBoolean(reader["IsCompleted"].ToString());
             

                tasks.Add(t);
            }

            sqlConnection.Close();

            return tasks;
        }
        public taskItem? GetById(Guid id)
        {
            return GetTasks().FirstOrDefault(t => t.TaskId == id);
        }

        public taskItem? GetTaskItem(string taskname)
        {
            return GetTasks().FirstOrDefault(t => t.TaskName == taskname);
        }
    }
}
