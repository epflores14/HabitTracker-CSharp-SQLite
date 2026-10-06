namespace HabitTracker;

class Program
{
    static Database database = new Database();

    static void Main()
    {
        database.Initialize();

        bool running = true;

        while (running)
        {
            ShowMenu();

            Console.Write("Choose an option: ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AddOrUpdateWater();
                    break;

                case "2":
                    ShowAllRecords();
                    break;

                case "3":
                    RemoveRecord();
                    break;

                case "4":
                    ShowAboveSevenGlasses();
                    break;

                case "0":
                    running = false;
                    Console.WriteLine("\nGoodbye!");
                    break;

                default:
                    Console.WriteLine("\nInvalid option. Please choose 0-4.");
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("================================");
        Console.WriteLine("         HABIT TRACKER");
        Console.WriteLine("         WATER INTAKE");
        Console.WriteLine("================================");
        Console.WriteLine("1. Add / Update water intake");
        Console.WriteLine("2. Show all records");
        Console.WriteLine("3. Remove a record");
        Console.WriteLine("4. Show records above 7 glasses");
        Console.WriteLine("0. Exit");
        Console.WriteLine("================================");
    }

    static void AddOrUpdateWater()
    {
        Console.WriteLine("\n--- ADD / UPDATE WATER INTAKE ---");

        DateTime date;

        while (true)
        {
            Console.Write("Enter date (yyyy-MM-dd): ");
            string? dateInput = Console.ReadLine();

            if (DateTime.TryParseExact(
                dateInput,
                "yyyy-MM-dd",
                null,
                System.Globalization.DateTimeStyles.None,
                out date))
            {
                break;
            }

            Console.WriteLine("Invalid date. Example: 2026-10-06");
        }

        int glasses;

        while (true)
        {
            Console.Write("Enter number of glasses: ");
            string? glassesInput = Console.ReadLine();

            if (int.TryParse(glassesInput, out glasses) && glasses >= 0)
            {
                break;
            }

            Console.WriteLine("Please enter a valid non-negative number.");
        }

        database.AddOrUpdateRecord(date, glasses);

        Console.WriteLine(
            $"\nWater intake saved: {date:yyyy-MM-dd} = {glasses} glasses.");
    }

    static void ShowAllRecords()
    {
        Console.WriteLine("\n--- ALL WATER RECORDS ---");

        List<WaterRecord> records = database.GetAllRecords();

        if (records.Count == 0)
        {
            Console.WriteLine("No water intake records found.");
            return;
        }

        Console.WriteLine(
            "{0,-5} {1,-15} {2,-10}",
            "ID",
            "DATE",
            "GLASSES");

        Console.WriteLine(new string('-', 32));

        foreach (WaterRecord record in records)
        {
            Console.WriteLine(
                "{0,-5} {1,-15} {2,-10}",
                record.Id,
                record.Date.ToString("yyyy-MM-dd"),
                record.Glasses);
        }
    }

    static void RemoveRecord()
    {
        Console.WriteLine("\n--- REMOVE RECORD ---");

        ShowAllRecords();

        Console.Write("\nEnter the ID of the record to remove: ");
        string? input = Console.ReadLine();

        if (!int.TryParse(input, out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        bool deleted = database.DeleteRecord(id);

        if (deleted)
        {
            Console.WriteLine($"Record with ID {id} was removed.");
        }
        else
        {
            Console.WriteLine($"No record found with ID {id}.");
        }
    }

    static void ShowAboveSevenGlasses()
    {
        Console.WriteLine("\n--- WATER INTAKE ABOVE 7 GLASSES ---");

        List<WaterRecord> records =
            database.GetRecordsAboveSevenGlasses();

        if (records.Count == 0)
        {
            Console.WriteLine("No records above 7 glasses.");
            return;
        }

        Console.WriteLine(
            "{0,-5} {1,-15} {2,-10}",
            "ID",
            "DATE",
            "GLASSES");

        Console.WriteLine(new string('-', 32));

        foreach (WaterRecord record in records)
        {
            Console.WriteLine(
                "{0,-5} {1,-15} {2,-10}",
                record.Id,
                record.Date.ToString("yyyy-MM-dd"),
                record.Glasses);
        }
    }
}