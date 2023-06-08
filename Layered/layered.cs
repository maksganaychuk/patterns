// Presentation Layer
// -----------------
// Цей шар відповідає за взаємодію з користувачем і відображення даних.
// Він містить форми, сторінки, контролери тощо.

public class UserController
{
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;

    public UserController(IUserService userService, IEmailService emailService)
    {
        _userService = userService;
        _emailService = emailService;
    }

    public void RegisterUser(string username, string email, string password)
    {
        // Виклик сервісного шару для реєстрації користувача
        var user = _userService.Register(username, email, password);

        // Відправлення листа з підтвердженням реєстрації
        _emailService.SendConfirmationEmail(user.Email);
    }
}

// Service Layer
// -------------
// Цей шар містить бізнес-логіку і правила обробки даних.
// Він виконує операції з даними, використовуючи репозиторії і інші сервіси.

public interface IUserService
{
    User Register(string username, string email, string password);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User Register(string username, string email, string password)
    {
        // Логіка реєстрації користувача

        var user = new User(username, email, password);

        // Збереження користувача в репозиторії
        _userRepository.Save(user);

        return user;
    }
}

// Data Access Layer
// ----------------
// Цей шар відповідає за доступ до даних і взаємодію з базою даних або зовнішніми джерелами даних.

public interface IUserRepository
{
    void Save(User user);
}

public class UserRepository : IUserRepository
{
    public void Save(User user)
    {
        // Логіка збереження користувача в базі даних
    }
}

// Domain Model
// ------------
// Цей шар містить основні моделі даних, логіку домену та правила бізнес-логіки.

public class User
{
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    public User(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
    }
}

// Infrastructure Layer
// -------------------
// Цей шар містить додаткові сервіси, утиліти, зовнішні бібліотеки і реалізації різних інтерфейсів.

public interface IEmailService
{
    void SendConfirmationEmail(string emailAddress);
}

public class EmailService : IEmailService
{
    public void SendConfirmationEmail(string emailAddress)
    {
        // Логіка відправлення листа з підтвердженням реєстрації
    }
}