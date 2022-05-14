﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIGAR.Modelos
{
    class ConsultaMedica
    {

        int _idConsulta;
        string _Observacion;
        string _Fecha;
        int _idMedico;
        int _idPaciente;
        string _Peso;
        string _Altura;
        String _Presion;
        string _receta;

        public int IdConsulta { get => _idConsulta; set => _idConsulta = value; }
        public string Observacion { get => _Observacion; set => _Observacion = value; }
        public string Fecha { get => _Fecha; set => _Fecha = value; }
        public int IdMedico { get => _idMedico; set => _idMedico = value; }
        public int IdPaciente { get => _idPaciente; set => _idPaciente = value; }
        public string Peso { get => _Peso; set => _Peso = value; }
        public string Altura { get => _Altura; set => _Altura = value; }
        public string Presion { get => _Presion; set => _Presion = value; }
        public string Receta { get => _receta; set => _receta = value; }

        public Boolean Guardar()
        {
            Boolean resultado = false;
            StringBuilder Sentencia = new StringBuilder();
            DataManager.DBOperacion operacion = new DataManager.DBOperacion();
            try
            {
                Sentencia.Append("INSERT INTO `cligar`.`consultas`(`idConsulta`,`Observacion`,`Fecha`,`Receta`,`idMedico`,`idPaciente`,`Peso`,`Altura`,`Presion`) VALUES(null,");
                Sentencia.Append("'" + this.Observacion + "',");
                Sentencia.Append("'" + this.Fecha + "',");
                Sentencia.Append("'" + this.Receta + "',");
                Sentencia.Append("'" + this.IdMedico + "',");
                Sentencia.Append("'" + this.IdPaciente + "',");
                Sentencia.Append("'" + this.Peso + "',");
                Sentencia.Append("'" + this.Altura + "',");
                Sentencia.Append("'" + this.Presion + "');");
               


                if (operacion.Insertar(Sentencia.ToString()) > 0)
                {
                    resultado = true;
                }




            }
            catch (Exception EX)
            {

              

                resultado = false;
            }

            return resultado;
        }
    }
}