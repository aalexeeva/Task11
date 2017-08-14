using System;
using static System.Console;

namespace Task11
{
    class Action
    {
        static readonly string alphabetRU = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя"; 
        static readonly string alphabetENG = "abcdefghijklmnopqrstuvwxyz";
        static readonly string symbols = @"!&.,?/\*^$#@№;%:()-_=+1234567890";

        static char Encode(char c, int n, bool lang) // кодирование элемента
        {
            return lang
                ? alphabetRU[(alphabetRU.IndexOf(c) + n) % alphabetRU.Length] // для русского алфавита
                : alphabetENG[(alphabetENG.IndexOf(c) + n) % alphabetENG.Length]; // для английского алфавита
        }

        public static int Input() // ввод числа N
        {
            int number = 0;
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
            return number;
        }

        public static int NConvert(int number, int length) // перевод числа N в корректный формат в зависимости от алфавита
        {
            number = number % length;
            if (number < 0) number = length + number;
            return number;
        }

        public static string Encryption(string text, int n) // кодировка
        {
            string newtxt = String.Empty; // переменная для закодированного текста
            int length = 0; // длина алфавита
            bool lang = true; // переменная для выбора алфавита
            foreach (char t in text)
            {
                if (!alphabetRU.Contains(t.ToString())) // определение алфавита
                {
                    lang = false;
                    length = alphabetENG.Length;
                    break;
                }
                else
                {
                    lang = true;
                    length = alphabetRU.Length;
                    break;
                }
            }

            n = NConvert(n, length); // перевод N в корректный формат

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
        public static bool Exit() // выход из программы
        {
            WriteLine("Желаете начать сначала или нет? \nВведите да или нет");
            var word = Convert.ToString(ReadLine()); // ответ пользователя
            Clear();
            if (word == "да" || word == "Да" || word == "ДА")
            {
                Clear();
                return false;
            }
            Clear();
            WriteLine("Вы ввели 'нет' или что-то непонятное. Нажмите любую клавишу, чтобы выйти из программы.");
            ReadKey();
            return true;
        }

        static void Main(string[] args)
        {
            bool okay;
            do
            {
                WriteLine("Введите текст:");
                string text = ReadLine();
                WriteLine("Введите число N (Количество символов, на которое сдвинется шифруемый текст. " +
                          "\nПоложительное число - сдвиг вправо), отрицательное - сдвиг влево):");
                int n = Action.Input();
                string newtxt = Action.Encryption(text, n); // зашифровка
                WriteLine(newtxt); // вывод текста
                okay = Exit();
            } while (!okay);
        }
    }
}
