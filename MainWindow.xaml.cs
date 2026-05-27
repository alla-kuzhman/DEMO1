using DEMO1.Data;
using DEMO1.Models;
using System;
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


//USE ShoeStoreDB;


//--Таблица пользователей
//CREATE TABLE Users (
//    Id INT PRIMARY KEY IDENTITY(1,1),
//    Login NVARCHAR(100) NOT NULL,
//    Password NVARCHAR(100) NOT NULL,
//    FullName NVARCHAR(200) NOT NULL,
//    Role NVARCHAR(50) NOT NULL DEFAULT 'Client'
//);

//--Таблица товаров
//CREATE TABLE Products (
//    Id INT PRIMARY KEY IDENTITY(1,1),
//    Article NVARCHAR(50) NOT NULL,
//    Name NVARCHAR(200) NOT NULL,
//    Price INT NOT NULL,
//    Category NVARCHAR(100) NOT NULL,
//    Quantity INT NOT NULL,
//    ImagePath NVARCHAR(500) NULL
//);

//--Таблица заказов
//CREATE TABLE Orders (
//    Id INT PRIMARY KEY IDENTITY(1,1),
//    OrderNumber NVARCHAR(50) NOT NULL,
//    OrderDate DATETIME NOT NULL,
//    DeliveryDate DATETIME NULL,
//    PickupPoint NVARCHAR(500) NOT NULL,
//    ClientName NVARCHAR(200) NOT NULL,
//    Status NVARCHAR(50) NOT NULL DEFAULT 'Новый'
//);

//--Добавляем тестовые данные(пользователи)
//INSERT INTO Users (Login, Password, FullName, Role) VALUES 
//('admin', '123', 'Главный админ', 'Admin'),
//('client', '123', 'Иванов Иван', 'Client');

//--Добавляем товары
//INSERT INTO Products (Article, Name, Price, Category, Quantity, ImagePath) VALUES 
//('A112T4', 'Ботинки женские', 4990, 'Женская обувь', 6, '1.jpg'),
//('F635R4', 'Ботинки Marco', 4990, 'Женская обувь', 13, '2.jpg'),
//('H782T5', 'Туфли мужские', 4499, 'Мужская обувь', 5, '3.jpg');

//--Добавляем тестовые заказы
//INSERT INTO Orders (OrderNumber, OrderDate, DeliveryDate, PickupPoint, ClientName, Status) VALUES 
//('ORD001', GETDATE(), DATEADD(day, 7, GETDATE()), 'г. Москва, ул. Тверская, 1', 'Иванов Иван', 'Новый'),
//('ORD002', GETDATE(), DATEADD(day, 3, GETDATE()), 'г. Москва, ул. Тверская, 1', 'Главный админ', 'Завершен');