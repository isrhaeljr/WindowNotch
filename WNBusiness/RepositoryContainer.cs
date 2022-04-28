using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WNBusiness.Repositories;
namespace System
{
    public static class RepositoryContainer
    {
        public static ConfigRepository GetConfigRepository()
        {
            return new ConfigRepository();
        }
    }
}
