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
    /// Логика взаимодействия для EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        AppDbContext db = new AppDbContext();
        private Product _product;
        public EditProductWindow(Product product)
        {
            InitializeComponent();
            _product = product;
            LoadCategories();
            LoadProductData();
        }
        private void LoadCategories()
        {
            var categories = db.Products.Select(p=> p.Category).Distinct().ToList();
            CategoryBox.Items.Clear();
            foreach (var cat in categories)
                CategoryBox.Items.Add(cat);
        }
        private void LoadProductData()
        {
            ArticleBox.Text = _product.Article;
            NameBox.Text=_product.Name;
            PriceBox.Text=_product.Price.ToString();
            CategoryBox.Text=_product.Category;
            QuantityBox.Text=_product.Quantity.ToString();
            ImagePathBox.Text=_product.ImagePath;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _product.Article = ArticleBox.Text;
                _product.Name = NameBox.Text;
                _product.Price = Convert.ToInt32(PriceBox.Text);
                _product.Category = CategoryBox.Text;
                _product.Quantity = Convert.ToInt32(QuantityBox.Text);
                _product.ImagePath = ImagePathBox.Text;

                db.Entry(_product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                MessageBox.Show("Товар обовлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
            this.DialogResult = false;
            this.Close();
        }
    }
}
