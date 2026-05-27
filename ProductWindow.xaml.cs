using DEMO1.Data;
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
using Microsoft.EntityFrameworkCore;

namespace DEMO1
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        AppDbContext db = new AppDbContext();
        private string _userRole;
        public ProductWindow(string role)
        {
            InitializeComponent();
            _userRole = role;

            if (_userRole == "Guest")
            {
                FilterPanel.Visibility = Visibility.Collapsed;
                AdminPanel.Visibility = Visibility.Collapsed;
                RoleInfo.Text = "Вы вошли как ГОСТЬ (только просмотр)";
            }
            else if (_userRole == "Client")
            {
                FilterPanel.Visibility = Visibility.Visible;
                AdminPanel.Visibility = Visibility.Collapsed;
                RoleInfo.Text = "Вы вошли как КЛИЕНТ";
            }
            else if ( _userRole == "Admin" || _userRole == "Manager")
            {
                FilterPanel.Visibility = Visibility.Visible;
                AdminPanel.Visibility = Visibility.Visible;
                RoleInfo.Text = "Вы вошли как АДМИН (полный доступ)";
            }
            LoadProducts();
            LoadCategories();
        }
        private void LoadProducts()
        {
            var products = db.Products.ToList();
            ProductsGrid.ItemsSource = products;

            ProductsGrid.LoadingRow += (sender, e) =>
            {
                var product = e.Row.DataContext as Product;
                if (product != null && product.Discount > 17)
                {
                    e.Row.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDEAD"));
                }
            };
        }
        private void LoadCategories()
        {
            var categories = db.Products.Select(p => p.Category).Distinct().ToList();
            categories.Insert(0, "Все категории");
            CategoryFilter.ItemsSource= categories;
            CategoryFilter.SelectedIndex = 0;
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string search = SearchBox.Text.ToLower();
            string selectedCategory=CategoryFilter.SelectedItem?.ToString();

            var query = db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(search)) 
                query=query.Where(p=>p.Name.ToLower().Contains(search) || p.Article.ToLower().Contains(search));
            if (selectedCategory != "Все категории" && !string.IsNullOrEmpty(selectedCategory))
                    query=query.Where(p=>p.Category == selectedCategory);
            ProductsGrid.ItemsSource = query.ToList();
        }

        private void ResetFilterButton_Click(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "";
            CategoryFilter.SelectedIndex = 0;
            LoadProducts();
        }
        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = ProductsGrid.SelectedItem as Product;
            if(selected == null)
            {
                MessageBox.Show("Выберите товар для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MessageBox.Show($"Удалить товар '{selected.Name}'?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                db.Products.Remove(selected);
                db.SaveChanges();
                LoadProducts();
                MessageBox.Show("Товар удален", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EditProductButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = ProductsGrid.SelectedItem as Product;
            if(selected == null)
            {
                MessageBox.Show("Выберите товар для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            EditProductWindow editWindow = new EditProductWindow(selected);
            editWindow.ShowDialog();

            db.SaveChanges();
            LoadProducts();
        }
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow addWindow = new AddProductWindow();
            addWindow.ShowDialog();

            LoadProducts();
        }

        private void ManageProductButton_Click(object sender, RoutedEventArgs e)
        {
            OrdersWindow orderWindow = new OrdersWindow();
            orderWindow.Show();

        }
    }
}
