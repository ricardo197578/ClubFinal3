using System;
using System.Windows.Forms;
using ClubMinimal.Services;
using System.Collections.Generic;
using ClubMinimal.Models;
using ClubMinimal.Interfaces;

namespace ClubMinimal.Views.Forms
{
    public class SociosConCuotasForm : Form
    {
        //private readonly CuotaService _cuotaService;
        private readonly ICuotaService _cuotaService;
        private ListBox listBoxSocios;
        private Button btnFiltrarVencidos;
        private Button btnFiltrarTodos;
        private Button btnVerDetalle;
        private DateTimePicker dtpFechaConsulta;

        public SociosConCuotasForm(ICuotaService cuotaService)
        {
            _cuotaService = cuotaService;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Socios con Cuotas";
            this.Width = 700;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Controles
            dtpFechaConsulta = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Left = 20,
                Top = 20,
                Width = 150,
                Value = DateTime.Today
            };

            btnFiltrarVencidos = new Button
            {
                Text = "Filtrar Vencidos",
                Left = 180,
                Top = 20,
                Width = 120
            };

            btnFiltrarTodos = new Button
            {
                Text = "Mostrar Todos",
                Left = 310,
                Top = 20,
                Width = 120
            };

            btnVerDetalle = new Button
            {
                Text = "Ver Detalle",
                Left = 440,
                Top = 20,
                Width = 120,
                Enabled = false
            };

            listBoxSocios = new ListBox
            {
                Left = 20,
                Top = 60,
                Width = 650,
                Height = 380
            };

            // Eventos
            btnFiltrarVencidos.Click += (s, e) => FiltrarSocios(true);
            btnFiltrarTodos.Click += (s, e) => FiltrarSocios(false);
            btnVerDetalle.Click += BtnVerDetalle_Click;
            listBoxSocios.SelectedIndexChanged += (s, e) => 
                btnVerDetalle.Enabled = listBoxSocios.SelectedIndex != -1;

            // Agregar controles
            this.Controls.AddRange(new Control[] {
                dtpFechaConsulta,
                btnFiltrarVencidos,
                btnFiltrarTodos,
                btnVerDetalle,
                listBoxSocios
            });
        }

        private void FiltrarSocios(bool soloVencidos)
        {
            listBoxSocios.Items.Clear();
            IEnumerable<Socio> socios;

            if (soloVencidos)
            {
                socios = _cuotaService.ObtenerSociosConCuotasVencidas(dtpFechaConsulta.Value);
            }
            else
            {
                socios = _cuotaService.ObtenerTodosSocios();
            }

            foreach (var socio in socios)
{
    string item = string.Format("{0}, {1} - DNI: {2} - Vencimiento: {3:dd/MM/yyyy} - Estado: {4}",
        socio.Apellido,
        socio.Nombre,
        socio.Dni,
        socio.FechaVencimientoCuota,
        socio.EstadoActivo ? "Activo" : "Inactivo");

    listBoxSocios.Items.Add(item);
}

        }
        //completar
        private void BtnVerDetalle_Click(object sender, EventArgs e)
        {
            if (listBoxSocios.SelectedItem != null)
            {
                
                MessageBox.Show("Detalle del socio seleccionado", "Detalle", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}