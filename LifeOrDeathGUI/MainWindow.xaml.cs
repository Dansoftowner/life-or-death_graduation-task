using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        private static readonly int MinSize = 5;
        private static readonly int MaxSize = 20;
        private static readonly int MinDistance = 1;

        private int RowsCount => (int)RowCombo.SelectedValue;
        private int ColumnsCount => (int)ColumnCombo.SelectedValue;

        public MainWindow()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            InitializeCombos();
        }

        private void InitializeCombos()
        {
            for (int i = MinSize; i <= MaxSize; i += MinDistance)
            {
                RowCombo.Items.Add(i);
                ColumnCombo.Items.Add(i);
            }
            RowCombo.SelectedValue = MaxSize;
            ColumnCombo.SelectedValue = MaxSize;
        }

        private void NewMatrix_Click(object sender, EventArgs args)
        {
            CheckBoxGrid.Children.Clear();
            for (int row = 0; row < RowsCount; row++)
            {
                for (int col = 0; col < ColumnsCount; col++)
                {
                    CheckBox chb = new CheckBox();
                    chb.SetValue(Grid.RowProperty, row);
                    chb.SetValue(Grid.ColumnProperty, col);
                    CheckBoxGrid.Children.Add(chb);

                    var cd = new ColumnDefinition();
                    cd.Width = GridLength.Auto;
                    CheckBoxGrid.ColumnDefinitions.Add(cd);
                }
                var rd = new RowDefinition();
                rd.Height = GridLength.Auto;
                CheckBoxGrid.RowDefinitions.Add(rd);
            }
        }

        private void Save_Click(object sender, EventArgs args)
        {
            string outputFile = $"Eletjatek_{RowsCount}x{ColumnsCount}.txt";

            using(StreamWriter sw = new StreamWriter(outputFile))
            {

                for (int i = 0; i<RowsCount; i++)
                {
                    var checksInRow = CheckBoxGrid.Children
                        .Cast<CheckBox>()
                        .Where(it => Grid.GetRow(it) == i)
                        .OrderBy(it => Grid.GetColumn(it));
                    foreach (var checkBox in checksInRow) 
                        sw.Write(checkBox.IsChecked.GetValueOrDefault(false) ? "1" : "0");
                    sw.WriteLine();
                }
            }
        }
    }
}
