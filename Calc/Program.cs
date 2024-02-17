using System.Text;

//Создаем словарь с делегатами математических операций, если в будующем появиться необходимость расширить функционал, достаточно добавить сюда новый оператор
Dictionary<string, Func<double, double, string>> Operations = new Dictionary<string, Func<double, double, string>>
{
    { "*", (double a, double b)=>{ return (a * b).ToString(); } },
    { "/", (double a, double b)=>{
        if(b == 0) return "Ошибка, деление на ноль!";
        return (a / b).ToString(); 
    } },
    { "+", (double a, double b)=>{ return (a + b).ToString(); } },
    { "-", (double a, double b)=>{ return (a - b).ToString(); } },
    { "^", (double a, double b)=>{ return Math.Pow(a, b).ToString(); } }
};

Console.WriteLine("Программа \"Калькулятор\"");

while (true)
{
    //Создаем StringBuilder и наполняем его поддерживаемыми выражениями из словаря.
    StringBuilder sb = new StringBuilder();
    Operations.ToList().ForEach(x => sb.Append($" a {x.Key} b,"));
    //Удаляем последнюю запятую
    sb.Remove(sb.Length - 1, 1);

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
        //Разделяем строку ввода по символу оператора
        inputArg = input.Split(key);
        //Если получили больше одного элемента, значит такой оператор присутствовал в строке, запоминаем что за оператор, выходим из цикла
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
        //Парсим ввод пользователя в Double и если оба TryParse вернули true, значит всё ок, выводим результат расчета, и завершаем иттерацию цикла while
        if (double.TryParse(inputArg[0], out a) && double.TryParse(inputArg[1], out b))
        {
            Console.WriteLine($"Равно: {Operations[inputOperator](a, b)}");
            continue;
        }
    }
    Console.WriteLine("Ошибка ввода!");
    
}
