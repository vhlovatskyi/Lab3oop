using System.Diagnostics;
using System.Xml;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO;
using System;
namespace Lab3
{
    class Program
    {
        static void Main()
        {
            string[] files = Directory.GetFiles(@"D:\cloud\code\uni\2 курс 1 сем\ООП\Lab3\Task\InvalidData\");
            foreach (var item in files) File.Delete(item);
            Console.WriteLine(AvgSum());
            Console.ReadKey();
        }

        static double AvgSum()
        {
            double avg = 0;       // Сума добутків чисел
            ushort count = 0;     // Лічильник оброблених файлів
            int number = 1;       // Нумерація винятків для виведення на екран

            for (int i = 10; i < 30; i++)
            {
                string path = $@"D:\cloud\code\uni\2 курс 1 сем\ООП\Lab3\Task\TXT\{i}.txt";
                try
                {
                    var file = new FileStream(path, FileMode.Open);  // Відкриває файл
                    StreamReader reader = new StreamReader(file);    // Створює читача файлу
                    int num1 = int.Parse(reader.ReadLine());         // Читає перший рядок і перетворює його на int
                    int num2 = int.Parse(reader.ReadLine());         // Читає другий рядок і перетворює його на int
                    file.Close();                                    // Закриває файл
                    reader.Close();                                  // Закриває читача файлу

                    checked
                    {
                        num1 *= num2; // Перевірка на переповнення при множенні
                    }
                    avg += num1;      // Додає добуток до загальної суми
                    count++;          // Збільшує лічильник файлів
                }
                catch (OverflowException)
                {
                    Console.WriteLine($"Exception {number}\nReason: numbers are too big and their product cannot exceed {int.MaxValue}/{-int.MaxValue}\nFile name: {Path.GetFileName(path)}\n");
                    AppendLog("overflow", path); // Додає лог у файл overflow.txt
                    number++;
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine($"Exception {number}\nReason: file must contain only 1 integer in first and 1 integer in second lines\nFile name: {Path.GetFileName(path)}\n");
                    AppendLog("bad_data", path); // Додає лог у файл bad_data.txt
                    number++;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Exception {number}\nReason: {ex.Message.ToLower()}\nFile name: {Path.GetFileName(path)}\n");
                    AppendLog("bad_data", path); // Додає лог у файл bad_data.txt
                    number++;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"Exception {number}\nReason: file wasn't found\nFile name: {Path.GetFileName(path)}\n");
                    AppendLog("no_file", path); // Додає лог у файл no_file.txt
                    number++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            try
            {
                double result = avg / count;
            }
            catch
            {
                Exception ex = new DivideByZeroException("divide by 0");
            }
            return avg / count;
        }
        static void AppendLog(string name, string path)
        {
            try
            {
                FileStream creator = new FileStream($@"D:\cloud\code\uni\2 курс 1 сем\ООП\Lab3\Task\InvalidData\{name}.txt", FileMode.Append);
                StreamWriter streamWriter = new StreamWriter(creator);
                streamWriter.WriteLine(Path.GetFileName(path)); // Записує ім'я файлу з помилкою
                streamWriter.Close();
                creator.Close();
            }
            catch (Exception)
            {
                Console.WriteLine($"Couldn't create or update {name}.txt file"); // Повідомлення про помилку запису лога
            }
        }
    }
}