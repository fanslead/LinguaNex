using LinguaNex.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaNex.EntityFrameworkCore
{
    public class LinguaNexDbContext : DbContext
    {
        public LinguaNexDbContext(DbContextOptions<LinguaNexDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Projects>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Id).HasMaxLength(36);
                e.Property(a => a.Name).HasMaxLength(128);
                e.HasMany(a=>a.ProjectAssociations);
            });
            modelBuilder.Entity<ProjectAssociation>(e =>
            {
                e.HasKey(a => new { a.MainProjectId, a.AssociationProjectId });
                e.Property(a => a.MainProjectId).HasMaxLength(36);
                e.Property(a => a.AssociationProjectId).HasMaxLength(36);
                e.HasOne(a => a.MainProject);
                e.HasOne(a => a.AssociationProject);
            });
            modelBuilder.Entity<Culture>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Id).HasMaxLength(36);
                e.Property(a => a.Name).HasMaxLength(64);
                e.HasOne(a => a.Project);
                e.HasMany(a => a.Resources);
            });
            modelBuilder.Entity<Resource>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Id).HasMaxLength(36);
                e.HasOne(a => a.Project);
                e.HasOne(a => a.Culture);
                e.Property(a => a.Key).HasMaxLength(256);
                e.Property(a => a.Value).HasMaxLength(16384);
            });
        }
    }
}
