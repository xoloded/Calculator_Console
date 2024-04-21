using System.Text;

class Сalculator
{
    private readonly History _history;
    private readonly Validate _validator;

    public Сalculator()
    {
        _history = new History();
        _validator = new Validate();
    }

    private void Welcome()
    {
        Console.Clear();
        Console.WriteLine(Text._welcome + "\n");
    }

    public void Run()
    {
        Welcome();

        while (true)
        {
            Console.WriteLine("Введите режим:");
            string method = Console.ReadLine();

            method = method.Trim();
            method = method.ToLower();

            switch (method)
            {
                case "вычислить":
                    _history.HistoryAddMethod(method);
                    var result = CalculateMode();
                    _history.HistoryAddExpression(result);
                    Welcome();
                    break;
                case "правила":
                    _history.HistoryAddMethod(method);
                    Console.WriteLine(Text._operation);
                    break;
                case "история":
                    _history.HistoryMethod();
                    _history.HistoryAddMethod(method);
                    Welcome();
                    break;
                case "время":
                    _history.HistoryAddMethod(method);
                    var time = DateTime.Now;
                    Console.WriteLine(time + "\n");
                    _history.HistoryAddExpression(time.ToString());
                    break;
                default:
                    Console.WriteLine("Неизвестный режим" + "\n");
                    break;
            }
        }
    }

    private string CalculateMode()
    {
        Console.Clear();
        Console.WriteLine("Для выхода из режима \"вычислить\" введите: \"выход\".\n\nВведите выражение:");

        var hist = new StringBuilder();
        int cursorStr = 3;
        bool flag = true;
        while (flag)
        {
            string expression = Console.ReadLine();

            if (string.Equals(expression, "выход", StringComparison.CurrentCultureIgnoreCase))
                flag = false;
            else
            {
                Console.SetCursorPosition(0, cursorStr);
                string result = Calculate(expression);
                Console.Write(expression + " = " + result + "\n");
                hist.Append("\t" + expression + " = " + result + "\n");
            }
            cursorStr++; 
        }
        return hist.ToString();
    }

    private string Calculate(string expression)
    {
        string[] parts = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (!_validator.CheckInput(parts))
            return _validator.error;

        List<string> partsList = [.. parts];

        int i = 0;
        int current = 1;
        int end = partsList.Count;
        while (partsList.Count > 1)
        {
            if ((partsList[i] == "^" && current == 1) ||
               ((partsList[i] == "%" || partsList[i] == "*" || partsList[i] == "/") && current == 2) ||
               ((partsList[i] == "+" || partsList[i] == "-") && current == 3))
            {
                partsList[i - 1] = СalculateTwo(partsList[i - 1], partsList[i], partsList[i + 1]).ToString();
                partsList.RemoveRange(i, 2);
                end = partsList.Count;
                i--;
            }

            if (i == end - 1)
            {
                i = 0;
                current++;
            }
            else
                i++;
        }
        return partsList[0];
    }

    private double СalculateTwo(string op1, string operation, string op2)
    {
        double operand1 = double.Parse(op1);
        double operand2 = double.Parse(op2);
        switch (operation)
        {
            case "+":
                return operand1 + operand2;
            case "-":
                return operand1 - operand2;
            case "*":
                return operand1 * operand2;
            case "/":
                return operand1 / operand2;
            case "%":
                return operand1 % operand2;
            case "^":
                return Math.Pow(operand1, operand2);
        };
        return 0;
    }  
}