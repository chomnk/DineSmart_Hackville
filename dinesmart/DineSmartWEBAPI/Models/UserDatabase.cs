using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DineSmartWEBAPI.Models
{
    public class UserDatabase
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UserCollectionName { get; set; } = null!;
    }
}