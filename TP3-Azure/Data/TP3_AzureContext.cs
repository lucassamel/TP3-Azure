using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TP3_Azure.Models;

namespace TP3_Azure.Data
{
    public class TP3_AzureContext : DbContext
    {
        public TP3_AzureContext (DbContextOptions<TP3_AzureContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Alterar a senha para acessar o banco
            optionsBuilder.UseSqlServer("Server=tcp:lucassamel-db-2020.database.windows.net,1433;" +
                "Initial Catalog=lucassamel-db-2020;Persist Security Info=False;User ID=lucassamel;" +
                "Password=C@dead0Sam3l;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;" +
                "Connection Timeout=30;");
        }

        public DbSet<Amigos> Amigos { get; set; }
    }
}
