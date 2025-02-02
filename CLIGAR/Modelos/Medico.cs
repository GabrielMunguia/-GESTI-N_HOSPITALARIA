﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLIGAR.Modelos
{
    class Medico
    {
        int idMedico;
        int idEmpleado;

        public int IdMedico
        {
            get
            {
                return idMedico;
            }

            set
            {
                idMedico = value;
            }
        }

        public int IdEmpleado
        {
            get
            {
                return idEmpleado;
            }

            set
            {
                idEmpleado = value;
            }
        }


        public Boolean Guardar()
        {
            Boolean resultado = false;
            StringBuilder Sentencia = new StringBuilder();
            DataManager.DBOperacion operacion = new DataManager.DBOperacion();
            try
            {
                Sentencia.Append("INSERT INTO `cligar`.`medicos`(`idMedico`,`idEmpleado`)VALUES(null,");
                Sentencia.Append("'" + this.IdEmpleado + "');");
        
       
                if (operacion.Insertar(Sentencia.ToString()) > 0)
                {
                    resultado = true;
                }

             


            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.Message);

                resultado = false;
            }

            return resultado;
        }


        public int obtenerUltimoIDInsertado()
        {
            StringBuilder Sentencia = new StringBuilder();
            DataManager.DBOperacion operacion = new DataManager.DBOperacion();
            int id = -1;
            try
            {
                Sentencia.Append("SELECT Max(idMedico)FROM medicos;");

                DataTable resultado = operacion.Consultar(Sentencia.ToString());
                if (resultado.Rows.Count > 0)
                {
                    id = Int32.Parse(resultado.Rows[0][0].ToString());
                }



            }
            catch (Exception)
            {

                id = -1;
            }

            return id;


        }

        public DataTable obtenerMedicoPorJVM(string jvm)
        {
            DataTable Resultado = new DataTable();
            StringBuilder Sentencia = new StringBuilder();
            DataManager.DBOperacion operacion = new DataManager.DBOperacion();
            try
            {
                Sentencia.Append("SELECT * FROM medicos as m , empleados as e where m.idEmpleado=e.idEmpleado and e.CodigoRegistroMedico= " + jvm);

                Resultado = operacion.Consultar(Sentencia.ToString());

            }
            catch (Exception)
            {

                Resultado = new DataTable();
            }
            return Resultado;
        }

        public DataTable obtenerMedicoPorNombre(string nombre)
        {
            DataTable Resultado = new DataTable();
            StringBuilder Sentencia = new StringBuilder();
            DataManager.DBOperacion operacion = new DataManager.DBOperacion();
            try
            {
                Sentencia.Append("SELECT m.idMedico,e.Nombres, e.Apellidos, e.Estado FROM medicos as m , empleados as e where m.idEmpleado=e.idEmpleado and e.Nombres LIKE '%" + nombre + "%' or e.Apellidos LIKE '%" + nombre + "%' and e.Estado=1;");

                Resultado = operacion.Consultar(Sentencia.ToString());

            }
            catch (Exception)
            {

                Resultado = new DataTable();
            }
            return Resultado;
        }

        public int obtenerIdMedicoPorIdEmpleado(int idEmpleado)
        {
            StringBuilder Sentencia = new StringBuilder();
            DataManager.DBOperacion operacion = new DataManager.DBOperacion();
            int id = -1;
            try
            {
                Sentencia.Append("SELECT * FROM cligar.medicos where idEmpleado="+idEmpleado+";");

                DataTable resultado = operacion.Consultar(Sentencia.ToString());
                if (resultado.Rows.Count > 0)
                {
                    id = Int32.Parse(resultado.Rows[0][0].ToString());
                }



            }
            catch (Exception)
            {

                id = -1;
            }

            return id;
        }

        public DataTable busqueda(string q)
        {
            DataTable Resultado = new DataTable();
            StringBuilder Sentencia = new StringBuilder();
            DataManager.DBOperacion operacion = new DataManager.DBOperacion();
            try
            {
                Sentencia.Append("SELECT  m.idMedico as Codigo,e.Nombres,e.Apellidos,e.DUI,e.NIT,e.CodigoRegistroMedico FROM medicos as m , empleados as e where m.idEmpleado=e.idEmpleado  and ");
                Sentencia.Append(" ( Nombres LIKE '%" + q + "%' or ");
                Sentencia.Append("  Apellidos LIKE '%" + q + "%' or ");
                Sentencia.Append("  Apellidos LIKE '%" + q + "%' or ");
                Sentencia.Append("  CodigoRegistroMedico LIKE '%" + q + "%' or ");
                Sentencia.Append(" concat(Nombres,' ',Apellidos) LIKE '%" + q + "%' or ");
                Sentencia.Append("  DUI LIKE '%" + q + "%') ; ");



                Clipboard.SetText(Sentencia.ToString());
                Resultado = operacion.Consultar(Sentencia.ToString());

            }
            catch (Exception)
            {
                MessageBox.Show("error");
                Resultado = new DataTable();
            }
            return Resultado;
        }

        public DataTable obtenerHorarios()
        {
            DataTable Resultado = new DataTable();
            StringBuilder Sentencia = new StringBuilder();
            DataManager.DBOperacion operacion = new DataManager.DBOperacion();
            try
            {
                Sentencia.Append("SELECT * FROM cligar.horarios where idMedico= "+this.idMedico);
              


             
                Resultado = operacion.Consultar(Sentencia.ToString());

            }
            catch (Exception)
            {
                MessageBox.Show("error");
                Resultado = new DataTable();
            }
            return Resultado;
        }

        public DataTable obtenerMedicoPorIdMedico()
        {
            DataTable Resultado = new DataTable();
            StringBuilder Sentencia = new StringBuilder();
            DataManager.DBOperacion operacion = new DataManager.DBOperacion();
            try
            {
                Sentencia.Append("SELECT * FROM medicos as m , empleados as e where m.idEmpleado=e.idEmpleado and idMedico="+this.IdMedico);




                Resultado = operacion.Consultar(Sentencia.ToString());

            }
            catch (Exception)
            {
                MessageBox.Show("error");
                Resultado = new DataTable();
            }
            return Resultado;
        }
        public DataTable obtenerMedicoPorEspecialidad(string id)
        {
            DataTable Resultado = new DataTable();
            StringBuilder Sentencia = new StringBuilder();
            DataManager.DBOperacion operacion = new DataManager.DBOperacion();
            try
            {
                Sentencia.Append("SELECT m.idMedico,e.Nombres, e.Apellidos, e.Telefono, e.direccion, e.Genero, e.CodigoRegistroMedico");
                Sentencia.Append(" FROM  medicos as m, empleados as e, especialidades esp, especialidades_medicos em");
                Sentencia.Append(" where m.idEmpleado = e.idEmpleado and m.idMedico = em.idMedico and em.idEspecialidad = esp.idEspecialidad");
                Sentencia.Append(" and e.Estado = 1 and esp.idEspecialidad ="+ id);

                Resultado = operacion.Consultar(Sentencia.ToString());

            }
            catch (Exception)
            {

                Resultado = new DataTable();
            }
            return Resultado;
        }

    }
}
