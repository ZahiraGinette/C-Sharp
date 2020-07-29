﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

/*Esta clase debe:
 * Ver
 * Agregar
 * Modificar*/

namespace Entidades
{
    public class AccesoDatos
    {
        #region "Atributos"
        private SqlConnection _conexion;
        private SqlCommand _comando;
        //Ejecuta distintos comando en la base de datos, lo vamos a instanciar cada vez qe lo necesitemos
        #endregion

        #region "Constructor"
        public AccesoDatos()
        {
            //Para acceder al SQL
            this._conexion = new SqlConnection(Properties.Settings.Default.Conexion_bn);
        }
        #endregion

        #region "Metodos"
        public List<Persona> TraerTodo()
        {
            List<Persona> lista = new List<Persona>();
            SqlDataReader sqlDataReader;

            try
            {
                this._comando = new SqlCommand();
                this._comando.Connection = this._conexion;
                this._comando.CommandType = CommandType.Text;
                this._comando.CommandText = "SELECT id,nombre,apellido,edad FROM Padron.dbo.Personas";

                this._conexion.Open();
               
                sqlDataReader = this._comando.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    Persona persona = new Persona((int)sqlDataReader["id"], sqlDataReader["nombre"].ToString(), sqlDataReader["apellido"].ToString(), (int)sqlDataReader["edad"]);
                    lista.Add(persona);
                }

                this._conexion.Close();
                //Establece la conexion con nuestra base de datos
            }

            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

            return lista;
        }

        public bool AgregarPersona(Persona persona)
        {
            bool retorno = false;

            this._comando = new SqlCommand();
            this._comando.Connection = this._conexion;
            this._comando.CommandType = CommandType.Text;
            this._comando.CommandText = "INSER INTO Personas nombre,apellido,edad values('" + persona.nombre + "','" + persona.apellido + "','" + persona.edad.ToString() + ")";

            this._conexion.Open();

            if(this._comando.ExecuteNonQuery() > 0)
            {
                retorno = true;
            }

            this._conexion.Close();

            return retorno;
        }
        #endregion

        //DATA READER
        //Solo lectura, solo avance y es el mas rapido que hay
        //Posee propiedades que me permite acceder a las filas
    }
}
