using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.AccessCache;
using Newtonsoft.Json;
using DataAccessLibrary.Models;
using System.Collections.ObjectModel;
using CsvHelper;
using CsvParse;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Linq;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();

        }

        StorageFile storageFile;
        StorageFolder storageFolder;
        public static List<Customer>customers = new List<Customer>();

        public class Customer
        {
            public Customer ()
            {

            }

            public Customer(string firstName, string lastName, string email)
            {
               
                FirstName = firstName;
                LastName = lastName;
                Email = email;
            }

            [JsonProperty(propertyName: "id")]
            public int Id { get; set; }

            [JsonProperty(propertyName: "firstName")]
            public string FirstName { get; set; }

            [JsonProperty(propertyName: "lastName")]
            public string LastName { get; set; }

            [JsonProperty(propertyName: "email")]
            public string Email { get; set; }

            public string DisplayName => $"{FirstName} {LastName}";
        }

        public class hej
        {
            public hej()
            {

            }
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string DisplayName => $"{FirstName} {LastName}";
        }


        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.FileTypeFilter.Add(".json");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            string text = await Windows.Storage.FileIO.ReadTextAsync(file);
            List<Customer> DeserializedProducts = JsonConvert.DeserializeObject<List<Customer>>(text);
            ListView.ItemsSource = DeserializedProducts;
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.FileTypeFilter.Add(".csv");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            customers.Clear();
            if (file !=null)
            {
                IList<string> lines = await FileIO.ReadLinesAsync(file);
                lines = lines.Skip(1).ToList() ;
                foreach (var line in lines)
                {
                    var data = line.Split(';');
                    customers.Add(new Customer(data[1], data[2], data[3]));
                }
            }
            ListView.ItemsSource = customers;
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.FileTypeFilter.Add(".xml");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            var stream = await file.OpenStreamForReadAsync();
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);

            foreach (XmlNode node in doc.DocumentElement)
            {
                CsvRowsListView.Items.Add(node.InnerText);
                foreach (XmlNode child in node.ChildNodes)
                {

                }
            }


        }

        private async void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.FileTypeFilter.Add(".xml");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);

            XmlSerializer serializer = new XmlSerializer(typeof(List<personer>));
            StreamReader reader = new StreamReader(stream.AsStream());

            var input = serializer.Deserialize(reader);
            ListView.ItemsSource = input;

        }
    }
}
