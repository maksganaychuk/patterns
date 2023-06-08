using NUnit.Framework;

[TestFixture]
public class UserControllerTests
{
    [Test]
    public void RegisterUser_ValidInput_CallsUserServiceRegisterAndEmailServiceSendConfirmationEmail()
    {
        // Arrange
        var userServiceMock = new Mock<IUserService>();
        var emailServiceMock = new Mock<IEmailService>();
        var userController = new UserController(userServiceMock.Object, emailServiceMock.Object);
        string username = "testuser";
        string email = "testuser@example.com";
        string password = "password";

        // Act
        userController.RegisterUser(username, email, password);

        // Assert
        userServiceMock.Verify(s => s.Register(username, email, password), Times.Once);
        emailServiceMock.Verify(s => s.SendConfirmationEmail(email), Times.Once);
    }
}

[TestFixture]
public class UserServiceTests
{
    [Test]
    public void Register_ValidInput_CreatesUserAndCallsUserRepositorySave()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var userService = new UserService(userRepositoryMock.Object);
        string username = "testuser";
        string email = "testuser@example.com";
        string password = "password";

        // Act
        var result = userService.Register(username, email, password);

        // Assert
        userRepositoryMock.Verify(r => r.Save(result), Times.Once);
        Assert.AreEqual(username, result.Username);
        Assert.AreEqual(email, result.Email);
        Assert.AreEqual(password, result.Password);
    }
}

[TestFixture]
public class UserRepositoryTests
{
    [Test]
    public void Save_ValidUser_CallsDatabaseSaveMethod()
    {
        // Arrange
        var userRepository = new UserRepository();
        var userMock = new Mock<User>();

        // Act
        userRepository.Save(userMock.Object);

        // Assert
        // Add assertions to verify that the user is saved to the database
    }
}

[TestFixture]
public class EmailServiceTests
{
    [Test]
    public void SendConfirmationEmail_ValidEmailAddress_CallsEmailServiceSendMethod()
    {
        // Arrange
        var emailService = new EmailService();
        string emailAddress = "testuser@example.com";

        // Act
        emailService.SendConfirmationEmail(emailAddress);

        // Assert
        // Add assertions to verify that the confirmation email is sent
    }
}