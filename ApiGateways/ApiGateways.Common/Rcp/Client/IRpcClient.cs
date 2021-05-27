using System.Threading;
using System.Threading.Tasks;

namespace Common.Rcp.Client
{
    public interface IRpcClient
    {
        Task<string> CallAsync(string message, CancellationToken cancellationToken);
    }
}
