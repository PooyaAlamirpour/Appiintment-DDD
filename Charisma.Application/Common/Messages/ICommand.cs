using ErrorOr;
using MediatR;

namespace Charisma.Application.Common.Messages
{
    internal interface ICommand<out TResponse> : IRequest<TResponse>
        where TResponse : IErrorOr
    {
    }
}