using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
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
        string str;
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
                        temp[cName].Add(bite[0]);
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

            StringBuilder sb = new StringBuilder();

            //string file = @"C:\Users\1314CK\Desktop\ITSS Redacted - 6973GH-19-D-00031 ITSS_Redacted.pdf";
            string file = str;
            using (PdfReader reader = new PdfReader(file))

            {

                for (int pageNo = 1; pageNo <= reader.NumberOfPages; pageNo++)

                {

                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();

                    string text = PdfTextExtractor.GetTextFromPage(reader, pageNo, strategy);

                    text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));

                    sb.Append(text);

                }

            }

            //missingTB.Text = sb.ToString();
            string s = sb.ToString();

            string selected = contractCombo.SelectedItem.ToString();


            foreach (var item in temp[selected])
            {
                if (s.Contains(item) == false)
                {
                    missingTB.Text += item + "\n";
                }
                //foreach (var items in con)
                //{
                //    if (items.Contains(item)==false)
                //    {
                //        missingTB.Text += item + "\n";
                //    }
                //}
            }
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {

            missingTB.Clear();
        }


        private void upload_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".pdf"; // Default file extension
            dialog.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dialog.FileName;
                str = filename;
            }
        }
    }
}
