using Microsoft.Data.Sqlite;

namespace HabitTracker;

public class Database
{
    private readonly string _connectionString =
        "Data Source=habit-tracker.db";

    // Creates the database table if it does not already exist
    public void Initialize()
    {
        using var connection =
            new SqliteConnection(_connectionString);

        connection.Open();

        string sql = """
            CREATE TABLE IF NOT EXISTS water_intake
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Date TEXT NOT NULL UNIQUE,
                Glasses INTEGER NOT NULL CHECK (Glasses >= 0)
            );
            """;

        using var command =
            new SqliteCommand(sql, connection);

        command.ExecuteNonQuery();
    }

    // Adds a new record or updates the record for an existing date
    public void AddOrUpdateRecord(DateTime date, int glasses)
    {
        using var connection =
            new SqliteConnection(_connectionString);

        connection.Open();

        string sql = """
            INSERT INTO water_intake (Date, Glasses)
            VALUES (@date, @glasses)
            ON CONFLICT(Date)
            DO UPDATE SET Glasses = excluded.Glasses;
            """;

        using var command =
            new SqliteCommand(sql, connection);

        command.Parameters.AddWithValue(
            "@date",
            date.ToString("yyyy-MM-dd"));

        command.Parameters.AddWithValue(
            "@glasses",
            glasses);

        command.ExecuteNonQuery();
    }

    // Gets all water intake records
    public List<WaterRecord> GetAllRecords()
    {
        var records = new List<WaterRecord>();

        using var connection =
            new SqliteConnection(_connectionString);

        connection.Open();

        string sql = """
            SELECT Id, Date, Glasses
            FROM water_intake
            ORDER BY Date DESC;
            """;

        using var command =
            new SqliteCommand(sql, connection);

        using var reader =
            command.ExecuteReader();

        while (reader.Read())
        {
            records.Add(new WaterRecord
            {
                Id = reader.GetInt32(0),
                Date = DateTime.Parse(reader.GetString(1)),
                Glasses = reader.GetInt32(2)
            });
        }

        return records;
    }

    // Deletes a record using its ID
    public bool DeleteRecord(int id)
    {
        using var connection =
            new SqliteConnection(_connectionString);

        connection.Open();

        string sql = """
            DELETE FROM water_intake
            WHERE Id = @id;
            """;

        using var command =
            new SqliteCommand(sql, connection);

        command.Parameters.AddWithValue(
            "@id",
            id);

        int rowsAffected =
            command.ExecuteNonQuery();

        return rowsAffected > 0;
    }

    // Gets records where water intake is greater than 7 glasses
    public List<WaterRecord> GetRecordsAboveSevenGlasses()
    {
        var records = new List<WaterRecord>();

        using var connection =
            new SqliteConnection(_connectionString);

        connection.Open();

        string sql = """
            SELECT Id, Date, Glasses
            FROM water_intake
            WHERE Glasses > 7
            ORDER BY Glasses DESC;
            """;

        using var command =
            new SqliteCommand(sql, connection);

        using var reader =
            command.ExecuteReader();

        while (reader.Read())
        {
            records.Add(new WaterRecord
            {
                Id = reader.GetInt32(0),
                Date = DateTime.Parse(reader.GetString(1)),
                Glasses = reader.GetInt32(2)
            });
        }

        return records;
    }
}