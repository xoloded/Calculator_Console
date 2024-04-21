class History
{
    private List<string> historyList = new List<string>();

    private int count = 1;
    public void HistoryAddMethod(string str)
    {
        historyList.Add(count + ") " + str);
        count++;
    }

    public void HistoryAddExpression(string str)
    {
        historyList.Add(str);
    }
    public void HistoryMethod()
    {
        Console.Clear();
        Console.WriteLine("Для выхода из режима \"история\" нажмите Enter.\n");

        if (historyList.Count == 0)
            Console.WriteLine("История пуста");
        else
        {
            foreach (var e in historyList)
                Console.WriteLine(e);
        }    
        Console.ReadLine();
    }
}

