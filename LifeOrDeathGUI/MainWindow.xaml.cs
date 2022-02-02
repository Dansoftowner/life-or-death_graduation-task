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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LifeOrDeathGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            for (int i = 5; i <= 20; i += 5)
            {
                RowCombo.Items.Add(i);
                ColumnCombo.Items.Add(i);
            }
        }
    }
}
