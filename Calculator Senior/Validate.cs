public class Validate
{
    public string error = "";
    public bool CheckInput(string[] parts)
    {
        if ( parts.Length == 0)
        {
            error = "Введена пустая строка";
            return false;
        }
        if (parts.Length % 2 == 1)
        {
            for (int i = 0; i < parts.Length; i += 2)
            {
                if (CheckOperand(parts[i]))
                {
                    error = "Некорректно введено число";
                    return false;
                }
            }
            for (int i = 1; i < parts.Length; i += 2)
            {
                if (CheckOperation(parts[i], double.Parse(parts[i + 1])))
                {
                    return false;
                }
            }
        }
        else
        {
            error = "В выражении потеряно число или операнд. Проверьте пробелы";
            return false;
        }
        return true;
    }
    private bool CheckOperand(string op)
    {
        return (double.TryParse(op, out double a1)) ? false : true;
    }
    private bool CheckOperation(string op, double operand)
    {
        if (op == "+" || op == "-" || op == "*" || op == "^")
        {
            return false;
        }
        else if (op == "/" || op == "%")
        {
            if (operand == 0)
            {
                error = "Деление на ноль невозможен";
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            error = "Недопустимая операция";
            return true;
        }
    }
}
