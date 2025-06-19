using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ClubMinimal.Models;
using ClubMinimal.Services;
using ClubMinimal.Interfaces;

namespace ClubMinimal.Views.Forms
{
    public class GestionCuotasForm : Form
    {
        private readonly ICuotaService _cuotaService;
        private readonly ListBox listBox;

        private Button btnListarTodos;
        private Button btnSociosVencidos;
        private Button btnDetalleCuotas;

        public GestionCuotasForm(ICuotaService cuotaService)
        {
            _cuotaService = cuotaService;

            this.Text = "Gestión de Cuotas";
            this.Width = 700;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen;

            listBox = new ListBox();
            listBox.Location = new Point(20, 80);
            listBox.Size = new Size(this.Width - 40, 350);
            listBox.Font = new Font("Consolas", 10);

            InitializeButtons();

            this.Controls.Add(listBox);
        }

        private void InitializeButtons()
        {
            btnListarTodos = new Button();
            btnListarTodos.Text = "Listar Todos los Socios";
            btnListarTodos.Location = new Point(20, 20);
            btnListarTodos.Size = new Size(150, 30);
            btnListarTodos.Click += new EventHandler(BtnListarTodos_Click);

            btnSociosVencidos = new Button();
            btnSociosVencidos.Text = "Socios con Cuotas Vencidas";
            btnSociosVencidos.Location = new Point(180, 20);
            btnSociosVencidos.Size = new Size(180, 30);
            btnSociosVencidos.BackColor = Color.LightCoral;
            btnSociosVencidos.Click += new EventHandler(BtnSociosVencidos_Click);

            btnDetalleCuotas = new Button();
            btnDetalleCuotas.Text = "Ver Detalle de Cuotas";
            btnDetalleCuotas.Location = new Point(370, 20);
            btnDetalleCuotas.Size = new Size(150, 30);
            btnDetalleCuotas.BackColor = Color.LightGreen;
            btnDetalleCuotas.Click += new EventHandler(BtnDetalleCuotas_Click);

            this.Controls.Add(btnListarTodos);
            this.Controls.Add(btnSociosVencidos);
            this.Controls.Add(btnDetalleCuotas);
        }

        private void BtnListarTodos_Click(object sender, EventArgs e)
        {
            CargarSocios(_cuotaService.ObtenerTodosSocios());
        }

        private void BtnSociosVencidos_Click(object sender, EventArgs e)
        {
            CargarSocios(_cuotaService.ObtenerSociosConCuotasVencidas(DateTime.Today));
        }

        private void BtnDetalleCuotas_Click(object sender, EventArgs e)
        {
            SociosConCuotasForm detalleForm = new SociosConCuotasForm(_cuotaService);
            detalleForm.ShowDialog();
        }

        private void CargarSocios(IEnumerable<Socio> socios)
        {
            listBox.Items.Clear();

            foreach (Socio socio in socios.OrderBy(s => s.Apellido).ThenBy(s => s.Nombre))
            {
                string estadoCuota = socio.FechaVencimientoCuota < DateTime.Today ? "VENCIDO" : "AL DÍA";
                string texto = string.Format("{0,-30} {1,-15} {2,-15} {3,-10}",
                    string.Format("{0}, {1}", socio.Apellido, socio.Nombre),
                    string.Format("DNI: {0}", socio.Dni),
                    string.Format("Vence: {0:dd/MM/yyyy}", socio.FechaVencimientoCuota),
                    string.Format("({0})", estadoCuota));

                listBox.Items.Add(texto);
            }
        }
    }
}
