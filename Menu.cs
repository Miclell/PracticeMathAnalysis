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
            decimal intervals = 0;
            bool trigger = false;

            //functionBody = "sqrt(xsqrt(xsqrt(x)))";
            //a = 4; b = 8;

            //functionBody = "x^8*cos(x)+x^4";
            //a = 3; b = 5.89m;

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
                Console.WriteLine($"Тело интеграла: {(functionBody == String.Empty ? "ожидается" : functionBody)}\tНижний предел: {(a == b ? "ожидается" : a)}\tВерхний предел: {(a == b ? "ожидается" : b)}\tКоличесвто инетвралов: {(trigger ? intervals : "ожидается")}");

                Console.WriteLine("1. Ввести тело интеграла\n2. Ввести нижний предел инетерирования\n3. Ввести верхний предел интегрирования\n4. Ввести количество инетрвалов (необязательно)\n5. Закончить ввод\n6. Показать инструкцию");

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
                        Console.WriteLine("Ввести количество инетрвалов:");
                        intervals = Convert.ToDecimal(Console.ReadLine(), nfi);
                        trigger = true;
                        Console.Clear();
                        ShowInput();
                        return;
                    case ConsoleKey.D5:
                        Console.Clear();
                        Main();
                        return;
                    case ConsoleKey.D6:
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
                            Console.WriteLine($"Результат при решении формулой Симпсона: {function2.SimpsonMethod().Result}\nКоличество интервалов: {(trigger ? intervals : function2.CalculateIntervals())}");
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
                            decimal sm, om = function.CalculateIntegral().Result;

                            if (trigger)
                                sm = function.SimpsonMethod(intervals, trigger).Result;
                            else
                                sm = function.SimpsonMethod().Result;

                            Console.WriteLine($"Решение методом Симпсона: {sm}\nРешение оптимальным методом: {om}\nПогрешность: {Math.Abs(om - sm)}\nКоличество интервалов: {(trigger ? intervals : function.CalculateIntervals())}");
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