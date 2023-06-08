using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[TestFixture]
public class MasterTests
{
    [Test]
    public async Task RunAsync_ProcessesDataForEachSlave()
    {
        // Arrange
        var slaves = new List<Slave>
        {
            new Slave(1),
            new Slave(2),
            new Slave(3)
        };
        var master = new Master(slaves);

        // Act
        await master.RunAsync();

        // Assert
        foreach (Slave slave in slaves)
        {
            // Verify that each slave processed at least one data item
            Assert.That(slave.ProcessAsync(Arg.Any<int>()).ReceivedCalls.Count, Is.GreaterThan(0));
        }
    }

    [Test]
    public async Task RunAsync_ReturnsCorrectResults()
    {
        // Arrange
        var slaves = new List<Slave>
        {
            new Slave(1),
            new Slave(2),
            new Slave(3)
        };
        var master = new Master(slaves);

        // Act
        await master.RunAsync();

        // Assert
        int[] expectedResults = slaves.Select(slave => slave.ProcessAsync(Arg.Any<int>()).Result).ToArray();
        CollectionAssert.AreEqual(expectedResults, Console.Out.ToString().Split('\n').Where(line => line.StartsWith("Result:")).Select(line => int.Parse(line.Split(':')[1].Trim())).ToArray());
    }
}