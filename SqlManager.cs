using System;
using System.Data.SQLite;
using System.IO;

namespace GymOtomasyonu
{
    public class SqlManager
    {
        private static string dbFile = "GymDB.sqlite";
        private static string connectionString = $"Data Source={dbFile};Version=3;";

        public static void VeritabaniniHazirla()
        {
            if (!File.Exists(dbFile))
            {
                SQLiteConnection.CreateFile(dbFile);
            }

            using (var conn = GetConnection())
            {
                conn.Open();
                string createTables = @"
                    CREATE TABLE IF NOT EXISTS Uyeler (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        TC TEXT UNIQUE,
                        AdSoyad TEXT,
                        Boy REAL,
                        Kilo REAL,
                        Yas INTEGER,
                        IsMilli INTEGER DEFAULT 0,
                        LisansNo TEXT,
                        KayitTarihi DATETIME DEFAULT CURRENT_TIMESTAMP
                    );
                    CREATE TABLE IF NOT EXISTS Odemeler (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        UyeTC TEXT,
                        Tutar REAL,
                        Durum TEXT,
                        Tarih DATETIME DEFAULT CURRENT_TIMESTAMP
                    );";
                using (var cmd = new SQLiteCommand(createTables, conn)) { cmd.ExecuteNonQuery(); }
            }

        }

        public static void OrnekVerileriYukle()
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string checkCount = "SELECT COUNT(*) FROM Uyeler";
                long count = 0;
                using (var cmd = new SQLiteCommand(checkCount, conn)) { count = (long)cmd.ExecuteScalar(); }

                if (count == 0)
                {
                    using (var transaction = conn.BeginTransaction())
                    {
                        // Örnek Milli Sporcular (60 Kişi)
                        for (int i = 1; i <= 60; i++)
                        {
                            string tc = "100000000" + i.ToString("D2");
                            string ad = "Milli Sporcu " + i;
                            string lisans = i == 1 ? "SAID2025" : "CHAMP" + (2000 + i);

                            string insert = $@"INSERT INTO Uyeler (TC, AdSoyad, Boy, Kilo, Yas, IsMilli, LisansNo)
                                             VALUES ('{tc}', '{ad}', 180, 75, 22, 1, '{lisans}')";
                            using (var cmd = new SQLiteCommand(insert, conn)) { cmd.ExecuteNonQuery(); }
                        }

         
                        string insertOdeme = @"
                            INSERT INTO Odemeler (UyeTC, Tutar, Durum) VALUES ('101101', 12500, 'Ödendi');
                            INSERT INTO Odemeler (UyeTC, Tutar, Durum) VALUES ('102102', 1000, 'Gecikti');";
                        using (var cmd = new SQLiteCommand(insertOdeme, conn)) { cmd.ExecuteNonQuery(); }

                        transaction.Commit();
                    }
                }
            }
        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}