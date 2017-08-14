using System;
using static System.Console;

namespace Task11
{
    class Action
    {
        private const string AlphabetRu = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя"; // русский алфавит
        private const string AlphabetEng = "abcdefghijklmnopqrstuvwxyz"; // английский алфавит
        private const string Symbols = @"!&.,?/\*^$#@№;%:()-_=+1234567890"; // символы

        private static char Encode(char c, int n, bool lang) // кодирование элемента
        {
            return lang
                ? AlphabetRu[(AlphabetRu.IndexOf(c) + n) % AlphabetRu.Length] // для русского алфавита
                : AlphabetEng[(AlphabetEng.IndexOf(c) + n) % AlphabetEng.Length]; // для английского алфавита
        }

        public static int Input() // ввод числа N
        {
            var number = 0; // переменная для числа
            bool ok; // показатель корректности ввода
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

        private static int NConvert(int number, int length) // перевод числа N в корректный формат в зависимости от алфавита
        {
            number = number % length;
            if (number < 0) number = length + number;
            return number;
        }

        public static string Encryption(string text, int n) // кодировка
        {
            var newtxt = string.Empty; // переменная для закодированного текста
            var length = 0; // длина алфавита
            var lang = true; // переменная для выбора алфавита
            foreach (var t in text)
            {
                if (!AlphabetRu.Contains(t.ToString())) // определение алфавита
                {
                    lang = false;
                    length = AlphabetEng.Length;
                    break;
                }
                length = AlphabetRu.Length;
                break;
            }
            n = NConvert(n, length); // перевод N в корректный формат
            foreach (var t in text) // зашифровка
            {
                if (t != ' ' || !Symbols.Contains(t.ToString()))
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
                var text = ReadLine();
                WriteLine("Введите число N (Количество символов, на которое сдвинется шифруемый текст. " +
                          "\nПоложительное число - сдвиг вправо), отрицательное - сдвиг влево):");
                var n = Action.Input();
                var newtxt = Action.Encryption(text, n); // зашифровка
                WriteLine(newtxt); // вывод текста
                okay = Exit();
            } while (!okay);
        }
    }
}
