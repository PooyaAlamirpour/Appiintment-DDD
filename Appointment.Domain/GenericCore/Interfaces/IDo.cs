using System.Threading.Tasks;

namespace Appointment.Domain.GenericCore.Interfaces
{
    public interface IDo<in TArg, TKey>
    {
        Task<TKey> Do(TArg arg);
    }
}