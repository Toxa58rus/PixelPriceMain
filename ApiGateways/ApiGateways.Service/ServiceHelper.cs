using Common.Extensions;
using Common.Models;
using Common.Rcp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateways.Service
{
    static public class ServiceHelper
    {
        static public async Task<T> SendCommandToServer<T>(this RpcClient _rpcClient, CommandResponse command)
        {
            var response = await _rpcClient.CallAsync(command.ToJson(), CancellationToken.None);
            _rpcClient.Dispose();

            return response.DeserializeToObject<T>();
        }
    }
}
