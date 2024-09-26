using FluentValidation;
using MediatR;

namespace BTG.Application.Behaviors
{
    /**
     * Recibe dos tipos genéricos, TRequest y TResponse, que son los tipos de datos que se van a validar.
     * Implementa la interfaz IPipelineBehavior<TRequest, TResponse> que es la interfaz que se utiliza para crear un comportamiento personalizado para un pipeline de MediatR.
     */
    public class ValidationsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        // Se crea un campo privado de tipo IEnumerable<IValidator<TRequest>> que se va a utilizar para almacenar los validadores que se van a utilizar.
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationsBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /**
         * Se implementa el método Handle que recibe el request, el siguiente manejador y el token de cancelación.
         * Se valida si hay validadores, si los hay se crea un contexto de validación y se ejecutan las validaciones.
         * Se obtienen los errores de validación y se lanzan en una excepción de tipo ValidationException.
         */
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any()) 
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count > 0)
                {
                    throw new Exceptions.ValidationException(failures);
                }  
            }
            return await next();
        }
    }
}
