using Microsoft.Win32;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentsDatabaseApp
{

    public class Student
    {
        public string PESEL { get; set; }
        public string Name { get; set; }
        public string SecName { get; set; }
        public string Surname { get; set; }
        public string Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string HomeAdress { get; set; }
        public string City { get; set; }
        public string PCode { get; set; }

        public Student(string PESEL, string name, string secName, string surname, string birthday, string phoneNumber, string homeAdress, string city, string pCode)
        {
            this.PESEL = PESEL;
            Name = name;
            SecName = secName;
            Surname = surname;
            Birthday = birthday;
            PhoneNumber = phoneNumber;
            HomeAdress = homeAdress;
            City = city;
            PCode = pCode;
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void RemoveSelected_Click(object sender, RoutedEventArgs e)
        {
           while(mainList.SelectedItems.Count > 0)
            {
                mainList.Items.Remove(mainList.SelectedItems[0]);
            }
        }

        private void Add_User_Click(object sender, RoutedEventArgs e)
        {
            new AddUserWindow(this).ShowDialog();
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pliki CSV z separatorem (,) |*.csv|Pliki CSV z separatorem (;) |*.csv";
            saveFileDialog.Title = "Zapisz jako plik CSV";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                string delimiter = ";";
                if (saveFileDialog.FilterIndex == 1)
                {
                    delimiter = ",";
                }
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (Student item in mainList.Items)
                    {
                        StringBuilder builder = new();
                        var fields = item.GetType().GetRuntimeFields();
                        var lastField = fields.Last();
                        foreach (var field in fields)
                        {
                            Console.WriteLine(field.GetValue(item));
                            builder.Append(field.GetValue(item).ToString()).Append(field.Equals(lastField) ? "" : delimiter);
                        }
                        
                        writer.WriteLine(builder.ToString());
                    }
                }
            }
        }

        private void AboutProgram_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadFile_CLick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pliki CSV z separatorem (,) |*.csv|Pliki CSV z separatorem (;) |*.csv";
            openFileDialog.Title = "Otwórz plik CSV";
            if (openFileDialog.ShowDialog() == true)
            {
                mainList.Items.Clear();
                string filePath = openFileDialog.FileName;
                int selectedFilterIndex = openFileDialog.FilterIndex;
                string delimiter = ";";
                if (selectedFilterIndex == 1)
                {
                    delimiter = ",";
                }
                Encoding encoding = Encoding.UTF8;
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath, encoding);
                    foreach (var line in lines)
                    {
                        string[] columns = line.Split(delimiter);
                        if (columns != null)
                        {
                            Student student = new(Utils.strConv(columns.ElementAtOrDefault(0)), //PESEL
                                Utils.strConv(columns.ElementAtOrDefault(1)), //name
                                Utils.strConv(columns.ElementAtOrDefault(2)), //secName
                                Utils.strConv(columns.ElementAtOrDefault(3)), //surname
                                Utils.strConv(columns.ElementAtOrDefault(4)), //birthday
                                Utils.strConv(columns.ElementAtOrDefault(5)), //phoneNumber
                                Utils.strConv(columns.ElementAtOrDefault(6)), //homeAdress
                                Utils.strConv(columns.ElementAtOrDefault(7)), //city
                                Utils.strConv(columns.ElementAtOrDefault(8))); //postalCode
                            mainList.Items.Add(student);
                        }
                    }
                }
            }

        }
    }
}