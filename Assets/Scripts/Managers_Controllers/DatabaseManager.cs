//using System.Data;
//using System.IO;
//using Mono.Data.Sqlite;
//using UnityEngine;

//public class DatabaseManager : MonoBehaviour
//{
//    private string dbPath;

//    void Awake()
//    {
//        dbPath = "URI=file:" + Application.persistentDataPath + "/GameDB.db";
//        CreateTable();
//    }

//    private void CreateTable()
//    {
//        using (var conn = new SqliteConnection(dbPath))
//        {
//            conn.Open();
//            using (var cmd = conn.CreateCommand())
//            {
//                cmd.CommandText =
//                    @"CREATE TABLE IF NOT EXISTS PlayerStats (
//                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
//                        CoinsCollected INTEGER,
//                        KeysCollected INTEGER,
//                        EnemiesKilled INTEGER,
//                        ChestsOpened INTEGER,
//                        DoorsOpened INTEGER,
//                        QuestsAccepted INTEGER,
//                        QuestsCompleted INTEGER
//                    );";
//                cmd.ExecuteNonQuery();
//            }
//        }
//        Debug.Log("Database table ready.");
//    }

//    public void SaveStatsInDatabase(StatsData stats)
//    {
//        using (var conn = new SqliteConnection(dbPath))
//        {
//            conn.Open();
//            using (var cmd = conn.CreateCommand())
//            {
//                // Increment existing counts
//                cmd.CommandText =
//                    @"UPDATE PlayerStats
//                      SET CoinsCollected = CoinsCollected + @coins,
//                          KeysCollected = KeysCollected + @keys,
//                          EnemiesKilled = EnemiesKilled + @enemies,
//                          ChestsOpened = ChestsOpened + @chests,
//                          DoorsOpened = DoorsOpened + @doors,
//                          QuestsAccepted = QuestsAccepted + @accepted,
//                          QuestsCompleted = QuestsCompleted + @completed
//                      WHERE ID = (SELECT MAX(ID) FROM PlayerStats);";

//                cmd.Parameters.Add(new SqliteParameter("@coins", stats.coinsCollected));
//                cmd.Parameters.Add(new SqliteParameter("@keys", stats.keysCollected));
//                cmd.Parameters.Add(new SqliteParameter("@enemies", stats.enemiesKilled));
//                cmd.Parameters.Add(new SqliteParameter("@chests", stats.chestsOpened));
//                cmd.Parameters.Add(new SqliteParameter("@doors", stats.doorsOpened));
//                cmd.Parameters.Add(new SqliteParameter("@accepted", stats.questAccepted));
//                cmd.Parameters.Add(new SqliteParameter("@completed", stats.questsCompleted));

//                int rowsAffected = cmd.ExecuteNonQuery();

//                // If no row exists, insert first row
//                if (rowsAffected == 0)
//                {
//                    cmd.CommandText =
//                        @"INSERT INTO PlayerStats 
//                          (CoinsCollected, KeysCollected, EnemiesKilled, ChestsOpened, DoorsOpened, QuestsAccepted, QuestsCompleted)
//                          VALUES (@coins, @keys, @enemies, @chests, @doors, @accepted, @completed);";
//                    cmd.ExecuteNonQuery();
//                }
//            }
//        }

//        Debug.Log("Stats saved/updated in database.");
//    }

//    public StatsData LoadStatsFromDatabase()
//    {
//        StatsData data = new StatsData();

//        using (var conn = new SqliteConnection(dbPath))
//        {
//            conn.Open();
//            using (var cmd = conn.CreateCommand())
//            {
//                cmd.CommandText = "SELECT * FROM PlayerStats ORDER BY ID DESC LIMIT 1";
//                using (IDataReader reader = cmd.ExecuteReader())
//                {
//                    if (reader.Read())
//                    {
//                        data.coinsCollected = reader.GetInt32(1);
//                        data.keysCollected = reader.GetInt32(2);
//                        data.enemiesKilled = reader.GetInt32(3);
//                        data.chestsOpened = reader.GetInt32(4);
//                        data.doorsOpened = reader.GetInt32(5);
//                        data.questAccepted = reader.GetInt32(6);
//                        data.questsCompleted = reader.GetInt32(7);
//                    }
//                }
//            }
//        }

//        Debug.Log("Stats loaded from database.");
//        return data;
//    }

//    public void ClearStatsInDatabase()
//    {
//        using (var conn = new SqliteConnection(dbPath))
//        {
//            conn.Open();
//            using (var cmd = conn.CreateCommand())
//            {
//                cmd.CommandText =
//                    @"UPDATE PlayerStats
//                      SET CoinsCollected = 0,
//                          KeysCollected = 0,
//                          EnemiesKilled = 0,
//                          ChestsOpened = 0,
//                          DoorsOpened = 0,
//                          QuestsAccepted = 0,
//                          QuestsCompleted = 0
//                      WHERE ID = (SELECT MAX(ID) FROM PlayerStats);";
//                cmd.ExecuteNonQuery();
//            }
//        }

//        Debug.Log("Most recent stats reset to 0.");
//    }
//}
