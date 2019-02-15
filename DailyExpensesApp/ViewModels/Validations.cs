using System;
using System.Collections.Generic;
using System.Text;

namespace DailyExpensesApp
{
   public class Validations
    {
       public  bool ValidatePassword(string password)
        {
            const int MIN_LENGTH = 6;
            const int MAX_LENGTH = 15;

            if (password == null) throw new ArgumentNullException();

            bool meetsLengthRequirements = password.Length >= MIN_LENGTH && password.Length <= MAX_LENGTH;
            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            bool hasDecimalDigit = false;


            if (meetsLengthRequirements)
            {
                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDecimalDigit = true;
                }
            }

            bool isValid = meetsLengthRequirements
                           && hasUpperCaseLetter
                           && hasLowerCaseLetter
                           && hasDecimalDigit
                ;
            return isValid;

        }


       public  bool ValidateEmail(string email)
        {
            try
            {
                if (email == null) throw new ArgumentNullException();
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
