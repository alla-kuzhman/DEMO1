using DEMO1.Data;
using DEMO1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Windows;

namespace DEMO1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppDbContext db = new AppDbContext();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text;
            string pass = PasswordBox.Password;
            var user = db.Users.FirstOrDefault(u => u.Login == login && u.Password == pass);
            if (user != null)
            {
                ProductWindow productWindow = new ProductWindow(user.Role);
                productWindow.Show();
                this.Close();
            }
            else
            {
                ErrorText.Text = "Неверный логин или пароль";
            }
        }
        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow("Guest");
            productWindow.Show();
            this.Close();
        }
    }
}



//USE ToyStoreDB;

//CREATE TABLE Users (
//    Id INT PRIMARY KEY IDENTITY(1,1),
//    Login NVARCHAR(100) NOT NULL,
//    Password NVARCHAR(100) NOT NULL,
//    FullName NVARCHAR(200) NOT NULL,
//    Role NVARCHAR(50) NOT NULL DEFAULT 'Client'
//);

//CREATE TABLE Products (
//    Id INT PRIMARY KEY IDENTITY(1,1),
//    Article NVARCHAR(50) NOT NULL,
//    Name NVARCHAR(200) NOT NULL,
//    Price INT NOT NULL,
//    Category NVARCHAR(100) NOT NULL,
//    Quantity INT NOT NULL,
//    Discount INT DEFAULT 0,
//    ImagePath NVARCHAR(500) NULL
//);

//CREATE TABLE Orders (
//    Id INT PRIMARY KEY IDENTITY(1,1),
//    OrderNumber NVARCHAR(50) NOT NULL,
//    OrderDate DATETIME NOT NULL,
//    DeliveryDate DATETIME NULL,
//    PickupPoint NVARCHAR(500) NOT NULL,
//    ClientName NVARCHAR(200) NOT NULL,
//    Status NVARCHAR(50) NOT NULL DEFAULT 'Новый'
//);
//INSERT INTO Users (Login, Password, FullName, Role) VALUES 
//('admin', '123', 'Администратор', 'Admin'),
//('manager', '123', 'Петрова Елена Сергеевна', 'Manager'),
//('client1', '123', 'Иванов Иван Петрович', 'Client'),
//('client2', '123', 'Сидорова Мария Алексеевна', 'Client');

//INSERT INTO Products (Article, Name, Price, Category, Quantity, Discount, ImagePath) VALUES 
//('T001', 'Мишка Тедди плюшевый', 1500, 'Мягкие игрушки', 25, 10, 'bear.jpg'),
//('T002', 'LEGO City Пожарная станция', 4990, 'Конструкторы', 8, 5, 'lego.jpg'),
//('T003', 'Barbie Dreamhouse', 8990, 'Куклы', 3, 20, 'barbie.jpg'),
//('T004', 'Hot Wheels Набор машинок', 1200, 'Машинки', 45, 15, 'hotwheels.jpg'),
//('T005', 'Кукла LOL Surprise', 2500, 'Куклы', 18, 10, 'lol.jpg'),
//('T006', 'Мяч футбольный', 800, 'Спорт', 32, 0, 'ball.jpg'),
//('T007', 'Монополия', 2100, 'Настольные игры', 12, 25, 'monopoly.jpg'),
//('T008', 'Робот-трансформер', 3500, 'Роботы', 7, 30, 'robot.jpg'),
//('T009', 'Пазл 1000 деталей', 900, 'Пазлы', 22, 5, 'puzzle.jpg'),
//('T010', 'Динозавр на радиоуправлении', 4200, 'Радиоуправляемые', 5, 18, 'dino.jpg'),
//('T011', 'Зайка плюшевый', 1200, 'Мягкие игрушки', 30, 8, 'rabbit.jpg'),
//('T012', 'Железная дорога', 6700, 'Конструкторы', 4, 12, 'train.jpg'),
//('T013', 'Кукла Беби Бон', 3200, 'Куклы', 9, 15, 'babybon.jpg'),
//('T014', 'Машинка на пульте', 2800, 'Радиоуправляемые', 14, 22, 'rc_car.jpg'),
//('T015', 'Набор фигурок животных', 950, 'Коллекционные', 40, 0, 'animals.jpg');

//INSERT INTO Orders (OrderNumber, OrderDate, DeliveryDate, PickupPoint, ClientName, Status) VALUES 
//('ORD-001', GETDATE(), DATEADD(day, 7, GETDATE()), 'г. Оренбург, ул. Советская, 48', 'Иванов Иван Петрович', 'Новый'),
//('ORD-002', GETDATE(), DATEADD(day, 5, GETDATE()), 'г. Оренбург, пр. Победы, 13', 'Иванов Иван Петрович', 'Завершен'),
//('ORD-003', GETDATE(), DATEADD(day, 10, GETDATE()), 'г. Оренбург, ул. Терешковой, 256', 'Сидорова Мария Алексеевна', 'В обработке'),
//('ORD-004', DATEADD(day, -3, GETDATE()), DATEADD(day, 4, GETDATE()), 'г. Оренбург, ул. Чкалова, 22', 'Иванов Иван Петрович', 'Завершен'),
//('ORD-005', DATEADD(day, -7, GETDATE()), DATEADD(day, -1, GETDATE()), 'г. Оренбург, ул. Постникова, 30', 'Сидорова Мария Алексеевна', 'Завершен');
