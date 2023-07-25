using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Appointment.Application.Common.Interfaces.Persistence;
using Appointment.Application.Common.Messages;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

namespace Appointment.Application.Common.Behaviors
{
    internal sealed class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> where TResponse : IErrorOr
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (request is IQuery<TResponse>)
            {
                return await next();
            }

            await using IDbContextTransaction transaction = await _unitOfWork.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);

            try
            {
                var response = await next();
            
                await transaction.CommitAsync(cancellationToken);

                return response;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }
        }
    }
}