using CentroSalud3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CentroSalud3.DbContext
{
    public class AmbulatorioDbContext : IdentityDbContext<ApplicationUser> /*Microsoft.EntityFrameworkCore.DbContext*/
    {
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Enfermera> Enfermeras { get; set; }
        public DbSet<Medicacion> Medicaciones { get; set; }

        public DbSet<PacienteConMedico> PacientesConMedicos { get; set; }
        public DbSet<PacienteConEnfermera> PacientesConEnfermeras { get; set; }
        public DbSet<PacienteConMedicacion> PacientesConMedicaciones { get; set; }

        public AmbulatorioDbContext(DbContextOptions<AmbulatorioDbContext> options) : base(options)
        {
        }

        #region Seed
        ////Seed
        //#region OnModelCreating
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    this.SeedPacientes(builder);
        //    this.SeedMedicos(builder);
        //    this.SeedEnfermeras(builder);
        //    this.SeedMedicaciones(builder);

        //    //this.SeedRoles(builder);
        //    //this.SeedUserRoles(builder);
        //}
        //#endregion OnModelCreating

        //#region SeedPacientes
        //private void SeedPacientes(ModelBuilder builder)
        //{
        //    Paciente p = new Paciente()
        //    {
        //        PacienteId = 1,
        //        PacienteNombre = "Ataúlfo",
        //        PacienteFxNacimiento = DateTime.ParseExact("15/06/1972", "dd/MM/yyyy", null),
        //        PacienteEdad = 43,
        //        PacienteSexo = (Sexo)1,
        //        PacienteTelefono = "372372372",
        //        PacienteEmail = "ataulfo@paciente.es"
        //    };
        //    //PasswordHasher<Paciente> passwordHasher = new PasswordHasher<Paciente>();
        //    //passwordHasher.HashPassword(p, "AA**00aa");
        //    //builder.Entity<Paciente>().HasData(p);
        //}
        //#endregion SeedPacientes

        //#region SeedMedicos
        //private void SeedMedicos(ModelBuilder builder)
        //{
        //    Medico m = new Medico()
        //    {
        //        MedicoId = 1,
        //        MedicoNombre = "Severo Ochoa",
        //        MedicoConsulta = "10",
        //        MedicoTelefono = "123123123",
        //        MedicoEmail = "sochoa@medico.es",
        //        NumPacientes = 0
        //    };
        //    //PasswordHasher<Medico> passwordHasher = new PasswordHasher<Medico>();
        //    //passwordHasher.HashPassword(m, "AA**00aa");
        //    //builder.Entity<Medico>().HasData(m);
        //}
        //#endregion SeedMedicos

        //#region SeedEnfermeras
        //private void SeedEnfermeras(ModelBuilder builder)
        //{
        //    Enfermera e = new Enfermera()
        //    {
        //        EnfermeraId = 1,
        //        EnfermeraNombre = "Isabel Zendal",
        //        EnfermeraConsulta = "10",
        //        EnfermeraTelefono = "123123123",
        //        EnfermeraEmail = "izendal@enfermera.es",
        //        EnfermeraNumPacientes = 0
        //    };
        //    //PasswordHasher<Enfermera> passwordHasher = new PasswordHasher<Enfermera>();
        //    //passwordHasher.HashPassword(e, "AA**00aa");
        //    //builder.Entity<Enfermera>().HasData(e);
        //}
        //#endregion SeedEnfermeras

        //#region SeedMedicaciones
        //private void SeedMedicaciones(ModelBuilder builder)
        //{
        //    Medicacion me = new Medicacion()
        //    {
        //        MedicacionId = 1,
        //        MedicacionNombre = "Omeprazol",
        //        MedicacionDosis = 10,
        //        MedicacionGrupo = (MedicacionGrupo)1,
        //        MedicacionDescripcion = "",
        //        NumPacientesPautados = 0,
        //    };
        //    //PasswordHasher<Medicacion> passwordHasher = new PasswordHasher<Medicacion>();
        //    //passwordHasher.HashPassword(me, "AA**00aa");
        //    //builder.Entity<Medicacion>().HasData(me);
        //}
        //#endregion SeedMedicaciones

        //#region SeedRoles
        ////private void SeedRoles(ModelBuilder builder)
        ////{
        ////    builder.Entity<IdentityRole>().HasData(
        ////        new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Administrador", ConcurrencyStamp = "1", NormalizedName = "Administrador" },
        ////        new IdentityRole() { Id = "c2b013f0-5201-4217-abd8-c211f91b7330", Name = "Paciente", ConcurrencyStamp = "2", NormalizedName = "Paciente" },
        ////        new IdentityRole() { Id = "c8b013f0-5201-4357-abd8-c211f91b7330", Name = "Medico", ConcurrencyStamp = "3", NormalizedName = "Medico" },
        ////        new IdentityRole() { Id = "c5b013f0-5201-4337-abd8-c211f91b7330", Name = "Enfermera", ConcurrencyStamp = "4", NormalizedName = "Enfermera" }
        ////        );
        ////}

        ////private void SeedUserRoles(ModelBuilder builder)
        ////{
        ////    builder.Entity<IdentityUserRole<string>>().HasData(
        ////        new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" }
        ////        );
        ////}
        //#endregion SeedRoles
        #endregion Seed
    }
}
