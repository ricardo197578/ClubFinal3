using System;
using System.Windows.Forms;
using ClubMinimal.Models;
using ClubMinimal.Repositories;
using ClubMinimal.Services;

namespace ClubMinimal.Views.Forms
{
    public partial class frmBuscarSocioPorDni : Form
    {
        private readonly SocioRepository _socioRepository;
        
        public frmBuscarSocioPorDni(SocioRepository socioRepository)
        {
            InitializeComponent();
            _socioRepository = socioRepository;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = txtDni.Text.Trim();
                
                if (string.IsNullOrEmpty(dni))
                {
                    MessageBox.Show("Por favor ingrese un DNI", "Advertencia", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Socio socio = _socioRepository.ObtenerPorDni(dni);
                
                if (socio == null)
                {
                    MessageBox.Show("No se encontró ningún socio con ese DNI", "Información", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    return;
                }

                // Mostrar los datos del socio
                MostrarSocio(socio);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al buscar socio:{0}",ex.Message), "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarSocio(Socio socio)
        {
            txtId.Text = socio.Id.ToString();
            txtNombre.Text = socio.Nombre;
            txtApellido.Text = socio.Apellido;
            txtDni.Text = socio.Dni;
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtDni.Clear();
            txtDni.Focus();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}