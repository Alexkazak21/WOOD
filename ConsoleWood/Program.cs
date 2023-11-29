using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using ConsoleWood;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8602,CS8604 // Разыменование вероятной пустой ссылки.

Console.WriteLine("Welcome to woodcut placement\n".ToUpper());

bool life = true;

while (life)
{
startMark:
    
    Console.WriteLine("Доступный функционал :\n" +
        "1. Авторизация\n" +
        "2. Зарегистрироваться\n" +
        "3. О программе\n" +
        "4. Завершение работы\n" +
        "Выберите нужный функционал");

    string? str = Console.ReadLine();

    if (!int.TryParse(str, out int simofor) || simofor > 4 || simofor < 1)
    {
        Console.WriteLine("Ошибка ввода, Повторите попытку снова");
        Console.WriteLine("Для продолжения нажмите любую клавишу ....");
        Console.ReadKey();
        Console.Clear();
        goto startMark;
    }

    bool authotiezed = false;
    switch (simofor)
    {
        case 1:   //Работа блока АВТОРИЗАЦИИ
            {
                int id = -1;
                Console.Clear();
                authotiezed =  Autoriz(out id);
                if (authotiezed)
                {
                    AuthWork(authotiezed,id);
                }                
                Console.Clear();
                break;
            }
        case 2: // Работа Блока ЗАРЕГИСТРИРОВАТЬСЯ
            {
                Console.Clear();
                Registration();
                Console.Clear();
                break;
            }
        case 3: // Раблта блока О ПРОГРАММЕ
            {
                Console.Clear();
                Console.WriteLine("Создана для учёбы");
                Console.WriteLine("Для продолжения нажмите любую клавишу ....");
                Console.ReadKey();
                Console.Clear();
                
                break;
            }
        default:   // Работа блока ЗАВЕРШЕНИЕ РАБОТЫ
            {
                life = false;
                Console.Clear();
                break;
            }
    }
}

bool Autoriz(out int id)
{
    using (WoodContext dbconn = new WoodContext())
    {
        Console.Clear();

        Console.WriteLine("Введите логин");
        string? login = Console.ReadLine();

        if (!dbconn.Customers.Select(item => item.Name).Contains(login))
        {
            Console.WriteLine($"Пользователя с именем {login} не существует в системе");
            Console.WriteLine("Для продолжения нажмите любую клавишу ....");
            Console.ReadKey();
            id = -1;
            return false;
        }
        else
        {
            Console.WriteLine("Введите пароль");
            string? password = Console.ReadLine();


            if (dbconn.Customers.Select(item => item).Where(item => item.Name == login && item.Pass == password).Any())
            {
                Console.WriteLine($"{login} добро пожаловать в систему");
                id = dbconn.Customers.Where(item => item.Name == login).Where(item => item.Pass == password).First().Id;
                Console.WriteLine("Для продолжения нажмите любую клавишу ....");
                Console.ReadKey();
                return true;
            }
        }

        id = -1;
        return false;
    }
}

