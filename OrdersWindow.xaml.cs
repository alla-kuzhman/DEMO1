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
using DEMO1.Data;
using DEMO1.Models;
using System.Windows.Shapes;

namespace DEMO1
{
    /// <summary>
    /// Логика взаимодействия для OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        AppDbContext db = new AppDbContext();
        public OrdersWindow()
        {
            InitializeComponent();
            LoadOrders();
            InfoText.Text = $"Всего заказов: {db.Orders.Count()}";
        }
        private void LoadOrders()
        {
            OrdersGrid.ItemsSource = db.Orders.ToList();
            InfoText.Text = $"Всего заказов: {db.Orders.Count()}";
        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadOrders();
            MessageBox.Show("Список заказов обновлен", "Обновление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EditStatusButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = OrdersGrid.SelectedItem as Order;
            if (selected == null)
            {
                MessageBox.Show("Выберите заказ для изменения статуса", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            EditStatusWindow statusWindow = new EditStatusWindow(selected);
            statusWindow.ShowDialog();

            db.SaveChanges();
            LoadOrders();

        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrderWindow addOrderWindow = new AddOrderWindow();
            addOrderWindow.ShowDialog();
            LoadOrders();
        }

        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = OrdersGrid.SelectedItem as Order;
            if (selected == null)
            {
                MessageBox.Show("Выберите заказ для удаления", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show($"Удалить заказ №{selected.OrderNumber}?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                db.Orders.Remove(selected);
                db.SaveChanges();
                LoadOrders();
                MessageBox.Show("Заказ удален", "Готово",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public class StatusColorConverter : System.Windows.Data.IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                string status = value as string;
                if (status == "Новый")
                    return System.Windows.Media.Brushes.Red;
                if (status == "Завершен")
                    return System.Windows.Media.Brushes.Green;
                return System.Windows.Media.Brushes.Black;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}