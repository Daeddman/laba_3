using System;
using System.IO;
using System.Text.RegularExpressions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Processing;

class Program
{
    static void Main()
    {
        // Отримати поточну папку або можна використовувати інший шлях
        string folderPath = "/Users/daeddman/RiderProjects/laba_3/task_2/files";
        string[] files = Directory.GetFiles(folderPath);
        
        // Регулярний вираз для перевірки розширень зображень
        Regex regexExtForImage = new Regex(@"^((bmp)|(gif)|(tiff?)|(jpe?g)|(png))$", RegexOptions.IgnoreCase);
        
        foreach (string filePath in files)
        {
            string extension = Path.GetExtension(filePath).TrimStart('.').ToLower();

            // Перевірка на графічне розширення
            if (regexExtForImage.IsMatch(extension))
            {
                try
                {
                    // Завантажити зображення
                    using (Image image = Image.Load(filePath))
                    {
                        // Виконати вертикальне відображення
                        image.Mutate(x => x.Flip(FlipMode.Vertical));

                        // Зберегти новий файл у форматі GIF
                        string newFileName = Path.Combine(folderPath,
                            Path.GetFileNameWithoutExtension(filePath) + "-mirrored.gif");
                        image.Save(newFileName, new GifEncoder());
                        
                        Console.WriteLine($"Зображення збережено: {newFileName}");
                    }
                }
                catch (Exception ex)
                {
                    // Викидаємо виняток з детальним повідомленням
                    throw new InvalidOperationException($"Помилка при обробці файлу '{filePath}': {ex.Message}", ex);
                }
            }
        }
    }
}