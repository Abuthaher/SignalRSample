using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleNotifiaction.Repository
{
    public interface IDatabaseSubscription
    {
        void Configure(string connectionString);
    }
}
