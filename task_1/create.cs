using System;
using System.IO;


class Create
{
    private string folderPath;
    private string[] fileNames;
    private Random random;

    public Create()
    {
        // Ініціалізація шляху до директорії
        folderPath = "/Users/daeddman/RiderProjects/laba_3/task_1/files";
        fileNames = new string[]
        {
            "10.txt", "11.txt", "12.txt", "13.txt", "14.txt", "15.txt", "16.txt", "17.txt", "18.txt", "19.txt",
            "20.txt", "21.txt", "22.txt", "23.txt", "24.txt", "25.txt", "26.txt", "27.txt", "28.txt", "29.txt"
        };
        random = new Random();

        // Створення директорії якщо її не існує
        Directory.CreateDirectory(folderPath);
    }

    public void CreateFiles()
    {
        foreach (string fileName in fileNames)
        {
            string filePath = Path.Combine(folderPath, fileName);
            int number1 = random.Next(1, 100);
            int number2 = random.Next(1, 100);

            File.WriteAllText(filePath, $"{number1}\n{number2}");
            Console.WriteLine($"Cтворено чи оновлено файл {fileName} з числами {number1} та {number2}");
        }
    }
    
}
