using Microsoft.Win32;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudentsDatabaseApp
{
    /// <summary>
    /// Logika interakcji dla klasy AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {

        private List<TextBox> requiredInputs;
        private MainWindow parent;

        public Brush INCORRECT_COLOR = Brushes.Red;
        public Brush CORRECT_COLOR = Brushes.Transparent;


        public AddUserWindow(MainWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.requiredInputs = new List<TextBox> { PeselInput, NameInput, SurnameInput, HomeAdressInput, CityInput, PosCodeInput };
        }

        private bool allFieldsEmpty()
        {
            foreach (TextBox input in this.requiredInputs)
            {
                if (!Utils.strEmpty(input.Text))
                { 
                    return false;
                }
            }
            if (!Utils.strEmpty(BirthdayInput.Text) || !Utils.strEmpty(SecNameInput.Text) || !Utils.strEmpty(PhoneNumberInput.Text))
                return false;

            return true;
        }

        

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            bool correct = true;
            foreach(TextBox input in this.requiredInputs)
            {
                if (Utils.strEmpty(input.Text))
                {
                    input.Background = INCORRECT_COLOR;
                    correct = false;
                }
                else
                {
                    input.Background = CORRECT_COLOR;
                }
            }
            //DRY :D
            if (Utils.strEmpty(BirthdayInput.Text))
            {
                BirthdayInput.Background = INCORRECT_COLOR; ;
                correct = false;
            }
            else
            {
                BirthdayInput.Background = CORRECT_COLOR;
            }

            if (!correct) return;

            string pesel = PeselInput.Text;
            string name = NameInput.Text;
            string secName = SecNameInput.Text;
            string surname = SurnameInput.Text;
            string birthday = BirthdayInput.Text;
            string phoneNumber = PhoneNumberInput.Text;
            string homeAdress = HomeAdressInput.Text;
            string city = CityInput.Text;
            string posCode = PosCodeInput.Text;

            DateOnly? birthdayDate = Utils.getDateFromString(birthday, '.');

            if(birthdayDate == null)
            {
                BirthdayInput.Background = INCORRECT_COLOR;
                return;
            }

            if (!Utils.IsValidPESEL(pesel) || !Utils.checkDatePESEL((DateOnly) birthdayDate, pesel)) 
            {
                PeselInput.Background = INCORRECT_COLOR;
                return;
            }

            name = formatName(name);
            secName = formatName(secName);
            surname = formatName(surname);

            if (!Utils.strEmpty(phoneNumber))
            {
                phoneNumber = phoneNumber.Trim();
                phoneNumber = Regex.Replace(phoneNumber, "[^0-9]", "");
                Console.WriteLine(phoneNumber);

                if (!phoneNumber.StartsWith("+"))
                {
                    phoneNumber = "+48" + phoneNumber;
                }
            }

            parent.mainList.Items.Add(new Student(pesel, name, secName, surname, birthday, phoneNumber, homeAdress, city, posCode));
            this.Close();
        }

        private String formatName(String text)
        {
            if (text.Length <= 0) return text;
            text = text.ToLower();
            text = setLetterUp(text);
            string[] partsName = text.Split("-");

            if (partsName.Length > 1)
            {
                StringBuilder builder = new StringBuilder(partsName[0]);

                for (int i = 1; i < partsName.Length; i++)
                {
                    string part = partsName[i];
                    builder.Append("-").Append(setLetterUp(part));
                }


                return builder.ToString();
            }
            else
            {
                return text;
            }

        }

        private string setLetterUp(String text)
        {
            return text[0].ToString().ToUpper() + text.Substring(1);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (!allFieldsEmpty())
            {
                MessageBoxResult result = MessageBox.Show("Jesteś pewny by wyjść?", "Are you sure", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if(result == MessageBoxResult.Yes)
                {
                    Close();
                }

            }
            else
            {
                Close();
            }
        }
    }
}
