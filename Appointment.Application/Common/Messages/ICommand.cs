using ErrorOr;
using MediatR;

namespace Appointment.Application.Common.Messages
{
    internal interface ICommand<out TResponse> : IRequest<TResponse>
        where TResponse : IErrorOr
    {
    }
}