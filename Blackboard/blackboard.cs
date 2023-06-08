using System;
using System.Collections.Generic;

// Факт, який може бути записаний на дошку
public class Fact
{
    public string Name { get; set; }
    public object Value { get; set; }
}

// Експерт, який спостерігає за дошкою і робить висновки на основі фактів
public class Expert
{
    public string Name { get; set; }

    public Expert(string name)
    {
        Name = name;
    }

    public void ProcessFacts(List<Fact> facts)
    {
        Console.WriteLine($"Expert {Name} is processing facts:");

        foreach (var fact in facts)
        {
            Console.WriteLine($"- Fact: {fact.Name}, Value: {fact.Value}");
        }

        // Виконання аналізу та генерація рішень
        // ...
    }
}

// Контролер, який керує взаємодією між експертами та дошкою
public class Controller
{
    private readonly List<Expert> _experts;
    private readonly List<Fact> _facts;

    public Controller()
    {
        _experts = new List<Expert>();
        _facts = new List<Fact>();
    }

    public void AddExpert(Expert expert)
    {
        _experts.Add(expert);
    }

    public void AddFact(Fact fact)
    {
        _facts.Add(fact);
        NotifyExperts();
    }

    private void NotifyExperts()
    {
        foreach (var expert in _experts)
        {
            expert.ProcessFacts(_facts);
        }
    }
}

// Використання
public class Program
{
    public static void Main()
    {
        var controller = new Controller();

        var expert1 = new Expert("Expert1");
        var expert2 = new Expert("Expert2");

        controller.AddExpert(expert1);
        controller.AddExpert(expert2);

        var fact1 = new Fact { Name = "Fact1", Value = 10 };
        var fact2 = new Fact { Name = "Fact2", Value = "Hello" };

        controller.AddFact(fact1);
        controller.AddFact(fact2);
    }
}