using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowModel.InteractionDB
{
    internal class FlowModelContext : DbContext
    {
        public DbSet<Material> Material { get; set; }
        public DbSet<EmpiricalCoef> Empirical_coef { get; set; }
        public DbSet<CharacteristicMaterial> Characteristic_material { get; set; }
        public DbSet<ValueEmpiricalCoef> Value_Empirical_Coef { get; set; }
        public DbSet<ValueCharacteristicMaterial> Value_Characteristic_Material { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=FlowModelDB.db");
        }
    }
}