void AuthWork(bool i, int customerid)
{
    using (WoodContext dbConn = new WoodContext())
    {
        Console.Clear();
        if (i)
        {
            bool t = true;
            while (t)
            {
                Console.WriteLine("1. Мои заказы\n" +
                    "2. Добавить заказ\n" +
                    "3. Удалить заказ\n" +
                    "4. Выйти\n\n" +
                    "Выберите необходимый функционал");
                int d = int.Parse(Console.ReadLine());

                switch (d)
                {
                    case 1: //Выполение блока "МОИ ЗАКАЗЫ"
                        {
                            Console.Clear();

                            var ordersArray = dbConn.Orders.Where(item => item.CustomerId == customerid).Select(item => item.OrderId).Distinct().ToList();

                            if (ordersArray.Count() != 0)
                            {
                                foreach (var iterator in ordersArray)
                                {
                                    Console.WriteLine($"Заказ номер {iterator} содержит объем {(dbConn.Orders.Where(item => item.CustomerId == customerid && item.OrderId == iterator).Join(dbConn.Timbers, o => o.TimberId, t => t.Id, (o, t) => t.Volume).Sum()):f4} m3 древисины подлежащей распилу");
                                }


                                Console.WriteLine("Для продолжения нажмите любую клавишу ....");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            else
                            {
                                Console.WriteLine("Nothing to see");
                                Console.WriteLine("Для продолжения нажмите любую клавишу ....");
                                Console.ReadKey();

                                Console.Clear();
                            }
                            break;
                        }
                    case 2:  //выполнение блока "ДОБАВИТЬ ЗАКАЗ"
                        {
                            // получение и инициализация идентификатора заказа
                            int orderIdentificator = dbConn.Orders.Max(item => item.OrderId) + 1;

                            Console.Clear();
                            List<Timber> timbers = null!;

                            // получение списка бревен

                            using (WoodContext dbconn = new WoodContext())
                            {
                                timbers = dbconn.Timbers.ToList();
                            }

                            bool contin_ue = true;
                            double timbLength = 0d;
                            int timbDiameter = 0;
                            while (contin_ue)
                            {
                            timbInput:
                                Console.WriteLine($"Суммарно у вас {(dbConn.Orders.Where(item => item.CustomerId == customerid && item.OrderId == orderIdentificator).Join(dbConn.Timbers, o => o.TimberId, t => t.Id, (o, t) => t.Volume).Sum()):f4} м3 древисины подлежащей распилу\n\n" +
                                    $"1. Добавить дерево\n" +
                                    $"2. Выйти\n" +
                                    $"Выберите необходимое действие...");
                                int chek = 0;
                                try
                                {
                                    int.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.InvariantCulture, out chek);
                                    Console.Clear();
                                    if (chek > 2 || chek < 1)
                                    {
                                        Console.WriteLine("Вы ошиблись с выбором параметра, попробуйте снова");
                                        Console.WriteLine("Для продолжения нажмите любую клавишу ....");
                                        Console.ReadKey();
                                        Console.Clear();
                                        goto timbInput;
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Вы ошиблись с выбором параметра, попробуйте снова");
                                    Console.WriteLine("Для продолжения нажмите любую клавишу ....");
                                    Console.ReadKey();
                                    Console.Clear();
                                    goto timbInput;
                                }

                                switch (chek)
                                {
                                    case 1:
                                        {
                                        timberLen:
                                            Console.WriteLine("Введите длинну бревна числом в \"м\" в формате \"1.5\" или \"1.2\" в диапазоне от 1,5м до 6,9м включительно)");
                                            double.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.InvariantCulture, out timbLength);
                                            if (timbLength > 6.9 || timbLength < 1.5)
                                            {
                                                Console.WriteLine("Вы ошиблись с размером, попробуйте снова");
                                                Console.WriteLine("Для продолжения нажмите любую клавишу ....");
                                                Console.ReadKey();
                                                Console.Clear();
                                                goto timberLen;
                                            }
                                        timbDiam:
                                            Console.WriteLine("Введите диаметер бревна числом в \"см\" в формате \"15\" в диапазоне от 14см до 100см включительно)");
                                            int.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.InvariantCulture, out timbDiameter);
                                            if (timbDiameter > 100 || timbDiameter < 14)
                                            {
                                                Console.WriteLine("Вы ошиблись с размером, попробуйте снова");
                                                Console.WriteLine("Для продолжения нажмите любую клавишу ....");
                                                Console.ReadKey();
                                                Console.Clear();
                                                goto timbDiam;
                                            }

                                            // Проверка на существование введённого значения для длинны в базе

                                            if (!dbConn.Kubs.Where(item => item.Length == timbLength).Any())
                                            {
                                                timbLength = dbConn.Kubs.Where(item => item.Length > timbLength).Min(item => item.Length);
                                            }
                                            else
                                            {
                                                timbLength = dbConn.Kubs.Where(item => item.Length == timbLength).Select(item => item.Length).First();
                                            }

                                            // Проверка на существование введённого значения для диаметра в базе

                                            if (!dbConn.Kubs.Where(item => item.Diameter == timbDiameter).Any())
                                            {
                                                timbDiameter = dbConn.Kubs.Where(item => item.Diameter > timbDiameter).Min(item => item.Diameter);
                                            }
                                            else
                                            {
                                                timbDiameter = dbConn.Kubs.Where(item => item.Diameter == timbDiameter).Select(item => item.Diameter).First();
                                            }

                                            using (WoodContext dbConnection = new WoodContext())
                                            {
                                                // сохранение данных о добавленном дереве  и изменившемся заказе в базу

                                                dbConnection.Timbers.Add(new Timber() { Length = timbLength, Diameter = timbDiameter, Volume = dbConn.Kubs.Where(item => item.Diameter == timbDiameter && item.Length == timbLength).Select(item => item.Value).First() });
                                                dbConnection.SaveChanges();
                                                dbConnection.Orders.Add(new Order() { CustomerId = customerid, OrderId = orderIdentificator, TimberId = dbConnection.Timbers.Select(i => i).Max(j => j.Id) });
                                                dbConnection.SaveChanges();
                                            }

                                            Console.Clear();
                                            break;
                                        }
                                    default:
                                        {
                                            contin_ue = false;

                                            Console.Clear();
                                            break;
                                        }
                                }


                            }

                            break;
                        }
                    case 3: // Выполнение блока "УДАЛИТЬ ЗАКАЗ"
                        {
                            Console.Clear();

                            var ordersArray = dbConn.Orders.Where(item => item.CustomerId == customerid).Select(item => item.OrderId).Distinct().ToList();
                selectOrder:
                            if (ordersArray.Count() != 0)
                            {
                                foreach (var iterator in ordersArray)
                                {
                                    Console.WriteLine($"Заказ номер {iterator} содержит объем {(dbConn.Orders.Where(item => item.CustomerId == customerid && item.OrderId == iterator).Join(dbConn.Timbers, o => o.TimberId, t => t.Id, (o, t) => t.Volume).Sum()):f4} m3 древисины подлежащей распилу");
                                }
                                Console.WriteLine("0. Выйти");
                            }
                            else 
                            {
                                Console.WriteLine("У вас ещё нет заказов");
                                Console.WriteLine("Для продолжения нажмите любую клавишу ....");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }

                            Console.WriteLine("Выберите заказ который хотите удалить");
                            int orderIdToDelete = 0;
                            int.TryParse(Console.ReadLine(),out orderIdToDelete);

                            if (orderIdToDelete == 0)
                            { 
                                Console.Clear();
                                break;
                            }
                            else if (ordersArray.Contains(orderIdToDelete))
                            {
                                dbConn.Orders.Where(item => item.CustomerId == customerid && item.OrderId == orderIdToDelete).ExecuteDelete();
                                dbConn.SaveChangesAsync();
                            }
                            else
                            {
                                Console.WriteLine("Указанного заказа у вас не существует\n" +
                                    "Повторите попытку ввода");
                                Console.WriteLine("Для продолжения нажмите любую клавишу ....");
                                Console.ReadKey();
                                Console.Clear();
                                goto selectOrder;
                            }

                            Console.Clear();
                                break;
                        }
                    default:
                        {
                            t = false;
                            break;
                        }
                }
            }
        }
    }
}

