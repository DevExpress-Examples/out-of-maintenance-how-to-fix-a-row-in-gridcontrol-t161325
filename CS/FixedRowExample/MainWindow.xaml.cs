using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Hierarchy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FixedRowExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void barButton1_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            var item = sender as BarButtonItem;
            var context = item.DataContext as GridCellMenuInfo;
            var rowHandle = context.Row.RowHandle.Value;
            fixedRowBehavior.CreateFixedRow(rowHandle);
        }
    }
}
