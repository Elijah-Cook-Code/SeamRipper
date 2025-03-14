using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using SeamRipperData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeamRipperData.Connections
{
    public class ClientContextFactory : IDesignTimeDbContextFactory<ClientContext>
    {
        public ClientContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClientContext>();
            optionsBuilder.UseSqlite("Data Source=SideSeams.db");

            return new ClientContext(optionsBuilder.Options);
        }
    }
}
