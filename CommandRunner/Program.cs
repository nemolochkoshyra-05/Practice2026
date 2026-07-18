using System;
using System.IO;
using System.Linq;
using System.Reflection;
using CommandLib;

namespace CommandRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Динамическая загрузка библиотек ===");

            string dllPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "FileSystemCommands", "bin", "Debug", "net8.0", "FileSystemCommands.dll"));

            if (!File.Exists(dllPath))
            {
                Console.WriteLine($"[Ошибка] Библиотека не найдена по пути:\n{dllPath}");
                Console.WriteLine("Убедитесь, что проект FileSystemCommands успешно скомпилирован.");
                return;
            }

            Console.WriteLine($"Загрузка сборки: {Path.GetFileName(dllPath)}");
            Assembly assembly = Assembly.LoadFrom(dllPath);

            var commandTypes = assembly.GetTypes()
                .Where(t => typeof(ICommand).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .ToList();

            string testDir = AppDomain.CurrentDomain.BaseDirectory; 

            foreach (var type in commandTypes)
            {
                Console.WriteLine($"\n--- Выполнение {type.Name} ---");
                try
                {
                    object instance = null;

                    if (type.Name == "DirectorySizeCommand")
                    {
                        instance = Activator.CreateInstance(type, testDir);
                    }
                    else if (type.Name == "FindFilesCommand")
                    {
                        instance = Activator.CreateInstance(type, testDir, "*.dll");
                    }

                    if (instance is ICommand command)
                    {
                        command.Execute();

                        if (type.Name == "DirectorySizeCommand")
                        {
                            var sizeProp = type.GetProperty("TotalSize");
                            Console.WriteLine($"Общий размер каталога: {sizeProp?.GetValue(instance)} байт.");
                        }
                        else if (type.Name == "FindFilesCommand")
                        {
                            var filesProp = type.GetProperty("FoundFiles");
                            var files = filesProp?.GetValue(instance) as System.Collections.Generic.List<string>;
                            Console.WriteLine($"Найдено файлов: {files?.Count ?? 0}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}