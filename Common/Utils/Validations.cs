namespace Common.Utils
{
    public static class Validations
    {
        public static bool ValidateEmail(string email)
        {
            try
            {
                if (email.Contains(" ") || email.Contains("\""))
                {
                    return false;
                }
                var mail = new System.Net.Mail.MailAddress(email);
                return mail.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidateStringLength(string input, int length)
        {
            if(input != null)
                return input.Length >= length;
            return true;
        }
    }
}
