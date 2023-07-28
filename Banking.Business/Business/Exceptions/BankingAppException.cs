using System.Globalization;

namespace BankingAPI.Business.Exceptions
{
    public class BankingAppException : Exception
    {
        public BankingAppException() : base() { }

        public BankingAppException(string message) : base(message)
        {
        }

        public BankingAppException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
    }
}
