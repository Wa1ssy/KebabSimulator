using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Kebab_Simulator.TestingxUnit.Mock
{
    public class MockIHostEnvironment : IHostingEnvironment
    {
        public string EnvironmentName {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public string ApplicationName {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public string ContentRootPath {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public string EnvironmentName {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

    }
}
