using System.Globalization;

namespace PracticeMathAnalysis
{
    public static class Menu
    {
        /// <summary>
        /// Запуск меню
        /// </summary>
        public static void StartMenu()
        {
            NumberFormatInfo nfi = new() { NumberDecimalSeparator = "." };

            string functionBody = String.Empty;
            decimal a = 0, b = 0;

            functionBody = "8x^4+6x^3+x^2*cos(x)+2^(1/2)";
            a = 6; b = 21;

            Console.WriteLine("Инструкция:\n1. Вводить все интегралы в виде его тела и пределов интегрирования\n2. Числа с плавающей точкой вводить только с точкой\n3. Для навигации по меню использовать цифры, соответствующие номерам строк\n4. По окончании ввода нажимать enter");
            Console.ReadKey();
            Console.Clear();
            Main();

            void ShowInstruction()
            {
                Console.WriteLine("Инструкция:\n1. Вводить все интегралы в виде его тела и пределов интегрирования\n2. Числа с плавающей точкой вводить только с точкой\n3. Для навигации по меню использовать цифры, соответствующие номерам строк\n4. По окончании ввода нажимать enter");
                Console.ReadKey();
                Console.Clear();
                ShowInput();
            }

            void ShowInput()
            {
                Console.WriteLine($"Тело интеграла: {(functionBody == String.Empty ? "ожидается" : functionBody)}\tНижний предел: {(a == b ? "ожидается" : a)}\tВерхний предел: {(a == b ? "ожидается" : b)}");

                Console.WriteLine("1. Ввести тело интеграла\n2. Ввести нижний предел инетерирования\n3. Ввести верхний предел интегрирования\n4. Закончить ввод\n5. Показать инструкцию");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        Console.WriteLine("Введите тело интеграла:");
                        functionBody = Console.ReadLine();
                        Console.Clear();
                        ShowInput();
                        return;
                    case ConsoleKey.D2:
                        Console.Clear();
                        Console.WriteLine("Введите нижний интегрирования:");
                        a = Convert.ToDecimal(Console.ReadLine(), nfi);
                        Console.Clear();
                        ShowInput();
                        return;
                    case ConsoleKey.D3:
                        Console.Clear();
                        Console.WriteLine("Введите верхний интегрирования:");
                        b = Convert.ToDecimal(Console.ReadLine(), nfi);
                        Console.Clear();
                        ShowInput();
                        return;
                    case ConsoleKey.D4:
                        Console.Clear();
                        Main();
                        return;
                    case ConsoleKey.D5:
                        Console.Clear();
                        ShowInstruction();
                        return;
                }
            }

            void Main()
            {
                Console.WriteLine("1. Ввести интеграл\n2. Посчитать интеграл формулой Симпсона\n3. Посчитать интеграл оптимальным способом\n4. Сравнить решенные интегралы\n5. Очистить\n6. Выход");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        ShowInput();
                        return;
                    case ConsoleKey.D2:
                        Console.Clear();

                        if (functionBody != String.Empty && a != b)
                        {
                            Function function2 = new(functionBody, a, b);
                            Console.WriteLine($"Результат при решении формулой Симпсона: {function2.SimpsonMethod()}");
                            Console.Read();
                            Console.Clear();
                            Main();
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Интеграл введен не до конца!");
                            Console.Read();
                            Console.Clear();
                            ShowInput();
                            return;
                        }
                    case ConsoleKey.D3:
                        Console.Clear();
                        if (functionBody != String.Empty && a != b)
                        {
                            Function function3 = new(functionBody, a, b);
                            Console.WriteLine($"Результат при решении опмтимальным способом: {function3.CalculateIntegral().Result}");
                            Console.Read();
                            Console.Clear();
                            Main();
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Интеграл введен не до конца!");
                            Console.Read();
                            Console.Clear();
                            ShowInput();
                            return;
                        }
                    case ConsoleKey.D4:
                        Console.Clear();

                        if (functionBody != String.Empty && a != b)
                        {
                            Function function = new(functionBody, a, b);
                            decimal sm = function.SimpsonMethod(), ow = function.CalculateIntegral().Result;
                            Console.WriteLine($"Решение методом Симпсона: {sm}\nРешение оптимальным методом: {ow}\nПогрешность {Math.Abs(ow - sm)}");
                            Console.Read();
                            Console.Clear();
                            Main();
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Интеграл введен не до конца!");
                            Console.Read();
                            Console.Clear();
                            ShowInput();
                            return;
                        }
                    case ConsoleKey.D5:
                        Console.Clear();

                        functionBody = String.Empty;
                        a = 0;
                        b = 0;

                        Main();

                        return;
                    case ConsoleKey.D6:
                        Console.Clear();
                        return;
                }
            }
        }
    }
}