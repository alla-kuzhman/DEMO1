using DEMO1.Data;
using DEMO1.Models;
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

namespace DEMO1
{
    /// <summary>
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        AppDbContext db = new AppDbContext();
        public AddProductWindow()
        {
            InitializeComponent();
            LoadCategories();
        }
        private void LoadCategories()
        {
            var categories = db.Products.Select(p => p.Category).Distinct().ToList();
            CategoryBox.Items.Clear();
            foreach (var cat in categories)
                CategoryBox.Items.Add(cat);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product newProduct = new Product
                {
                    Article = ArticleBox.Text,
                    Name = NameBox.Text,
                    Price = Convert.ToInt32(PriceBox.Text),
                    Category = CategoryBox.Text,
                    Quantity = Convert.ToInt32(QuantityBox.Text),
                    ImagePath = ImagePathBox.Text,
                };
                db.Products.Add(newProduct);
                db.SaveChanges();

                MessageBox.Show("Товар добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult=false;
            this.Close();
        }
    }
}
