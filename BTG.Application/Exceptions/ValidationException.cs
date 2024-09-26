using FluentValidation.Results;

namespace BTG.Application.Exceptions
{
    public class ValidationException : Exception
    {
        /**
         * Se crea un constructor que inicializa la lista de errores.
         * Hereda de la clase base Exception y le pasa un mensaje por defecto.
         */
        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new List<string>();
        }
        public List<string> Errors { get; }

        /**
         * Se crea un constructor que recibe una lista de validaciones fallidas.
         * Recorre la lista de validaciones fallidas y agrega los mensajes de error a la lista de errores.
         */
        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
    }
}
