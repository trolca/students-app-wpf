using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDatabaseApp
{
    public class Utils
    {

        public static bool strEmpty(string str)
        {
            return str.Trim().Length == 0;
        }

        public static bool IsValidPESEL(string pesel)
        {
            if (pesel.Length != 11 || !long.TryParse(pesel, out _))
            {
                return false;
            }

            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int checksum = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                checksum += weights[i] * (pesel[i] - '0');
            }

            int controlDigit = 10 - (checksum % 10);
            if (controlDigit == 10)
            {
                controlDigit = 0;
            }

            return controlDigit == (pesel[10] - '0');
        }

        //yyyy(m)m(d)d
        public static DateOnly? getDateFromString(string dateString, char splitChar)
        {
            string[] dateData = dateString.Split(splitChar);
            if (dateData.Length != 3) return null;
            int year, month, day;

            day = int.Parse(dateData[0]);
            month = int.Parse(dateData[1]);
            year = int.Parse(dateData[2]);

            if (year < 1800 || year > 2299) return null;

            if (month < 1 || month > 12) return null;

            bool leapYear = (year % 4 == 0 && year % 100 != 0) || year % 400 == 0;

            if (month == 2)
            {
                if (leapYear && day > 29)
                {
                    return null;
                }
                else if (leapYear && day > 28)
                {
                    return null;
                }
            }
            else if (month <= 7)
            {
                if (month % 2 == 0 && day > 31) return null;
                else if (month % 2 != 0 && day > 30) return null;
            }
            else
            {
                if (month % 2 != 0 && day > 30) return null;
                else if (month % 2 == 0 && day > 31) return null;
            }


            return new DateOnly(year, month, day);
        }

        public static bool checkDatePESEL(DateOnly birthDate, string pesel)
        {
            string yearPslStr = pesel.Substring(0, 2);
            string monthPslStr = pesel.Substring(2, 2);
            string dayPslStr = pesel.Substring(4, 2);

            string yearFirstCheck = birthDate.Year.ToString().Substring(0, 2);
            string yearStrCheck = birthDate.Year.ToString().Substring(2);
            string dayStrCheck = birthDate.Day.ToString();
            dayStrCheck = dayStrCheck.Length == 1 ? "0" + dayStrCheck : dayStrCheck;
            int monthCheck = birthDate.Month;

            if (!yearStrCheck.Equals(yearPslStr))
            {
                return false;
            }
            int addYear = 0;
            if (yearFirstCheck.Equals("18")) addYear = 80;
            if (yearFirstCheck.Equals("19")) addYear = 0;
            if (yearFirstCheck.Equals("20")) addYear = 20;
            if (yearFirstCheck.Equals("21")) addYear = 40;
            if (yearFirstCheck.Equals("22")) addYear = 60;
            int monthIntPsl = int.Parse(monthPslStr);

            if (monthIntPsl != monthCheck + addYear)
            { 
                return false;
            }
            if (!dayPslStr.Equals(dayStrCheck))
            {
              
                return false;
            }

            return true;
        }

    }
}
