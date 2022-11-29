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

        //private List<string> clausesNo = new List<string>();
        public List<string> docType = new List<string>() { "Solicitaion", "Contract" };
        public Dictionary<string, List<string>> temp1 = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> temp2 = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> notReq1 = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> notReq2 = new Dictionary<string, List<string>>();
        string str;
        public MainWindow()
        {
            //this.clausesNo = new List<string>();
            this.notReq1 = new Dictionary<string, List<string>>();
            this.notReq2 = new Dictionary<string, List<string>>();
            this.temp1 = new Dictionary<string, List<string>>();
            this.temp2 = new Dictionary<string, List<string>>();
            InitializeComponent();

            string[] contractLine = File.ReadAllLines("clause_matrix_updated.csv");

            string[] henceForth = contractLine[0].Split(",");
            for (int i = 2; i < henceForth.Length; i++)
            {
                string cName = henceForth[i];
                temp2.Add(cName, new List<string>());
                temp1.Add(cName, new List<string>());
                notReq1.Add(cName, new List<string>());
                notReq2.Add(cName, new List<string>());

                for (int j = 1; j < contractLine.Length; j++)
                {
                    string[] bite = contractLine[j].Split(",");

                    if (bite[i] == "R" && bite[1] == "C")
                    {
                        temp1[cName].Add(bite[0]);
                    }

                    if (bite[i] == "R")
                    {
                        temp2[cName].Add(bite[0]);
                    }

                    if (bite[1] == "C")
                    {
                        if (bite[i] == "A" || bite[i] == "O")
                        {
                            notReq1[cName].Add(bite[0]);
                        }
                    }
                    
                    if (bite[i] == "A" || bite[i] == "O")
                    {
                        notReq2[cName].Add(bite[0]);
                    }

                }
            }
            

            this.contractCombo.ItemsSource = temp1.Keys;
            this.typeCombo.ItemsSource = docType;
        }

        private void getButton_Click(object sender, RoutedEventArgs e)
        {
            missingTB.Clear(); 
            //missingNotReqTB.Clear();
            string type = typeCombo.SelectedItem.ToString();

            //Required Clauses depending on type of document
            if (type == "Contract")
            {
                foreach (var item in temp1.Keys)
                {
                    if (item == contractCombo.SelectedItem.ToString())
                    {
                        clausesLB.ItemsSource = temp1[item];
                    }
                }

                foreach (var item in notReq1.Keys)
                {
                    if (item == contractCombo.SelectedItem.ToString())
                    {
                        notReqLB.ItemsSource = notReq1[item];
                    }
                }
            }
            else
            {
                foreach (var item in temp2.Keys)
                {
                    if (item == contractCombo.SelectedItem.ToString())
                    {
                        clausesLB.ItemsSource = temp2[item];
                    }
                }

                foreach (var item in notReq2.Keys)
                {
                    if (item == contractCombo.SelectedItem.ToString())
                    {
                        notReqLB.ItemsSource = notReq2[item];
                    }
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

            string s = sb.ToString();

            string selected = contractCombo.SelectedItem.ToString();

            if (type == "Contract")
            {
                foreach (var item in temp1[selected])
                {
                    if (s.Contains(item) == false)
                    {
                        missingTB.Text += item + "\n";
                    }
                }

                foreach (var item in notReq1[selected])
                {
                    if (s.Contains(item) == false)
                    {
                        missingNotReqTB.Text += item + "\n";
                    }
                }
            }
            else
            {
                foreach (var item in temp2[selected])
                {
                    if (s.Contains(item) == false)
                    {
                        missingTB.Text += item + "\n";
                    }
                }

                foreach (var item in notReq2[selected])
                {
                    if (s.Contains(item) == false)
                    {
                        missingNotReqTB.Text += item + "\n";
                    }
                }
            }
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
