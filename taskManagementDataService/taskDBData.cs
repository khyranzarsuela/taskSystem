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
                    TaskName = "Coding"
                };
                Add(taskid);

            }
        }
          
        public void Add(taskItem taskss)
        {
            var insertStatement = "INSERT INTO tbl_tasks VALUES (@TaskId,@TaskName)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@TaskId",taskss.TaskId);
            insertCommand.Parameters.AddWithValue("@TaskName", taskss.TaskName);
            sqlConnection.Open();
            insertCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public List<taskItem> GetTasks()
        {
            var tasks = new List<taskItem>();

            var selectStatement = "SELECT TaskId, TaskName FROM tbl_tasks";
            SqlCommand command = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                tasks.Add(new taskItem
                {
                    TaskId = reader.GetGuid(0),
                    TaskName = reader.GetString(1)
                });
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
