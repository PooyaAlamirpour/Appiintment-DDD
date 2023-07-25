using ErrorOr;
using MediatR;

namespace Charisma.Application.Common.Messages
{
    internal interface IQuery<out TResponse> : IRequest<TResponse> 
        where TResponse : IErrorOr
    {
    }
}