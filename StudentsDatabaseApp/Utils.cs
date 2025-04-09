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

    }
}
