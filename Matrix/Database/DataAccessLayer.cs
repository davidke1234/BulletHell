
using Matrix.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Matrix
{
    public static class DataAccessLayer
    {
        private static string _connectionString;


        //Singleton for connection string
        private static string GetConnectionString()
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                var appSettings = ConfigurationManager.AppSettings;
                _connectionString = appSettings.Get("AzureConnString");
            }

            return _connectionString;
        }

        public static List<HighScore> GetHighScores(ref string retValue)
        {
            HighScore highScore = null;
            List<HighScore> highScores = new List<HighScore>();

            try
            {
                SqlDataReader rdr = null;

                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand("dbo.GetHighScores", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        int i = 0;
                        while (rdr.Read() && i<5)
                        {
                            i++;
                            highScore = new HighScore();
                            highScore.Score = (int)rdr["Score"];
                            highScore.Name = rdr["Name"].ToString();
                            highScores.Add(highScore);
                        }

                        retValue = "Successfully retrieved highscores";
                    }
                    else
                        retValue = "No data returned for highscores dates";
                }
            }
            catch (Exception ex)
            {
                if (string.IsNullOrWhiteSpace(retValue))
                    retValue = "Error occurred while getting highscores: " + ex.Message;
            }

            return highScores;
        }

        public static void InsertHighScores(string name, int score, ref string retValue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand("dbo.InsertScore", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Score", score);
                    connection.Open();
                    int result = cmd.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        retValue = "Error inserting data into highscores table!";
                    else
                        retValue = "Successful";
                }
            }
            catch (Exception ex)
            {
                if (string.IsNullOrWhiteSpace(retValue))
                    retValue = "Error occurred while insertng into highscores: " + ex.Message;
            }
        }
    }
}

