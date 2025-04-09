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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddUserWindow(this).ShowDialog();
        }
    }
}