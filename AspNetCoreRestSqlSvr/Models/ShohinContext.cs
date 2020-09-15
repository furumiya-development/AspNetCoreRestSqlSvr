using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreRestSqlSvr.Models
{
    public class ShohinContext : DbContext
    {
        public ShohinContext(DbContextOptions<ShohinContext> options) : base(options)
        {

        }

        public DbSet<ShohinEntity> ShohinItems { get; set; }
    }
}