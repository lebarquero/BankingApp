using System.Globalization;

namespace BankingAPI.Infrastructure
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
