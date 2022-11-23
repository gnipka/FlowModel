using Microsoft.EntityFrameworkCore;

namespace FlowModel.InteractionDB
{
    internal class FlowModelContext : DbContext
    {
        public string ConnectionString;

        public FlowModelContext()
        {
            ConnectionString = "Data Source=FlowModelDB.db";
        }

        public DbSet<Material> Material { get; set; }
        public DbSet<EmpiricalCoef> Empirical_coef { get; set; }
        public DbSet<CharacteristicMaterial> Characteristic_material { get; set; }
        public DbSet<ValueEmpiricalCoef> Value_Empirical_Coef { get; set; }
        public DbSet<ValueCharacteristicMaterial> Value_Characteristic_Material { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Unit> Unit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }
    }
}
