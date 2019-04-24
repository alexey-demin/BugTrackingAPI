namespace BugTrackingAPI.Enums
{
    public enum SortState
    {
        Default, // сортировка по умолчанию
        CreateDateAsc, // по дате создания по возрастанию
        CreateDateDesc,    // по дате создания по убыванию
        UpdateDateAsc,    // по дате изменения по возрастанию
        UpdateDateDesc,   // по дате изменения по убыванию
        PriorityAsc, // по приоритету по возрастанию
        PriorityDesс // по приоритету по убыванию
    }
}