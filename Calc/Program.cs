//Создаем словарь с делегатами математических операций, если в будующем появиться необходимость расширить функционал, достаточно добавить сюда новый оператор
using System.Text;

Dictionary<string, Func<double, double, double>> Operations= new Dictionary<string, Func<double, double, double>>
{
    { "*", (double a, double b)=>{ return a * b; } },
    { "/", (double a, double b)=>{ return a / b; } },
    { "+", (double a, double b)=>{ return a + b; } },
    { "-", (double a, double b)=>{ return a - b; } },
    { "^", (double a, double b)=>{ return Math.Pow(a, b); } }
};

Console.WriteLine("Программа \"Калькулятор\"");

while (true)
{
    //Создаем StringBuilder и наполняем его поддерживаемыми выражениями из словаря.
    StringBuilder sb = new StringBuilder();
    Operations.ToList().ForEach(x => sb.Append($" a {x.Key} b,"));
    //Удаляем последнюю запятую
    sb.Remove(sb.Length-1, 1);

    Console.WriteLine($"Введите выражение вида {sb} или q для выхода");
    //Читаем ввод пользователя
    string input = Console.ReadLine();
    //Удаляем пробелы
    input = input.Replace(" ", "");
    //Если ввели q, выходим из программы
    if (input == "q") break;
    //Переменная для хранения оператора который ввел пользователь
    string inputOperator = null;
    //Тут будем хранить массив из двух найденных элементов a и b
    string[] inputArg = null;

    //Пробегаемся по списку наших возможных операторв и ищем их в введенных пользователем данных
    foreach (string key in Operations.Keys)
    {
        inputArg = input.Split(key);

        if (inputArg.Length > 1)
        {
            inputOperator = key;
            break;
        };
    }

    //Если имеется введенный оператор, и кол-во элементов для операции 2, то всё ок
    if (inputOperator != null && inputArg != null && inputArg.Length == 2)
    {
        double a;
        double b;
        //Парсим ввод пользователя в Double и если хоть один парсер вернул false, значит что-то не так с вводом, выводим ошибку.
        if (double.TryParse(inputArg[0], out a) == false || double.TryParse(inputArg[1], out b) == false) Console.WriteLine("Ошибка ввода!");
        //А если всё ок, выполняем функцию, которая соответствует введенному пользователем оператору
        else Console.WriteLine($"Равно: {Operations[inputOperator](a, b)}");
    }
    else
    {
        Console.WriteLine("Ошибка ввода!");
    }
}
