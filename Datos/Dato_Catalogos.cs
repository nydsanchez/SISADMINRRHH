using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;


namespace Datos
{
    public class Dato_Catalogos
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion
       public DataSet SeleccionarPaises(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);

            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerPaises";
            cmd.Connection = sqlConnection;
           

            try
            {               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "paises");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

       public DataSet SeleccionarDepartamentos(int idpadre,int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);

            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerDepartamentos";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@idpadre", System.Data.SqlDbType.Int);
            p1.Value = idpadre;
            cmd.Parameters.Add(p1);


            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "departamentos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataTable PlnObtenerDeptosSuburdinados(int codigo_empleado, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);

            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnObtenerDeptosSuburdinados";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo_empleado", System.Data.SqlDbType.Int);
            p1.Value = codigo_empleado;
            cmd.Parameters.Add(p1);


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet SeleccionarMunicipios(int idpadre,int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerMunicipios";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@idpadre", System.Data.SqlDbType.Int);
            p1.Value = idpadre;
            cmd.Parameters.Add(p1);

            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "municipios");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }

       public DataSet SeleccionarOperaciones(int codigo_cargo,int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo_cargo", System.Data.SqlDbType.Int);
            p1.Value = codigo_cargo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerOperaciones";
            cmd.Connection = sqlConnection;
            

            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "operaciones");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

       public DataSet SeleccionarNivelAcademico(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerNivelAcademico";
            cmd.Connection = sqlConnection;
            

            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "nivelacademico");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

       public DataSet SeleccionarUbicaciones(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerUbicaciones";
            cmd.Connection = sqlConnection;
            
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "ubicaciones");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }

       public DataSet SeleccionarEmpresas(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerEmpresas";
            cmd.Connection = sqlConnection;
           
           try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "empresas");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }
       public DataSet SeleccionarProcesos(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerProcesos";
            cmd.Connection = sqlConnection;
            

            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "procesos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }


            return ds;
        }

        public DataTable ObtenerTipoContratosActivo(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnObtenerTipoContratosActivoSel";
            cmd.Connection = sqlConnection;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
            }
            return ds;
        }
        public DataSet SeleccionarMonedas(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerMonedas";
            cmd.Connection = sqlConnection;
            

            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "monedas");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

       public DataSet SeleccionarEstadoCivil(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerEstadoCivil";
            cmd.Connection = sqlConnection;
            
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "estado");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }

       public DataSet SeleccionarTipoCasa(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerTipoCasa";
            cmd.Connection = sqlConnection;
           
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "casa");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }

       public DataSet SeleccionarEstadoEmpleado(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerEstadoEmpleado";
            cmd.Connection = sqlConnection;
            
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "estado");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }

       public DataSet SeleccionarTipoContrato(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerTipoContrato";
            cmd.Connection = sqlConnection;
            

            try
            {
              
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "tipo");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

       public DataSet SeleccionarTipoSalario(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerTipoSalario";
            cmd.Connection = sqlConnection;
            

            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "tipo");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

       public DataSet SeleccionarTurno(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerTurno";
            cmd.Connection = sqlConnection;
            
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "turno");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }

       public DataSet SeleccionarCargo(int codigo_depto,int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerCargo";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo_depto", System.Data.SqlDbType.Int);
            p1.Value = codigo_depto;
            cmd.Parameters.Add(p1);

            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "cargo");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }

       public DataSet SeleccionarGrupos(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerGruposSel";
            cmd.Connection = sqlConnection;
            
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "grupos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }

       public DataSet seleccionarGruposCatalogos(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GruposCatalogosSel";
            cmd.Connection = sqlConnection;
            

            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "grupos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

       public byte[] SeleccionarFoto(int codEmple, int idEmpresa)
        {

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            byte[] byteArray = new byte[0];
            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmple", System.Data.SqlDbType.Int);
            p1.Value = codEmple;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Obtenerfoto";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
            try
            {
                da.Fill(ds, "fotoempleado");
                byte[] Datos = new byte[0];
                DataRow DR = ds.Tables["fotoempleado"].Rows[0];
                Datos = (byte[])DR["fotoempleado"];
                byteArray = Datos;
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                
            }
            return byteArray;
           
        }

       public bool InsertarAGrupo(int idGrupo, string descripcion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);

            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AgregarAGrupoIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@idGrupo", System.Data.SqlDbType.Int);
            p1.Value = idGrupo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@descripcion", System.Data.SqlDbType.VarChar);
            p2.Value = descripcion;
            cmd.Parameters.Add(p2);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;
        }

       public bool EditarGrupo(int idGrupo, int antGrupo, string descripcion, string antDescrip, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GrupoCatalogosEdit";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@idGrupo", System.Data.SqlDbType.Int);
            p1.Value = idGrupo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@antGrupo", System.Data.SqlDbType.Int);
            p2.Value = antGrupo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@descripcion", System.Data.SqlDbType.VarChar);
            p3.Value = descripcion;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@antDescrip", System.Data.SqlDbType.VarChar);
            p4.Value = antDescrip;
            cmd.Parameters.Add(p4);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;
        }

       public bool EliminarGrupo(int antGrupo, string antDescrip, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GrupoCatalogosElim";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@antGrupo", System.Data.SqlDbType.Int);
            p2.Value = antGrupo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@antDescrip", System.Data.SqlDbType.VarChar);
            p4.Value = antDescrip;
            cmd.Parameters.Add(p4);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;
        }

       public DataSet SeleccionarMeses(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "mesesSel";
            cmd.Connection = sqlConnection;
            
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "meses");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }

       public bool EditarFactor(decimal factor, bool activo, int id, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EditarFactor";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@factor", System.Data.SqlDbType.Decimal);
            p1.Value = factor;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@activo", System.Data.SqlDbType.Bit);
            p2.Value = activo;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p3.Value = id;
            cmd.Parameters.Add(p3);


            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;
        }

       public DataSet cargarTipoPermisos(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "cargarTipoPermisos";
            cmd.Connection = sqlConnection;
            
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "permisos");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

       public DataTable selecionarUbicaciones(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "seleccionarUbicaciones";
            cmd.Connection = sqlConnection;
            

            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "ubicaciones");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }
        public DataTable seleccionarUbicacionesxCod(int codigo_ubicacion,int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "seleccionarUbicacionesxCod";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codigo_ubicacion", System.Data.SqlDbType.Int);
            p3.Value = codigo_ubicacion;
            cmd.Parameters.Add(p3);

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "ubicaciones");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }
        public DataTable selecionarDepartamentos(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "seleccionarDepartamentos";
            cmd.Connection = sqlConnection;
            
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "departamentos");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds.Tables[0];
        }

       public bool agregarUbicaciones(int idEmp, string ubicacion, string telefono, string nPatronal, string nRuc,
            string departamento, string municipio, string direccion,int tplanilla, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "agregarUbicaciones";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@idEmpresa", System.Data.SqlDbType.Int);
            p1.Value = idEmp;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@ubicacion", System.Data.SqlDbType.VarChar);
            p2.Value = ubicacion;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@telefono", System.Data.SqlDbType.VarChar);
            p3.Value = telefono;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@nPatronal", System.Data.SqlDbType.VarChar);
            p4.Value = nPatronal;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@nRuc", System.Data.SqlDbType.VarChar);
            p5.Value = nRuc;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@departamento", System.Data.SqlDbType.VarChar);
            p6.Value = departamento;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@municipio", System.Data.SqlDbType.VarChar);
            p7.Value = municipio;
            cmd.Parameters.Add(p7);

            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@direccion", System.Data.SqlDbType.VarChar);
            p8.Value = direccion;
            cmd.Parameters.Add(p8);

            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@tplanilla", System.Data.SqlDbType.Int);
            p11.Value = tplanilla;
            cmd.Parameters.Add(p11);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;
            
        }

       public bool editarUbicaciones(int idEmp, string ubicacion, string telefono, string nPatronal, string nRuc,
            string departamento, string municipio, string direccion, int idUbicacion,int tplanilla, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "editarUbicaciones";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@idEmpresa", System.Data.SqlDbType.Int);
            p1.Value = idEmp;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@ubicacion", System.Data.SqlDbType.VarChar);
            p2.Value = ubicacion;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@telefono", System.Data.SqlDbType.VarChar);
            p3.Value = telefono;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@nPatronal", System.Data.SqlDbType.VarChar);
            p4.Value = nPatronal;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@nRuc", System.Data.SqlDbType.VarChar);
            p5.Value = nRuc;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@departamento", System.Data.SqlDbType.VarChar);
            p6.Value = departamento;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@municipio", System.Data.SqlDbType.VarChar);
            p7.Value = municipio;
            cmd.Parameters.Add(p7);

            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@direccion", System.Data.SqlDbType.VarChar);
            p8.Value = direccion;
            cmd.Parameters.Add(p8);

            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@idUbicacion", System.Data.SqlDbType.Int);
            p9.Value = idUbicacion;
            cmd.Parameters.Add(p9);

            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@tplanilla", System.Data.SqlDbType.Int);
            p11.Value = tplanilla;
            cmd.Parameters.Add(p11);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;
        }

       public bool PlnDepartamentosIns(string departamento, int centroCosto, bool directo,int idpadre, int idjefe,string jefe,int omitirpl, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnDepartamentosIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@departamento", System.Data.SqlDbType.VarChar);
            p1.Value = departamento;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@centroCosto", System.Data.SqlDbType.Int);
            p2.Value = centroCosto;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@directo", System.Data.SqlDbType.Bit);
            p3.Value = directo;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@idpadre", System.Data.SqlDbType.Int);
            p5.Value = idpadre;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@idjefe", System.Data.SqlDbType.Int);
            p6.Value = idjefe;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@jefe", System.Data.SqlDbType.Char);
            p7.Value = jefe;
            cmd.Parameters.Add(p7);

            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@omitirpl", System.Data.SqlDbType.Int);
            p8.Value = omitirpl;
            cmd.Parameters.Add(p8);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;
        }

       public bool PlnDepartamentosUpd(string departamento, int centroCosto, bool directo, int codigoDepto,int idpadre,int idjefe,string jefe,int omitirpl,bool activo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnDepartamentosUpd";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@departamento", System.Data.SqlDbType.VarChar);
            p1.Value = departamento;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@centroCosto", System.Data.SqlDbType.Int);
            p2.Value = centroCosto;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@directo", System.Data.SqlDbType.Bit);
            p3.Value = directo;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@idpadre", System.Data.SqlDbType.Int);
            p5.Value = idpadre;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@idjefe", System.Data.SqlDbType.Int);
            p6.Value = idjefe;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@jefe", System.Data.SqlDbType.Char);
            p7.Value = jefe;
            cmd.Parameters.Add(p7);

            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@omitirpl", System.Data.SqlDbType.Int);
            p8.Value = omitirpl;
            cmd.Parameters.Add(p8);

            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@codigoDepto", System.Data.SqlDbType.Int);
            p9.Value = codigoDepto;
            cmd.Parameters.Add(p9);

            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@activo", System.Data.SqlDbType.Bit);
            p10.Value = activo;
            cmd.Parameters.Add(p10);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;
        }
        public DataSet seleccionarCatalogosxGrupo(int grupo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@grupo", System.Data.SqlDbType.Int);
            p1.Value = grupo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CatalogosxGrupoSel";
            cmd.Connection = sqlConnection;


            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "grupos");
            }
            catch (Exception)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                
            }
            return ds;
        }

        public DataTable PlnObtenerRubrosViativo(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnObtenerRubrosViativo";
            cmd.Connection = sqlConnection;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception)
            {
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
            }
            return ds;
        }
    }
}
