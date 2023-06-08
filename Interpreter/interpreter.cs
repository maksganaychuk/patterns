using System;
using System.Collections.Generic;

// Абстрактний вираз
public abstract class Expression
{
    public abstract int Interpret();
}

// Термінальний вираз
public class NumberExpression : Expression
{
    private readonly int _number;

    public NumberExpression(int number)
    {
        _number = number;
    }

    public override int Interpret()
    {
        return _number;
    }
}

// Нетермінальний вираз
public class AddExpression : Expression
{
    private readonly Expression _leftExpression;
    private readonly Expression _rightExpression;

    public AddExpression(Expression leftExpression, Expression rightExpression)
    {
        _leftExpression = leftExpression;
        _rightExpression = rightExpression;
    }

    public override int Interpret()
    {
        return _leftExpression.Interpret() + _rightExpression.Interpret();
    }
}

// Клієнтський код
public class Client
{
    public static void Main()
    {
        // Створення виразу 5 + (10 - 3)
        Expression expression = new AddExpression(
            new NumberExpression(5),
            new AddExpression(
                new NumberExpression(10),
                new NumberExpression(-3)
            )
        );

        int result = expression.Interpret();
        Console.WriteLine("Result: " + result);
    }
}