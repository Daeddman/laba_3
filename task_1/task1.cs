using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Create create = new Create();
        // create.CreateFiles();
        
        // Список файлів
        string directory = "/Users/daeddman/RiderProjects/laba_3/task_1/files";
        string resultDirectory = directory;
        string[] fileNames = new string[]
        {
            "10.txt", "11.txt", "12.txt", "13.txt", "14.txt", "15.txt", "16.txt", "17.txt", "18.txt", "19.txt",
            "20.txt", "21.txt", "22.txt", "23.txt", "24.txt", "25.txt", "26.txt", "27.txt", "28.txt", "29.txt"
        };

        // Списки для результатів
        List<int> products = new List<int>();
        List<string> noFileList = new List<string>();
        List<string> badDataList = new List<string>();
        List<string> overflowList = new List<string>();

        try
        {
            // Основний цикл
            foreach (var fileName in fileNames)
            {
                try
                {
                    // Шлях до файлу
                    string filePath = Path.Combine(directory, fileName);
                    string[] lines = File.ReadAllLines(filePath);

                    // Перевірка кількості рядків
                    if (lines.Length < 2)
                        throw new IndexOutOfRangeException("Недостатньо рядків у файлі для читання двох чисел");

                    // Спроба зчитати два числа з перших двох рядків
                    int number1 = int.Parse(lines[0]);
                    int number2 = int.Parse(lines[1]);

                    // Обчислення добутку чисел
                    checked
                    {
                        int product = number1 * number2;
                        products.Add(product);
                    }
                }
                catch (FileNotFoundException)
                {
                    noFileList.Add(fileName);
                }
                catch (FormatException)
                {
                    badDataList.Add(fileName);
                }
                catch (IndexOutOfRangeException)
                {
                    badDataList.Add(fileName);
                }
                catch (OverflowException)
                {
                    overflowList.Add(fileName);
                }
            }

            // Запис результатів у файли
            WriteToFile(Path.Combine(resultDirectory, "no_file.txt"), noFileList);
            WriteToFile(Path.Combine(resultDirectory, "bad_data.txt"), badDataList);
            WriteToFile(Path.Combine(resultDirectory, "overflow.txt"), overflowList);

            // Обчислення середнього арифметичного добутків
            double average = products.Average();
            Console.WriteLine($"Середнє арифметичне допустимих добутків: {average}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Не вдалося створити або оновити файл результатів: {ex.Message}");
            Environment.Exit(1); // Негайне завершення програми
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Не вдалося обчислити середнє арифметичне, оскільки немає допустимих добутків.");
        }
    }

    // Метод для запису списку у файл
    static void WriteToFile(string filePath, List<string> data)
    {
        try
        {
            File.WriteAllLines(filePath, data);
        }
        catch (IOException ex)
        {
            throw new IOException($"Помилка запису у файл {filePath}: {ex.Message}");
        }
    }
}
