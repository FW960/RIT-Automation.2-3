using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using Task._1.Entities;

namespace Task._1.Db
{
    public class MyDatabase : IDatabase
    {
        private SqlConnection Connection { get; set; }

        public MyDatabase()
        {
            Connection = new SqlConnection
            {
                ConnectionString = "Server=localhost\\MSSQLSERVER01;Database=MarkersDb;Trusted_Connection=True;"
            };
        }

        public List<MyMarker> GetAll()
        {
            try
            {
                Connection.Open();
                
                var cmd = Connection.CreateCommand();

                cmd.CommandText = "SELECT * FROM Markers";

                var reader = cmd.ExecuteReader();

                List<MyMarker> markersPositions = new List<MyMarker>();

                while (reader.Read())
                {
                    markersPositions.Add(new MyMarker
                    {
                        Name = reader.GetString(0),
                        Latitude = reader.GetDouble(1),
                        Longitude = reader.GetDouble(2)
                    });
                }

                return markersPositions;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());

                throw new Exception(e.ToString());
            }
            finally
            {
                Connection.Close();
            }
        }

        public void Update(MyMarker marker)
        {
            try
            {
                Connection.Open();

                var cmd = Connection.CreateCommand();

                cmd.CommandText = "UPDATE Markers SET Latitude = @lat, Longitude = @long WHERE MarkerName = @name";

                cmd.Parameters.AddWithValue("@lat", marker.Latitude);

                cmd.Parameters.AddWithValue("@long", marker.Longitude);

                cmd.Parameters.AddWithValue("@name", marker.Name);

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());

                throw new Exception(e.ToString());
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}