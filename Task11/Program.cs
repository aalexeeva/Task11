using System;
using static System.Console;

namespace Task11
{
    class Action
    {
        public static readonly string alphabetRU = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        public static readonly string alphabetENG = "abcdefghijklmnopqrstuvwxyz";
        public static readonly string symbols = @"!&.,?/\*^$#@№;%:()-_=+1234567890";

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
                    number = Convert.ToInt32(ReadLine());
                    ok = true;
                }
                catch (FormatException)
                {
                    WriteLine("Ошибка при вводе числа");
                    ok = false;
                }
                catch (OverflowException)
                {
                    WriteLine("Ошибка при вводе числа");
                    ok = false;
                }
            } while (!ok);

            number = number % lenght;
            if (number < 0) number = lenght + number;
            return number;
        }

        public static string Encryption(string text, int n)
        {
            string newtxt = String.Empty;
            
            bool lang = true;
            foreach (char t in text)
            {
                if (!alphabetRU.Contains(t.ToString())) // определение алфавита
                {
                    lang = false;
                    break;
                }
            }

            foreach (char t in text) // зашифровка
            {
                if (t != ' ' || !symbols.Contains(t.ToString()))
                    newtxt += Encode(t, n, lang);
                else newtxt += t;
            }

            return newtxt;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Введите текст:");
            string text = ReadLine();

            WriteLine("Введите число N (Количество символов, на которое сдвинется шифруемый текст. " +
                              "\nПоложительное число - сдвиг вправо), отрицательное - сдвиг влево):");
            int n = Action.Input();
            
            string newtxt = Action.Encryption(text, n);
            WriteLine(newtxt);
            Read();
        }
    }
}
