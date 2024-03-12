// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

public class ObservableLimitedList(Predicate<string> predicate, uint size)
{
    private Predicate<string> _predicate = predicate;
    private uint _size = size;
    private List<string> _items = new List<string>();

    public delegate void ListChangedEventHandler(object source, ListChangedEventArgs e);
    public event ListChangedEventHandler? ListChanged;
    public IEnumerable<string> GetItems() => _items.AsReadOnly();


    public bool TryAdd(string item)
    {
        if (!(_items.Count > _size) && _predicate(item))
        {
            _items.Add(item);
            ListChanged?.Invoke(this, new ListChangedEventArgs(item));

            if (_items.Count >= _size)
            {
                PrintAll();
            }
            return true;
        }
        return false;
    }

    public void PrintAll()
    {
        Console.WriteLine($"All {_items.Count} list items:");
        foreach (var item in _items)
        {
            Console.WriteLine(item);
        }
    }
}

public class ListChangedEventArgs : EventArgs
{
    public string ItemChanged { get; }

    public ListChangedEventArgs(string itemChanged)
    {
        ItemChanged = itemChanged;
    }
}
