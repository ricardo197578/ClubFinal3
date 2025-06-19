using System;
using System.Windows.Forms;
using ClubMinimal.Services;
using ClubMinimal.Repositories;
using ClubMinimal.Models;

namespace ClubMinimal.Views.Forms
{

    public class NoSocioForm : Form
    {
        private readonly NoSocioService _noSocioService;
        private readonly TextBox txtNombre;
        private readonly TextBox txtApellido;
        private readonly TextBox txtDni;//para dni
        private readonly ListBox listBox;

        public NoSocioForm()
        {
            // Configuración inicial del formulario
            this.Text = "Gestión de No Socios";
            this.Width = 450;
            this.Height = 450;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Inicialización de dependencias
            var dbHelper = new DatabaseHelper();
            var repo = new NoSocioRepository(dbHelper);
            _noSocioService = new NoSocioService(repo);

            // Inicializacion
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            txtDni = new TextBox();//nuevo
            listBox = new ListBox();

            // Crear controles
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Configuración de los controles ya inicializados
            txtNombre.Left = 120;
            txtNombre.Top = 20;
            txtNombre.Width = 300;

            txtApellido.Left = 120;
            txtApellido.Top = 60;
            txtApellido.Width = 300;

            txtDni.Left = 120;
            txtDni.Top = 100;
            txtDni.Width = 300;

            listBox.Left = 20;
            listBox.Top = 250;
            listBox.Width = 400;
            listBox.Height = 200;
            listBox.Font = new System.Drawing.Font("Consolas", 9.75f);

            // Controles para Nombre
            var lblNombre = new Label
            {
                Text = "Nombre:",
                Left = 20,
                Top = 20,
                Width = 80
            };

            // Controles para Apellido
            var lblApellido = new Label
            {
                Text = "Apellido:",
                Left = 20,
                Top = 60,
                Width = 80
            };

            //nuevo para Dni
            var lblDni = new Label
            {
                Text = "Dni",
                Left = 20,
                Top = 100,
                Width = 80
            };


            // Botones
            var btnGuardar = new Button
            {
                Text = "Registrar No Socio",
                Left = 120,
                Top = 140,
                Width = 150
            };

            var btnListar = new Button
            {
                Text = "Listar No Socios",
                Left = 120,
                Top = 180,
                Width = 150
            };

            // Configuración de eventos
            btnGuardar.Click += BtnGuardar_Click;
            btnListar.Click += BtnListar_Click;

            // Agregar controles al formulario
            this.Controls.AddRange(new Control[]
            {
                lblNombre, txtNombre,
                lblApellido, txtApellido,
                lblDni , txtDni,
                btnGuardar, btnListar,
                listBox
            });
        }




        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validación completa de todos los campos requeridos
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                   string.IsNullOrWhiteSpace(txtApellido.Text) ||
                   string.IsNullOrWhiteSpace(txtDni.Text))
                {
                    MessageBox.Show("Debe completar nombre, apellido y DNI", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                // Registrar con todos los datos
                _noSocioService.RegistrarNoSocio(txtNombre.Text, txtApellido.Text, txtDni.Text);

                MessageBox.Show("No Socio registrado exitosamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar campos
                txtNombre.Clear();
                txtApellido.Clear();
                txtDni.Clear();
                txtNombre.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al registrar:{0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnListar_Click(object sender, EventArgs e)
        {
            try
            {
                listBox.Items.Clear();
                var noSocios = _noSocioService.ObtenerNoSocios();

                // Encabezado
                listBox.Items.Add("ID\tNombre\t\tApellido\tFecha Registro");
                listBox.Items.Add(new string('-', 70));

                foreach (var ns in noSocios)
                {
                    listBox.Items.Add(
                        string.Format("{0}\t{1}\t\t{2}\t{3}",
                        ns.Id,
                        ns.Nombre.PadRight(10).Substring(0, 10),
                        ns.Apellido.PadRight(10).Substring(0, 10),
                        ns.FechaRegistro.ToShortDateString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al listar: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

