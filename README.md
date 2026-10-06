# Habit Tracker - Water Intake

A C# console-based habit tracker that allows users to record and manage their daily water intake using SQLite.

## Features

- Add or update daily water intake
- View all water intake records
- Delete water intake records
- View records with more than 7 glasses
- Persistent SQLite database storage
- Input validation

## Technologies Used

- C#
- .NET 10
- SQLite
- Microsoft.Data.Sqlite
- Visual Studio
- Git / GitHub

## Project Structure

- `Program.cs` - Handles the application flow, menu, switch statements, and user input.
- `Database.cs` - Handles SQLite database operations.
- `WaterRecord.cs` - Represents a water intake record.
- `HabitTracker.csproj` - Project configuration and dependencies.

## Database Structure

### water_intake

| Column | Type | Description |
|---|---|---|
| Id | INTEGER | Primary key |
| Date | TEXT | Date of water intake |
| Glasses | INTEGER | Number of glasses |

## How to Run

1. Clone the repository.
2. Open the project in Visual Studio.
3. Restore the NuGet packages.
4. Build the solution.
5. Run the application.

The SQLite database is created automatically when the application starts.

## Application Menu

```text
1. Add / Update water intake
2. Show all records
3. Remove a record
4. Show records above 7 glasses
0. Exit