using NUnit.Framework;
using System.Linq;

[TestFixture]
public class PipelineTests
{
    [Test]
    public void Execute_EmptyPipeline_ReturnsInputData()
    {
        // Arrange
        var pipeline = new Pipeline<int>();
        var inputData = Enumerable.Range(1, 10);

        // Act
        var result = pipeline.Execute(inputData);

        // Assert
        Assert.AreEqual(inputData, result);
    }

    [Test]
    public void Execute_PipelineWithFilters_ReturnsFilteredData()
    {
        // Arrange
        var pipeline = new Pipeline<int>();
        pipeline.AddFilter(new FilterA());
        pipeline.AddFilter(new FilterB());
        var inputData = Enumerable.Range(1, 10);
        var expectedOutput = new List<int> { 4, 8, 12, 16, 20 };

        // Act
        var result = pipeline.Execute(inputData);

        // Assert
        CollectionAssert.AreEqual(expectedOutput, result);
    }
}