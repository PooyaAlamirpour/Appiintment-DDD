using ErrorOr;
using MediatR;

namespace Appointment.Application.Common.Messages
{
    internal interface IQuery<out TResponse> : IRequest<TResponse> 
        where TResponse : IErrorOr
    {
    }
}