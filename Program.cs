using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace TableFormat
{
    class Program
    {
        static double GetFunctionResult(double x)
        {
            double y = Math.Pow(x, 2) + 3*x + 17;
            return y;
        }

        static double CheckForNumber(string value) //Если введено число - возвращает его, ели нет - условный код ошибки
        {
            double parseValue;
            if (Double.TryParse(value, out parseValue))
            {
                return parseValue;
            }
            else
            {
                return -1;
            }
        }

        static double[] AskUser() //получает от пользователя необходимую для работы программы информацию
        {
            double[] inputValues = { -1, - 1, -1 };                                //значения по умолчанию равны условным кодам ошибки.
            Console.WriteLine("Введите шаг построения функции 'x^2 + 3x + 17'");//если они неизменны после выполнения кода метода, значит,
            string stepStr = Console.ReadLine().Replace('.', ',');                                //на каком-то этапе была введена некорректная информация
            double step;
            if ((step = CheckForNumber(stepStr)) > 0)
            {
                Console.WriteLine("Теперь введите число X, от которого будет вестись построение");
                string fromStr = Console.ReadLine().Replace('.', ',');
                double from;
                if ((from = CheckForNumber(fromStr)) != -1)
                {
                    Console.WriteLine("Теперь введите число X, ДО которого будет вестись построение");
                    string toStr = Console.ReadLine().Replace('.', ',');
                    double to;
                    if ((to = CheckForNumber(toStr)) != -1)
                    {
                        inputValues[0] = step; //если все введенные данные были корректны, присваиваем элементам массива введенные значения
                        inputValues[1] = from;
                        inputValues[2] = to;
                    }
                }
            }
            return inputValues;
        }

        static void PrintTable(double step, double from, double to) //рисуем таблицу
        {
            Dictionary<string, string> results = new Dictionary<string, string>(); //словарь, в котором ключи - это X, а значения - Y
            int lengthX = 0;
            int lengthY = 0;
            for (double x = from; x <= to; x += step)
            {
                string xStr = Convert.ToString(x);
                if (xStr.Length > lengthX)
                {
                    lengthX = xStr.Length; //находим максимальную длину X
                }
                double result = GetFunctionResult(x); //получаем Y от текущего X
                string resStr = Convert.ToString(result);
                if (resStr.Length > lengthY)
                {
                    lengthY = resStr.Length; //находим максимальную длину Y
                }
                results.Add(xStr, resStr); //добавляем соответствие текущий X -> Y в словарь
            }

            Console.WriteLine();                                                                    //
            PrintLine('-', lengthY + lengthX + 3);                                                  //
            string xRow = "X";                                                                      //
            string yRow = "Y";                                                                      //шапка таблицы
            Console.WriteLine("|{0}|{1}|", CenterAlign(xRow, lengthX), CenterAlign(yRow, lengthY)); //      
            PrintLine('-', lengthY + lengthX + 3);                                                  //
            
            foreach(KeyValuePair<string, string> keyValue in results) //перебираем все пары ключ -> значение (x -> y) словаря и выводим
            {
                string key = keyValue.Key;
                string value = keyValue.Value;
                Console.WriteLine("|{0}|{1}|", CenterAlign(key, lengthX), CenterAlign(value, lengthY));

            }
            
        }

        static string CenterAlign(string str, int width) //форматирование по центру
        {
            return str.PadRight(width - (width - str.Length) / 2).PadLeft(width);
        }

        static void PrintLine(char symbol, int with) //нарисовать линию из символов symbol ширины with
        {
            Console.WriteLine(new string(symbol, with));
        }


        static void Main(string[] args)
        {
            while (true)
            {
                double[] input = AskUser();
                if (input[0] == -1)
                {
                    break;
                }
                double step = input[0];
                double from = input[1];
                double to = input[2];
                PrintTable(step, from, to);
                Console.ReadKey();
            }
        }
    }
}
