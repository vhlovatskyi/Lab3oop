using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            // Отримуємо всі файли в поточній папці
            string[] files = Directory.GetFiles(@"D:\cloud\code\uni\2 курс 1 сем\ООП\Lab3\Task2\Images\");

            // Регулярний вираз для перевірки графічних форматів
            Regex regexExtForImage = new Regex(@"\.(bmp|gif|tiff?|jpe?g|png)$", RegexOptions.IgnoreCase);

            foreach (string filePath in files)
            {
                string extension = Path.GetExtension(filePath);
                if (regexExtForImage.IsMatch(extension))
                {
                    try
                    {
                        // Завантажуємо зображення
                        using (Bitmap bitmap = new Bitmap(filePath))
                        {
                            // Дзеркальне відображення по вертикалі
                            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);

                            // Формуємо нову назву файлу
                            string newFileName = Path.GetFileNameWithoutExtension(filePath) + "-mirrored.gif";
                            string newFilePath = Path.Combine(@"D:\cloud\code\uni\2 курс 1 сем\ООП\Lab3\Task2\Images\", newFileName);

                            // Зберігаємо файл у форматі GIF
                            bitmap.Save(newFilePath, System.Drawing.Imaging.ImageFormat.Gif);

                            Console.WriteLine($"Зображення '{filePath}' успішно оброблено та збережено як '{newFileName}'.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Не вдалося обробити файл '{filePath}': {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"Файл '{filePath}' не є зображенням.");
                }
            }

        }
    }
}
