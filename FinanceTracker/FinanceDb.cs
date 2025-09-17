using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;

namespace FinanceTracker
{
    public class FinanceDb
    {
        private readonly string _connString;

        public FinanceDb(string path)
        {
            _connString = $"Data Source={path}";
        }

        public void EnsureCreated()
        {
            using (var con = new SqliteConnection(_connString))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS transactions(
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        date TEXT NOT NULL,
                        category TEXT NOT NULL,
                        amount REAL NOT NULL,
                        note TEXT
                      );";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddTransaction(DateTime date, string category, double amount, string note)
        {
            using (var con = new SqliteConnection(_connString))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO transactions(date, category, amount, note) VALUES ($d,$c,$a,$n)";
                    cmd.Parameters.AddWithValue("$d", date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("$c", category);
                    cmd.Parameters.AddWithValue("$a", amount);
                    cmd.Parameters.AddWithValue("$n", note ?? "");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetTransactions(DateTime from, DateTime to)
        {
            var table = new DataTable();
            using (var con = new SqliteConnection(_connString))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM transactions WHERE date >= $f AND date < $t ORDER BY date";
                    cmd.Parameters.AddWithValue("$f", from.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("$t", to.ToString("yyyy-MM-dd"));
                    using (var reader = cmd.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                }
            }
            return table;
        }

        public List<CategoryTotal> GetCategoryTotals(DateTime from, DateTime to)
        {
            var list = new List<CategoryTotal>();
            using (var con = new SqliteConnection(_connString))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"SELECT category, SUM(amount) 
                                        FROM transactions
                                        WHERE date >= $f AND date < $t
                                        GROUP BY category";
                    cmd.Parameters.AddWithValue("$f", from.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("$t", to.ToString("yyyy-MM-dd"));
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new CategoryTotal
                            {
                                Category = reader.GetString(0),
                                Total = reader.IsDBNull(1) ? 0 : reader.GetDouble(1)
                            });
                        }
                    }
                }
            }
            return list;
        }
    }

    public class CategoryTotal
    {
        public string Category { get; set; }
        public double Total { get; set; }
    }
}
