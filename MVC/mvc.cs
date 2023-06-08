// Модель
public class UserModel
{
    public string Name { get; set; }
    public int Age { get; set; }
}

// Представлення
public class UserView
{
    public void DisplayUserDetails(string name, int age)
    {
        Console.WriteLine($"User: {name}, Age: {age}");
    }
}

// Контролер
public class UserController
{
    private readonly UserModel _model;
    private readonly UserView _view;

    public UserController(UserModel model, UserView view)
    {
        _model = model;
        _view = view;
    }

    public void UpdateUserData(string name, int age)
    {
        _model.Name = name;
        _model.Age = age;
    }

    public void DisplayUserData()
    {
        string name = _model.Name;
        int age = _model.Age;
        _view.DisplayUserDetails(name, age);
    }
}

// Використання
public class Program
{
    public static void Main()
    {
        // Ініціалізація моделі, представлення і контролера
        var model = new UserModel();
        var view = new UserView();
        var controller = new UserController(model, view);

        // Оновлення даних моделі
        controller.UpdateUserData("John Doe", 30);

        // Відображення даних через контролер і представлення
        controller.DisplayUserData();
    }
}