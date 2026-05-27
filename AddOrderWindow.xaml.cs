using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DEMO1.Data;
using DEMO1.Models;

namespace DEMO1
{
    /// <summary>
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        AppDbContext db = new AppDbContext();

        public AddOrderWindow()
        {
            InitializeComponent();
            OrderDatePicker.SelectedDate = DateTime.Today;
            LoadPickupPoints();
            LoadClients();
        }

        private void LoadPickupPoints()
        {
            var points = db.Orders.Select(o => o.PickupPoint).Distinct().ToList();
            foreach (var point in points)
                PickupPointBox.Items.Add(point);
        }

        private void LoadClients()
        {
            var clients = db.Users.Select(u => u.FullName).Distinct().ToList();
            foreach (var client in clients)
                ClientNameBox.Items.Add(client);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order newOrder = new Order
                {
                    OrderNumber = OrderNumberBox.Text,
                    OrderDate = OrderDatePicker.SelectedDate ?? DateTime.Today,
                    DeliveryDate = DeliveryDatePicker.SelectedDate,
                    PickupPoint = PickupPointBox.Text,
                    ClientName = ClientNameBox.Text,
                    Status = (StatusBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Новый"
                };

                db.Orders.Add(newOrder);
                db.SaveChanges();

                MessageBox.Show("Заказ добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
