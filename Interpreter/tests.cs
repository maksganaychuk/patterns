using NUnit.Framework;

[TestFixture]
public class ExpressionTests
{
    [Test]
    public void NumberExpression_Interpret_ReturnsNumber()
    {
        // Arrange
        Expression expression = new NumberExpression(5);

        // Act
        int result = expression.Interpret();

        // Assert
        Assert.AreEqual(5, result);
    }

    [Test]
    public void AddExpression_Interpret_ReturnsSum()
    {
        // Arrange
        Expression expression = new AddExpression(
            new NumberExpression(5),
            new NumberExpression(10)
        );

        // Act
        int result = expression.Interpret();

        // Assert
        Assert.AreEqual(15, result);
    }

    [Test]
    public void ComplexExpression_Interpret_ReturnsCorrectResult()
    {
        // Arrange
        Expression expression = new AddExpression(
            new NumberExpression(5),
            new AddExpression(
                new NumberExpression(10),
                new NumberExpression(-3)
            )
        );

        // Act
        int result = expression.Interpret();

        // Assert
        Assert.AreEqual(12, result);
    }
}