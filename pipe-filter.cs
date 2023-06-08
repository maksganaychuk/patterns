using System;
using System.Collections.Generic;

// Інтерфейс для фільтра
public interface IFilter<T>
{
    IEnumerable<T> Process(IEnumerable<T> input);
}

// Фільтр для обробки даних
public class FilterA : IFilter<int>
{
    public IEnumerable<int> Process(IEnumerable<int> input)
    {
        List<int> output = new List<int>();
        foreach (var item in input)
        {
            // Виконати обробку даних (наприклад, фільтрацію)
            if (item % 2 == 0)
            {
                output.Add(item);
            }
        }
        return output;
    }
}

// Інший фільтр для обробки даних
public class FilterB : IFilter<int>
{
    public IEnumerable<int> Process(IEnumerable<int> input)
    {
        List<int> output = new List<int>();
        foreach (var item in input)
        {
            // Виконати обробку даних (наприклад, мапування)
            int result = item * 2;
            output.Add(result);
        }
        return output;
    }
}

// Компонент, який керує послідовним виконанням фільтрів
public class Pipeline<T>
{
    private readonly List<IFilter<T>> _filters;

    public Pipeline()
    {
        _filters = new List<IFilter<T>>();
    }

    public void AddFilter(IFilter<T> filter)
    {
        _filters.Add(filter);
    }

    public IEnumerable<T> Execute(IEnumerable<T> input)
    {
        IEnumerable<T> currentData = input;
        foreach (var filter in _filters)
        {
            currentData = filter.Process(currentData);
        }
        return currentData;
    }
}

// Використання
public class Program
{
    public static void Main()
    {
        // Створення фільтрів
        var filterA = new FilterA();
        var filterB = new FilterB();

        // Створення конвеєра та додавання фільтрів
        var pipeline = new Pipeline<int>();
        pipeline.AddFilter(filterA);
        pipeline.AddFilter(filterB);

        // Вхідні дані для обробки
        List<int> inputData = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        // Виконання обробки через конвеєр
        IEnumerable<int> result = pipeline.Execute(inputData);

        // Виведення результатів
        Console.WriteLine("Pipeline Result:");
        foreach (var item in result)
        {
            Console.WriteLine(item);
        }
    }
}