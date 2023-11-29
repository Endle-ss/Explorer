using System;
using System.Diagnostics;
using System.IO;
using System.Text;
class ArrowMenu
{
    static int selectedItemIndex = 0;
    static FileSystemInfo[] items;

    public static void ShowMenu(DirectoryInfo directory)
    {
        selectedItemIndex = 0;
        items = directory.GetFileSystemInfos();
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Папка: {directory.FullName}");
            Console.WriteLine("****************************************************************************");
            Console.WriteLine("! Имя                      ! Дата                 ! Тип                     !");
            Console.WriteLine("****************************************************************************");
            for (int i = 0; i < items.Length; i++)
            {
                Console.WriteLine($"!{(i == selectedItemIndex ? " ->" : "   ")} {GetFileName(items[i]),-25} ! {items[i].CreationTime,-20} ! {GetFileType(items[i]),-25} !");
            }
            Console.WriteLine("****************************************************************************");
            ConsoleKeyInfo eKey = Console.ReadKey();
            if (eKey.Key == ConsoleKey.Escape)
            {
                return;
            }
            int totalItems = items.Length;
            if (eKey.Key == ConsoleKey.UpArrow && selectedItemIndex > 0)
            {
                selectedItemIndex--;
            }
            else if (eKey.Key == ConsoleKey.DownArrow && selectedItemIndex < totalItems - 1)
            {
                selectedItemIndex++;
            }
            else if (eKey.Key == ConsoleKey.Enter)
            {
                FileSystemInfo selectedItem = items[selectedItemIndex];

                if (selectedItem is DirectoryInfo selectedDirectory)
                {
                    ShowMenu(selectedDirectory);
                }
                else if (selectedItem is FileInfo selectedFile)
                {
                    OpenFile(selectedFile);
                }
            }
        }
    }
    private static void OpenFile(FileInfo file)
    {
        Console.Clear();
        Console.WriteLine($"Открываем файл: {file.FullName}");
        try
        {
            Process.Start("explorer.exe", file.FullName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка когда открывался файл(((: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
        }
        Console.WriteLine("Чтоб продолжить нажми любую кнопочку кроме выключения пк плиз");
        Console.ReadKey();
    }
    private static string GetFileName(FileSystemInfo info)
    {
        return info.Name;
    }
    private static string GetFileType(FileSystemInfo info)
    {
        return info is DirectoryInfo ? "--- Это папка" : "--- Это файл";
    }
}