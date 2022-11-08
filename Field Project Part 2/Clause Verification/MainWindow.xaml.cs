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
        public Dictionary<string, List<string>> contract = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> temp = new Dictionary<string, List<string>>();
        public MainWindow()
        {
            this.clausesNo = new List<string>();
            this.contract = new Dictionary<string, List<string>>();
            this.temp = new Dictionary<string, List<string>>();
            InitializeComponent();


            //Getting clause number
            /*var clauseLine = File.ReadAllLines("clause_matrix.csv").Skip(1);

            foreach (var line in clauseLine)
            {
                string b = line;
                b = b.Substring(0, b.IndexOf(','));
                clausesNo.Add(b);
            }*/

            //Getting contract codes
            string[] contractLine = File.ReadAllLines("clause_matrix.csv");

            /*string code = contractLine[0];
            string[] piece = code.Split(",");
            piece = piece.Skip(1).ToArray();
            foreach (var item in piece)
            {
                contract.Add(item, clausesNo);
            }*/

            string[] henceForth = contractLine[0].Split(",");
            for (int i = 1; i < henceForth.Length; i++)
            {
                string cName = henceForth[i];
                temp.Add(cName, new List<string>());
                for (int j = 1; j < contractLine.Length; j++)
                {
                    string[] bite = contractLine[j].Split(",");

                    if (bite[i] == "R")
                    {
                        temp[cName].Add(bite[0] + "\n");
                    }

                }
            }

            this.contractCombo.ItemsSource = temp.Keys;
            //this.clausesListBox.ItemsSource = clausesNo;
        }

        private void getButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in temp.Keys)
            {
                if (item == contractCombo.SelectedItem.ToString())
                {
                    clausesLB.ItemsSource = temp[item];
                }
            }
        }
    }
}
