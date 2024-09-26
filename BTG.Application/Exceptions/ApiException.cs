using System.Globalization;

namespace BTG.Application.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException() : base()
        {

        }

        public ApiException(string message) : base(message)
        {

        }

        /**
         * Se crea un constructor que recibe un mensaje y un array de objetos.
         * tambien se formatea el mensaje con el string.Format para que se pueda pasar los argumentos.
         */
        public ApiException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
    }
}
