using System;

namespace Task11
{
    class Program
    {
        public static readonly string alphabetRU = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        public static readonly string alphabetENG = "abcdefghijklmnopqrstuvwxyz";

        public static char Encode(char c, int n, bool lang)
        {
            return lang
                ? alphabetRU[(alphabetRU.IndexOf(c) + n) % alphabetRU.Length]
                : alphabetENG[(alphabetENG.IndexOf(c) + n) % alphabetENG.Length];
        }

        public static int Input()
        {
            int number = 0, lenght = 33;
            bool ok;
            do
            {
                try
                {
                    number = Convert.ToInt32(Console.ReadLine());
                    ok = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка при вводе числа");
                    ok = false;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Ошибка при вводе числа");
                    ok = false;
                }
            } while (!ok);

            number = number % lenght;
            if (number < 0) number = lenght + number;
            return number;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите текст:");
            string text = Console.ReadLine();

            Console.WriteLine("Введите число N (Количество символов, на которое сдвинется шифруемый текст. " +
                              "\nПоложительное число - сдвиг вправо), отрицательное - сдвиг влево):");
            int n = Input();
            char[] mem = text.ToCharArray();
            string newtxt = String.Empty;
            string symbols = @"!&.,?/\*^$#@№;%:()-_=+1234567890";
            bool lang = true;
            foreach (char t in mem)
            {
                if (!alphabetRU.Contains(t.ToString()))
                {
                    lang = false;
                    break;
                }
            }

            foreach (char t in mem)
            {
                if (t != ' ' || !symbols.Contains(t.ToString()))
                    newtxt += Encode(t, n, lang);
                else newtxt += t;
            }
            Console.WriteLine(newtxt);
            Console.Read();
        }
    }
}