void Registration()
{
RegistrationMark:
    Console.Clear();
    Console.WriteLine("Добро пожаловать в окно регистрации");

    Console.WriteLine("Введите логин");
    string? login = Console.ReadLine();
passwordMark:
    Console.WriteLine("Введите пароль");
    string? password1 = Console.ReadLine();
    Console.WriteLine("Введите повторно пароль");
    string? password2 = Console.ReadLine();

    using (WoodContext dbConn = new WoodContext())
    {
        if (!dbConn.Customers.Select(item => item.Name).Contains(login))
        {
            if (password1 == password2)
            {
#pragma warning disable 
                dbConn.Customers.Add(new Customer() { Name = login, Pass = password1 });
                dbConn.SaveChanges();
                Console.WriteLine($"Пользователь с именем {login} успешно создан");
                Console.WriteLine("Для продолжения нажмите любую клавишу ....");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Введённые пароли не совпадают\nПовторите попытку");
                Console.WriteLine("Для продолжения нажмите любую клавишу ....");
                Console.ReadKey();
                goto passwordMark;
            }
        }
        else
        {
            Console.WriteLine($"Ошибка операции");
            Console.WriteLine("Повторите попытку регистрации заново");
            Console.WriteLine("Для продолжения нажмите любую клавишу ....");
            Console.ReadKey();
            goto RegistrationMark;
        }
#pragma warning enable
    }
}