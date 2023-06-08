using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// Слейв
public class Slave
{
    private readonly int _id;

    public Slave(int id)
    {
        _id = id;
    }

    public async Task<int> ProcessAsync(int data)
    {
        Console.WriteLine($"Slave {_id}: Processing data {data}...");
        // Виконати обчислення або операції над даними
        await Task.Delay(1000); // Приклад затримки для ілюстрації обчислень
        int result = data * 2;
        Console.WriteLine($"Slave {_id}: Processed data {data}, Result: {result}");
        return result;
    }
}

// Мастер
public class Master
{
    private readonly List<Slave> _slaves;

    public Master(List<Slave> slaves)
    {
        _slaves = slaves;
    }

    public async Task RunAsync()
    {
        // Початкові дані для обчислення
        List<int> input = new List<int> { 1, 2, 3, 4, 5 };

        // Створення списка завдань
        List<Task<int>> tasks = new List<Task<int>>();
        foreach (var data in input)
        {
            // Надіслати дані до випадкового слейва для обробки
            int slaveIndex = new Random().Next(0, _slaves.Count);
            Slave slave = _slaves[slaveIndex];
            tasks.Add(slave.ProcessAsync(data));
        }

        // Очікування завершення всіх завдань
        int[] results = await Task.WhenAll(tasks);

        // Виведення результатів
        Console.WriteLine("Master: Results:");
        for (int i = 0; i < input.Count; i++)
        {
            Console.WriteLine($"Data: {input[i]}, Result: {results[i]}");
        }
    }
}

// Використання
public class Program
{
    public static void Main()
    {
        // Створення слейвів
        List<Slave> slaves = new List<Slave>
        {
            new Slave(1),
            new Slave(2),
            new Slave(3)
        };

        // Створення мастера і запуск
        var master = new Master(slaves);
        master.RunAsync().Wait();
    }
}