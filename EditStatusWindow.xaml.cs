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
using DEMO1.Models;

namespace DEMO1
{
    /// <summary>
    /// Логика взаимодействия для EditStatusWindow.xaml
    /// </summary>
    public partial class EditStatusWindow : Window
    {
        private Order _order;

        public EditStatusWindow(Order order)
        {
            InitializeComponent();
            _order = order;
            OrderNumberText.Text = order.OrderNumber;

            // Выбираем текущий статус в комбобоксе
            for (int i = 0; i < StatusComboBox.Items.Count; i++)
            {
                var item = StatusComboBox.Items[i] as ComboBoxItem;
                if (item != null && item.Content.ToString() == order.Status)
                {
                    StatusComboBox.SelectedIndex = i;
                    break;
                }
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = StatusComboBox.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                _order.Status = selectedItem.Content.ToString();
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
