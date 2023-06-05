using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class EntrepriseDatabaseSettings
    {
        public const string SectionName = "EntrepriseDatabaseSettings";
        public string ConnectionString { get; set; } = null!;

        //public string UsersCollectionName { get; set; } = null!;
    }
}
