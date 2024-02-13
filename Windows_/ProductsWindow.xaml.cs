using Cosmetic3121.Classes_;
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

namespace Cosmetic3121.Windows_
{
    /// <summary>
    /// Interaction logic for ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        Entities1 context = new Entities1();
        List<Products> prodList;
        int count = 0, count2=0;
        public ProductsWindow()
        {
            InitializeComponent();
            prodList = context.Products.ToList();
            count = prodList.Count;
            LV.ItemsSource = prodList;
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            var CurrentSP = (sender as StackPanel).DataContext as Classes_.Products;
            if (CurrentSP.Discount >= 15) (sender as StackPanel).Background = Brushes.LightGreen;
            decimal price = CurrentSP.Price;
            int disc = CurrentSP.Discount;
            decimal newPrice = price - ((price * disc) / 100);
            foreach (var item in (sender as StackPanel).Children.OfType<TextBlock>())
            {
                if (item.Name == "txt_price") item.Text = newPrice.ToString() + " руб.";
            }
        }

        private void cmb_sort_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void cmb_filtr_DropDownClosed(object sender, EventArgs e)
        {
            SortAndFiltr();
        }
        private void SortAndFiltr()
        {
            prodList = context.Products.ToList();
            if (cmb_sort.SelectedIndex== 0) prodList = prodList.OrderBy(x => x.Price).ToList();
            if (cmb_sort.SelectedIndex == 1) prodList = prodList.OrderByDescending(x => x.Price).ToList();
            if (cmb_sort.SelectedIndex == 1) prodList = prodList.Where(x => x.Discount <10).ToList();
            if (cmb_sort.SelectedIndex == 2) prodList = prodList.Where(x => x.Discount >=10 && x.Discount<15).ToList();
            if (cmb_sort.SelectedIndex == 3) prodList = prodList.Where(x => x.Discount >=15).ToList();
            LV.ItemsSource= null;
            LV.ItemsSource= prodList;
            count2=prodList.Count();
            txt_qty.Text = count2 + " из " + count;
        }
    }
}
