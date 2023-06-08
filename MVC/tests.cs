using NUnit.Framework;

[TestFixture]
public class UserControllerTests
{
    [Test]
    public void UpdateUserData_ValidInput_UpdatesModelData()
    {
        // Arrange
        var model = new UserModel();
        var viewMock = new Mock<UserView>();
        var controller = new UserController(model, viewMock.Object);
        string name = "John Doe";
        int age = 30;

        // Act
        controller.UpdateUserData(name, age);

        // Assert
        Assert.AreEqual(name, model.Name);
        Assert.AreEqual(age, model.Age);
    }

    [Test]
    public void DisplayUserData_CallsViewDisplayUserDetailsWithModelData()
    {
        // Arrange
        var model = new UserModel();
        var viewMock = new Mock<UserView>();
        var controller = new UserController(model, viewMock.Object);
        string name = "John Doe";
        int age = 30;
        model.Name = name;
        model.Age = age;

        // Act
        controller.DisplayUserData();

        // Assert
        viewMock.Verify(v => v.DisplayUserDetails(name, age), Times.Once);
    }
}

[TestFixture]
public class UserViewTests
{
    [Test]
    public void DisplayUserDetails_ValidInput_DisplaysUserDetails()
    {
        // Arrange
        var view = new UserView();
        string name = "John Doe";
        int age = 30;
        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        string expectedOutput = $"User: {name}, Age: {age}";

        // Act
        view.DisplayUserDetails(name, age);
        string actualOutput = stringWriter.ToString().Trim();

        // Assert
        Assert.AreEqual(expectedOutput, actualOutput);
    }
}