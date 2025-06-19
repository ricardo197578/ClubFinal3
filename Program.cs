using System;
using System.Windows.Forms;
using ClubDeportivo.Repositories;
using ClubDeportivo.Services;
using ClubDeportivo.Views.Forms;

namespace ClubDeportivo
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                DebugMessage("Iniciando aplicación...");

                var dbHelper = new DatabaseHelper("ClubDB.sqlite");
                DebugMessage("Base de datos configurada");

                // Crear los repositorios
                var socioRepository = new SocioRepository(dbHelper);
                var carnetRepository = new CarnetRepository(dbHelper);
                var pagoRepository = new PagoRepository(dbHelper);
                var actividadRepository = new ActividadRepository(dbHelper);
                var noSocioRepository = new NoSocioRepository(dbHelper);
                //para cuota 
                var cuotaRepository = new CuotaRepository(dbHelper);
                //var cuotaService = new CuotaService(cuotaRepository);
                var cuotaService = new CuotaService(cuotaRepository, socioRepository);

                // Crear los servicios
                var socioService = new SocioService(socioRepository);
                var carnetService = new CarnetService(carnetRepository);
                var actividadService = new ActividadService(actividadRepository);
                var noSocioService = new NoSocioService(noSocioRepository);
                var pagoService = new PagoService(
                    pagoRepository,
                    actividadRepository,
                    noSocioRepository);

                DebugMessage("Servicios creados");

                // Pasar todos los servicios requeridos (7 parámetros) Despeus sacar lo que no me piden!!!
                var mainForm = new MenuPrincipalForm(
                    socioService,
                    carnetService,
                    pagoService,
                    socioRepository,
                    noSocioService,
                    actividadService,
                    actividadRepository,
                    cuotaService,  
                    cuotaRepository);

                mainForm.Shown += (s, e) => DebugMessage("Formulario visible");

                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                ShowError(string.Format("Error crítico:{0}", ex.Message));
                
            }
            finally
            {
                DebugMessage("Aplicación finalizada");
            }
        }

        private static void DebugMessage(string message)
        {
#if DEBUG
            Console.WriteLine($"[DEBUG] {DateTime.Now}: {message}");
            // MessageBox.Show(message, "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
        }

        private static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}