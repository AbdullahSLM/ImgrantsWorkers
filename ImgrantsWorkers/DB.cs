using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace System
{
    public delegate void OnComplete<T>(T result);
}


namespace ImgrantsWorkers
{



    static public class DB
    {
        static public string ServerName = @"PC-WORLD-PC\SQLEXPRESS";
        static private string DatabaseName { get; } = @"ImgrantsWorkers";
        static private SqlConnection connection = null;

        static public void InitialDatabase(string serverName)
        {
            ServerName = serverName;
            CreateDatabaseIfNotExists("ImgrantsWorkers");

            InitializeConnection($@"SERVER={ServerName}; DATABASE={DatabaseName}; INTEGRATED SECURITY=true");
            InitialTabelsIfNotExists();
        }




        static private void InitializeConnection(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }




        static private void CreateDatabaseIfNotExists(string dbName)
        {
            using (var connection = new SqlConnection($@"SERVER ={ ServerName}; INTEGRATED SECURITY = true"))
            {
                connection.Open();
                using (var cmd = new SqlCommand($"If(db_id(N'{dbName}') IS NULL) CREATE DATABASE [{dbName}]", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }




        static private void InitialTabelsIfNotExists()
        {
            var query = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Workers' and xtype='U')
                    CREATE TABLE Workers (
                        ID INTEGER PRIMARY KEY IDENTITY,
                        Name VARCHAR(64) NOT NULL,
                        Birthday DATE,
                        Nationality VARCHAR(32) NOT NULL,
                        CreatedAt DATETIME NOT NULL,
                        UpdatedAt DATETIME NOT NULL
                    )";
            using (var cmd = new SqlCommand(query , connection))
            {
                cmd.ExecuteNonQuery();
            }
        }



        static public void InsertWorker(Worker worker, OnComplete<Worker> onComplete)
        {
            new Thread(() =>
            {
               worker.CreatedAt = DateTime.Now;
               worker.UpdatedAt = DateTime.Now;
               var query = @"INSERT INTO Workers (Name, Birthday, Nationality, CreatedAt, UpdatedAt)
                            VALUES(@Name, @Birthday, @Nationality, @CreatedAt, @UpdatedAt)";
               using (var cmd = new SqlCommand(query, connection))
               {
                   cmd.Parameters.AddWithValue("@Name", worker.Name);
                   cmd.Parameters.AddWithValue("@Birthday", worker.Birthday);
                   cmd.Parameters.AddWithValue("@Nationality", worker.Nationality);
                   cmd.Parameters.AddWithValue("@CreatedAt", worker.CreatedAt);
                   cmd.Parameters.AddWithValue("@UpdatedAt", worker.UpdatedAt);

                   cmd.ExecuteNonQuery();
               }

               onComplete?.Invoke(worker);
           }).Start();
        }




        static public Worker SelectWorker(int id)
        {
            Worker worker = null;

            var query = $@"SELECT * FROM Workers WHERE ID={id}";
            using (var adapter = new SqlDataAdapter(query , connection))
            {
                var table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count == 1)
                {
                    var row = table.Rows[0];

                    worker = new Worker();
                    worker.ID = (int)row["ID"];
                    worker.Name = row["Name"].ToString();
                    worker.Nationality = row["Nationality"].ToString();
                    worker.Birthday = DateTime.Parse(row["Birthday"].ToString());
                    worker.CreatedAt = DateTime.Parse(row["CreatedAt"].ToString());
                    worker.UpdatedAt = DateTime.Parse(row["UpdatedAt"].ToString());
                }

            }

            return worker;
        }




        static public List<Worker> SelectWorkers()
        {
            List<Worker> workers = new List<Worker>();

            var query = "SELECT * FROM Workers";

            using (var adapter = new SqlDataAdapter(query, connection))
            {
                var table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow row in table.Rows)
                {
                    var worker = new Worker();
                    worker.ID = (int)row["ID"];
                    worker.Name = row["Name"].ToString();
                    worker.Nationality = row["Nationality"].ToString();
                    worker.Birthday = DateTime.Parse(row["Birthday"].ToString());
                    worker.CreatedAt = DateTime.Parse(row["CreatedAt"].ToString());
                    worker.UpdatedAt = DateTime.Parse(row["UpdatedAt"].ToString());

                    workers.Add(worker);
                }

                return workers;
            }
        }




        static public void DeleteWorker (int id)
        {
            new Thread(() => {
                var query = $"DELETE FROM Workers WHERE ID = {id}";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }).Start();
        }



        static public void UpdateWorker(Worker worker, OnComplete<Worker> onComplete)
        {
            new Thread(() =>
            {
                worker.UpdatedAt = DateTime.Now;
                var query = $@"UPDATE Workers
                        SET 
                            Name = @Name,
                            Birthday = @Birthday,
                            Nationality = @Nationality,
                            UpdatedAt = @UpdatedAt
                        WHERE ID = @ID;";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ID", worker.ID);
                    cmd.Parameters.AddWithValue("@Name", worker.Name);
                    cmd.Parameters.AddWithValue("@Birthday", worker.Birthday);
                    cmd.Parameters.AddWithValue("@Nationality", worker.Nationality);
                    cmd.Parameters.AddWithValue("@UpdatedAt", worker.UpdatedAt);

                    cmd.ExecuteNonQuery();

                    onComplete?.Invoke(worker);
                }
            }).Start();
        }
    }
}