using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using dotnetEval.Models;

namespace dotnetEval.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }
        
    public DbSet<EtapeEquipePenalite> _etape_equipe_penalite {get; set;}
    public DbSet<Administrateur> _administrateur {get; set;}
 
    public DbSet<Genre> _genre {get; set;} 
 
    public DbSet<Equipe> _equipe {get; set;}
 
    public DbSet<Categorie> _categorie {get; set;}
 
    public DbSet<CategorieCoureur> _categorie_coureur {get; set;}
 
    public DbSet<Etape> _etape {get; set;}
 
    public DbSet<EtapeCoureur> _etape_coureur {get; set;}
 
    public DbSet<TempsCoureur> _temps_coureur {get; set;}
    public DbSet<Coureur> _coureur {get; set;}
 
    public DbSet<Points> _points {get; set;}
 
    public DbSet<PointsCoureur> _points_coureur {get; set;}


    public DbSet<TempEtape> _temp_etape {get; set;}
 
    public DbSet<TempPoints> _temp_points {get; set;}
    public DbSet<TempResultat> _temp_resultat {get; set;}
    public DbSet<Classement_general_view> _classement_general_view {get; set;}
    public DbSet<ClassementGeneralCategorieView> _classement_general_categorie_view {get; set;}
    public DbSet<VEtapeCoureurEquipeDuree> _v_etape_coureur_equipe_duree {get; set;}
    public DbSet<VEtapeCoureurEquipe> _v_etape_coureur_equipe {get; set;}
    public DbSet<CoefficientEtape> _coefficient_etape {get; set;}
 
    public DbSet<ClassementGeneralCoefficientEtape> _classement_general_coefficient_etape {get; set;}
 
    public DbSet<ClassementGeneralCoefficientEtapeRang> _classement_general_coefficient_etape_rang {get; set;}




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // modelBuilder.Entity<ListDevisClient>().HasNoKey();
            modelBuilder.Entity<Classement_general_view>().HasNoKey();
            modelBuilder.Entity<TempEtape>().HasNoKey();
            modelBuilder.Entity<TempPoints>().HasNoKey();
            modelBuilder.Entity<TempResultat>().HasNoKey();
            modelBuilder.Entity<ClassementGeneralCategorieView>().HasNoKey();
            modelBuilder.Entity<ClassementGeneralCoefficientEtape>().HasNoKey();
            modelBuilder.Entity<ClassementGeneralCoefficientEtapeRang>().HasNoKey();

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Administrateur>() 
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR administrateur_seq");

            modelBuilder.Entity<Genre>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR genre_seq");

            modelBuilder.Entity<Equipe>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR equipe_seq");

            modelBuilder.Entity<Categorie>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR categorie_seq");

            modelBuilder.Entity<CategorieCoureur>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR categorie_coureur_seq");

            modelBuilder.Entity<Etape>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR etape_seq");

            modelBuilder.Entity<EtapeCoureur>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR etape_coureur_seq");

            modelBuilder.Entity<TempsCoureur>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR temps_coureur_seq");

            modelBuilder.Entity<Coureur>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR coureur_seq");

            modelBuilder.Entity<Points>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR points_seq");

            modelBuilder.Entity<PointsCoureur>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR points_coureur_seq");
            modelBuilder.Entity<EtapeEquipePenalite>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR etape_equipe_penalite_seq");
            modelBuilder.Entity<VEtapeCoureurEquipeDuree>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR ");

            modelBuilder.Entity<CoefficientEtape>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR coefficient_etape_seq");

            modelBuilder.Entity<CategorieCoureur>()
                .HasOne(d => d.Coureur)
                .WithMany()
                .HasForeignKey(d => d.IdCoureur);

            modelBuilder.Entity<CategorieCoureur>()
                .HasOne(d => d.Categorie)
                .WithMany()
                .HasForeignKey(d => d.IdCategorie);

            modelBuilder.Entity<Coureur>()
                .HasOne(d => d.Equipe)
                .WithMany()
                .HasForeignKey(d => d.IdEquipe);

            modelBuilder.Entity<Coureur>()
                .HasOne(d => d.Genre)
                .WithMany()
                .HasForeignKey(d => d.IdGenre);


            modelBuilder.Entity<EtapeCoureur>()
                .HasOne(d => d.Etape)
                .WithMany()
                .HasForeignKey(d => d.IdEtape);

            modelBuilder.Entity<EtapeCoureur>()
                .HasOne(d => d.Coureur)
                .WithMany()
                .HasForeignKey(d => d.IdCoureur);

            modelBuilder.Entity<PointsCoureur>()
                .HasOne(d => d.Etape)
                .WithMany()
                .HasForeignKey(d => d.IdEtape);
            modelBuilder.Entity<PointsCoureur>()
                .HasOne(d => d.Points)
                .WithMany()
                .HasForeignKey(d => d.IdPoints);
            modelBuilder.Entity<PointsCoureur>()
                .HasOne(d => d.Coureur)
                .WithMany()
                .HasForeignKey(d => d.IdCoureur);
            modelBuilder.Entity<TempsCoureur>()
                .HasOne(d => d.Etape)
                .WithMany()
                .HasForeignKey(d => d.IdEtape);

            modelBuilder.Entity<TempsCoureur>()
                .HasOne(d => d.Coureur)
                .WithMany()
                .HasForeignKey(d => d.IdCoureur);

            modelBuilder.Entity<EtapeEquipePenalite>()
                .HasOne(d => d.Etape)
                .WithMany()
                .HasForeignKey(d => d.IdEtape);

            modelBuilder.Entity<EtapeEquipePenalite>()
                .HasOne(d => d.Equipe)
                .WithMany()
                .HasForeignKey(d => d.IdEquipe);
            }

        public DbSet<DataImport> dataImport { get; set; }
        public DbSet<Map_data> _map_data {get; set;}
    }
}