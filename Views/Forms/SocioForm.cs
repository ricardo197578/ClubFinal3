using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ClubMinimal.Models;
using ClubMinimal.Repositories;
using ClubMinimal.Services;

namespace ClubMinimal.Views.Forms
{
    public class SocioForm : Form
    {
        private readonly SocioService _socioService;
        private readonly CuotaService _cuotaService;
        private readonly TextBox txtNombre;
        private readonly TextBox txtApellido;
        private readonly TextBox txtDni;
        private readonly DateTimePicker dtpFechaInscripcion;
        private readonly DateTimePicker dtpFechaVencimiento;
        private readonly CheckBox chkEstadoActivo;
        private readonly ComboBox cmbTipoSocio;
        private readonly ListBox listBox;

        public SocioForm()
        {
            this.Text = "Gestión de Socios";
            this.Width = 600;
            this.Height = 600;
            this.StartPosition = FormStartPosition.CenterScreen;

            var dbHelper = new DatabaseHelper();
            var socioRepo = new SocioRepository(dbHelper);
            var cuotaRepo = new CuotaRepository(dbHelper);

            _socioService = new SocioService(socioRepo);
            _cuotaService = new CuotaService(cuotaRepo, socioRepo);

            txtNombre = new TextBox();
            txtApellido = new TextBox();
            txtDni = new TextBox();
            dtpFechaInscripcion = new DateTimePicker();
            dtpFechaVencimiento = new DateTimePicker();
            chkEstadoActivo = new CheckBox();
            cmbTipoSocio = new ComboBox();
            listBox = new ListBox();

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            int top = 20;
            int spacing = 35;
            int labelWidth = 150;
            int inputLeft = labelWidth + 30;
            int inputWidth = 250;

            // Controles de entrada
            txtNombre.SetBounds(inputLeft, top, inputWidth, 20);
            txtApellido.SetBounds(inputLeft, top + spacing, inputWidth, 20);
            txtDni.SetBounds(inputLeft, top + spacing * 2, inputWidth, 20);

            dtpFechaInscripcion.SetBounds(inputLeft, top + spacing * 3, inputWidth, 20);
            dtpFechaInscripcion.Format = DateTimePickerFormat.Short;

            dtpFechaVencimiento.SetBounds(inputLeft, top + spacing * 4, inputWidth, 20);
            dtpFechaVencimiento.Format = DateTimePickerFormat.Short;

            chkEstadoActivo.SetBounds(inputLeft, top + spacing * 5, inputWidth, 20);
            chkEstadoActivo.Text = "Activo";
            chkEstadoActivo.Checked = true;

            cmbTipoSocio.SetBounds(inputLeft, top + spacing * 6, inputWidth, 21);
            cmbTipoSocio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoSocio.Items.AddRange(Enum.GetNames(typeof(TipoSocio)));
            cmbTipoSocio.SelectedIndex = 0;

            listBox.SetBounds(20, top + spacing * 9, this.Width - 40, 200);

            // Etiquetas
            this.Controls.Add(CreateLabel("Nombre:", top));
            this.Controls.Add(CreateLabel("Apellido:", top + spacing));
            this.Controls.Add(CreateLabel("DNI:", top + spacing * 2));
            this.Controls.Add(CreateLabel("Fecha Inscripción:", top + spacing * 3));
            this.Controls.Add(CreateLabel("Vencimiento Primera Cuota:", top + spacing * 4));
            this.Controls.Add(CreateLabel("Tipo de Socio:", top + spacing * 6));

            // Botones
            var btnGuardar = new Button { Text = "Guardar Socio" };
            btnGuardar.SetBounds(20, top + spacing * 7, 120, 30);
            btnGuardar.Click += btnGuardar_Click;

            var btnListar = new Button { Text = "Ver Todos" };
            btnListar.SetBounds(160, top + spacing * 7, 120, 30);
            btnListar.Click += btnListar_Click;

            var btnSociosVencidos = new Button { Text = "Socios Vencidos", BackColor = Color.LightCoral };
            btnSociosVencidos.SetBounds(300, top + spacing * 7, 120, 30);
            btnSociosVencidos.Click += BtnSociosVencidos_Click;

            var btnSociosCuotas = new Button { Text = "Gestión Cuotas", BackColor = Color.LightGreen };
            btnSociosCuotas.SetBounds(440, top + spacing * 7, 120, 30);
            btnSociosCuotas.Click += BtnSociosCuotas_Click;

            // Agregar controles
            this.Controls.AddRange(new Control[] {
                txtNombre, txtApellido, txtDni,
                dtpFechaInscripcion, dtpFechaVencimiento,
                chkEstadoActivo, cmbTipoSocio, listBox,
                btnGuardar, btnListar, btnSociosVencidos, btnSociosCuotas
            });
        }

        private Label CreateLabel(string text, int top)
        {
            return new Label
            {
                Text = text,
                Left = 20,
                Top = top,
                Width = 150,
                TextAlign = ContentAlignment.MiddleRight
            };
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNombre.Text) &&
                !string.IsNullOrWhiteSpace(txtApellido.Text) &&
                !string.IsNullOrWhiteSpace(txtDni.Text))
            {
                if (_socioService.ExisteDni(txtDni.Text))
                {
                    MessageBox.Show("El DNI ingresado ya está registrado. Por favor ingrese un DNI diferente.");
                    txtDni.Focus();
                    return;
                }

                var tipoNombre = cmbTipoSocio.SelectedItem.ToString();
                TipoSocio tipo;
                Enum.TryParse(tipoNombre, out tipo);

                var nuevoSocio = new Socio
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Dni = txtDni.Text,
                    FechaInscripcion = dtpFechaInscripcion.Value,
                    FechaVencimientoCuota = dtpFechaVencimiento.Value,
                    EstadoActivo = chkEstadoActivo.Checked,
                    Tipo = tipo
                };

                _socioService.RegistrarSocio(nuevoSocio);
                MessageBox.Show("Socio registrado correctamente!");
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Por favor complete todos los campos obligatorios (Nombre, Apellido y DNI)");
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            CargarSocios(_socioService.ObtenerSocios());
        }

        private void BtnSociosVencidos_Click(object sender, EventArgs e)
        {
            var sociosVencidos = _cuotaService.ObtenerSociosConCuotasVencidas(DateTime.Today);
            CargarSocios(sociosVencidos);
        }

        private void BtnSociosCuotas_Click(object sender, EventArgs e)
        {
            var formCuotas = new SociosConCuotasForm(_cuotaService);
            formCuotas.ShowDialog();
        }

        private void CargarSocios(IEnumerable<Socio> socios)
        {
            listBox.Items.Clear();
            foreach (var socio in socios.OrderBy(s => s.Apellido).ThenBy(s => s.Nombre))
            {
                string estadoCuota = socio.FechaVencimientoCuota < DateTime.Today ? "VENCIDO" : "AL DÍA";
                string texto = string.Format("{0}, {1} - DNI: {2} | Vence: {3} ({4})",
                    socio.Apellido,
                    socio.Nombre,
                    socio.Dni,
                    socio.FechaVencimientoCuota.ToString("dd/MM/yyyy"),
                    estadoCuota);

                listBox.Items.Add(texto);
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtDni.Clear();
            dtpFechaInscripcion.Value = DateTime.Today;
            dtpFechaVencimiento.Value = DateTime.Today.AddMonths(1);
            chkEstadoActivo.Checked = true;
            cmbTipoSocio.SelectedIndex = 0;
        }
    }
}
