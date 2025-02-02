﻿using CLIGAR.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLIGAR.GUI
{
    public partial class Login : Form
    {
        SessionManager.Sesion session = SessionManager.Sesion.Instance;

        public Login()
        {
            InitializeComponent();     
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            this.labelErrorLogin.Visible = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);

        }
        
        private void logeo()
        {
            try
            {
                this.labelErrorLogin.Visible = false;

                this.btnLogin.Text = "Cargando..";

                string password = inputPassword.Text;

                string usuario = inputUsuario.Text;

                //Se realiza la consulta a la base de datos
                DataTable tabla = DataSource.Consultas.INICIO_SESION(usuario, password);

                //Se comprueba que se encontro un usuario con esas credenciales 

                if (tabla.Rows.Count > 0)
                {
                    //Si  hay un registro con esas credenciales se procede a cerrar el login
                    // Y abrir el dashboard

                    Dashboard dash = new Dashboard();

                    //Almaceno los datos del usuario que ingreso 
                    session.Nombres = tabla.Rows[0][5].ToString();
                    session.Apellidos = tabla.Rows[0][6].ToString();

                    session.Cargo = tabla.Rows[0][10].ToString();
                    if (tabla.Rows[0][10].ToString() == "2")
                    {
                        Medico m = new Medico();
                        session.IdMedico = m.obtenerIdMedicoPorIdEmpleado(Int32.Parse(tabla.Rows[0][4].ToString())).ToString();
                    }
                    session.IDUsuario = tabla.Rows[0][0].ToString();
                    session.IDEmpleado = tabla.Rows[0][4].ToString();
                    this.Hide();

                    dash.ShowDialog();

                    this.Close();

                }

                //Si no se encuentra ninguno se hace visible el mensaje de error
                //Y se coloca nuevamente el texto de iniciar session 

                this.labelErrorLogin.Visible = true;

                this.btnLogin.Text = "Iniciar session";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error comuniquese con un administrador");
                MessageBox.Show(ex.Message);
            }
        }


        private void btnLogin_Click(object sender, EventArgs e)

        {
            logeo();
        }

        private void inputPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                logeo();
            }      
        }

        private void inputUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
               this.inputPassword.Focus();
            }
        }
    
    }
}
