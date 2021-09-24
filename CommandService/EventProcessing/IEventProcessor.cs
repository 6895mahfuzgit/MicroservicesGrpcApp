using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandService.EventProcessing
{
    public interface IEventProcessor
    {
        void Process(string message);
    }
}
