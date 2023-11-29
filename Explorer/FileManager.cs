using System;
using System.Diagnostics;
using System.IO;

class FileManager
{
    static int selectedDriveIndex = 0;
    static DriveInfo[] drives;
    public static void Explore()
    {
        drives = DriveInfo.GetDrives();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Выбери дискарь:");
            for (int i = 0; i < drives.Length; i++)
            {
                Console.WriteLine($"{(i == selectedDriveIndex ? " ->" : "   ")} {GetDriveName(drives[i])}");
            }
            ConsoleKeyInfo driveKey = Console.ReadKey();
            if (driveKey.Key == ConsoleKey.Escape)
            {
                break;
            }
            if (driveKey.Key == ConsoleKey.UpArrow && selectedDriveIndex > 0)
            {
                selectedDriveIndex--;
            }
            else if (driveKey.Key == ConsoleKey.DownArrow && selectedDriveIndex < drives.Length - 1)
            {
                selectedDriveIndex++;
            }
            else if (driveKey.Key == ConsoleKey.Enter)
            {
                ArrowMenu.ShowMenu(drives[selectedDriveIndex].RootDirectory);
            }
        }
    }
    private static string GetDriveName(DriveInfo drive)
    {
        return $"{drive.Name} - {FormatBytes(drive.AvailableFreeSpace)} свободно из {FormatBytes(drive.TotalSize)}";
    }
    private static string FormatBytes(long bytes)
    {
        string[] unit = { "б", "кб", "мб", "гб", "тб", "пт", "эб" };
        int unitindex = 0;
        double size = bytes;
        while (size >= 1024 && unitindex < unit.Length - 1)
        {
            size /= 1024;
            unitindex++;
        }
        return $"{size:N1} {unit[unitindex]}";
    }
}