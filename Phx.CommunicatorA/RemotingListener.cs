using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;

namespace Phx.Communicator
{
    class RemotingListener : ICommunicationListener
    {
        public async Task<string> OpenAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task CloseAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Abort()
        {
            throw new NotImplementedException();
        }
    }
}
