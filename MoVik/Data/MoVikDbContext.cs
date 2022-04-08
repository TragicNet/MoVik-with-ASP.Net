using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoVik.Models;

namespace MoVik.Data
{
    public class MoVikDbContext : DbContext
    {
        public MoVikDbContext (DbContextOptions<MoVikDbContext> options)
            : base(options)
        {
        }
        public DbSet<MoVik.Models.MenuItem> MenuItem { get; set; }

        public virtual DbSet<MoVik.Models.FoodItem> FoodItems { get; set; }
    }
}
