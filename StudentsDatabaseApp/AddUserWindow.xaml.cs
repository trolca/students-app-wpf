using System;
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

        public AddUserWindow(MainWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.requiredInputs = new List<TextBox> { PeselInput, NameInput, SurnameInput, BirthdayInput, HomeAdressInput, CityInput, PosCodeInput };
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            bool correct = true;
            foreach(TextBox input in this.requiredInputs)
            {
                if (Utils.strEmpty(input.Text))
                {
                    incorrectStyle(input);
                }
                else
                {
                    correctStyle(input);
                }
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

            if (!Utils.IsValidPESEL(pesel))
            {
                incorrectStyle(PeselInput);
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

                if (!phoneNumber.StartsWith("+")) ;
            }




        }

        private String formatName(String text)
        {
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

        private void incorrectStyle(TextBox textBox)
        {
            textBox.Background = Brushes.Red;
        }

        private void correctStyle(TextBox textBox)
        {
            textBox.Background = Brushes.Transparent;
        }
    }
}
