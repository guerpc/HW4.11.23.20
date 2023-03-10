using HW2DLL;
using HW4._11._23._20;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
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

namespace HW4._11._23._20
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //intiialziing json variables as well as publisher class and book class
        DataContractJsonSerializer inputSerializer;
        Publisher p1;
        Book b1;

        private BookPublishingEntities dbBookEntities = new BookPublishingEntities();
        public MainWindow()
        {
            InitializeComponent();
            p1 = new Publisher();
            b1 = new Book();
            
            // DataContext = p1;
            //DataContext = dbBookEntities.Books.Local;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //clears the data in the current Books Database
            dbBookEntities.Books.RemoveRange(dbBookEntities.Books);

            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = ""; // Default file name
            dlg.DefaultExt = ""; // Default file extension
            dlg.Filter = "JSON files (.json)|*.json"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                FileStream pReader = new FileStream(filename, FileMode.Open, FileAccess.Read);
                inputSerializer = new DataContractJsonSerializer(typeof(Publisher));
                p1 = (Publisher)inputSerializer.ReadObject(pReader);
                pReader.Dispose();

            }

            foreach (HW2DLL.Book b in p1.BookList)
            {
               // int count = 0;
              //  count++;
                HW4._11._23._20.Book bookdata = new HW4._11._23._20.Book();
               // bookdata.Id = count;
                bookdata.Price = b.Price;
                bookdata.Title = b.Title;
                dbBookEntities.Books.Add(bookdata);
                dbBookEntities.SaveChanges();
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            dbBookEntities.Books.Load();

            DataContext = dbBookEntities.Books.Local;


        }
    }
}
