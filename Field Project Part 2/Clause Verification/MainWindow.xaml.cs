using System;
using System.Collections.Generic;
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

namespace Clause_Verification
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<string> clausesNo = new List<string>();
        public Dictionary<string, string> contract = new Dictionary<string, string>();
        public MainWindow()
        {
            this.clausesNo = new List<string>();
            this.contract = new Dictionary<string, string>();
            InitializeComponent();



            var clauseLine = File.ReadAllLines("clause_matrix.csv").Skip(1);
            string[] contractLine = File.ReadAllLines("clause_matrix.csv");

            foreach (var line in clauseLine)
            {
                string b = line;
                b = b.Substring(0, b.IndexOf(','));
                clausesNo.Add(b);
            }

            for (int i = 0; i < contractLine[0].Length; i++)
            {
                string code = contractLine[i];
                contract.Add(code,"");
            }
            this.contractCombo.ItemsSource = contract.Keys;
            this.clausesListBox.ItemsSource = clausesNo;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
