using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[TestFixture]
public class ExpertTests
{
    [Test]
    public void ProcessFacts_WritesFactsToConsole()
    {
        // Arrange
        string expertName = "Expert1";
        Expert expert = new Expert(expertName);
        List<Fact> facts = new List<Fact>
        {
            new Fact { Name = "Fact1", Value = 10 },
            new Fact { Name = "Fact2", Value = "Hello" }
        };
        StringWriter consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        // Act
        expert.ProcessFacts(facts);

        // Assert
        string expectedOutput = $"Expert {expertName} is processing facts:\n" +
            "- Fact: Fact1, Value: 10\n" +
            "- Fact: Fact2, Value: Hello\n";
        Assert.AreEqual(expectedOutput, consoleOutput.ToString());
    }
}