using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace Datos
{
    public class Dato_Empresas
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion
       public dsPlanilla.dtEmpresaDataTable ObtenerDetalleEmpresas( int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            dsPlanilla.dtEmpresaDataTable empresa = new dsPlanilla.dtEmpresaDataTable();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            //System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@nombEmpresa", System.Data.SqlDbType.VarChar);
            //p1.Value = nombEmpresa;
            //cmd.Parameters.Add(p1);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerDetalleEmpresa";
            cmd.Connection = sqlConnection;
            
            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(empresa);

            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return empresa;
        }

       public DataSet SeleccionarTipoNomina(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerTipoNomina";
            cmd.Connection = sqlConnection;
           
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "nomina");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }

       public bool EditarEmpresa(string nombEmpAnt, string nombEmpresa, int pais, string idioma,
            int tipoNomina, decimal salarMin, decimal PorcSEmple, decimal PorcSEmpresa, decimal PorcEdcEmp,
            decimal SalarMaxSS, decimal MaxSSEmp, decimal MaxSSEmpr, decimal MinS4Sempl, decimal MinS5Sem,
            decimal ValorS4Sem, decimal ValorS5Sem, decimal MinS4Sempresa, decimal MinS5Sempresa, decimal ValorSS4Semprs,
            decimal ValorSS5Semprs, decimal FactCamb, string GerRrhh, string user, bool pagaAntig, bool RedondearPago, bool PromVac,int moneda, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);

            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DetalleEmpresaEditar";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@nombEmpAnt", System.Data.SqlDbType.VarChar);
            p1.Value = nombEmpAnt;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@nombEmpresa", System.Data.SqlDbType.VarChar);
            p2.Value = nombEmpresa;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@pais", System.Data.SqlDbType.Int);
            p3.Value = pais;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@idioma", System.Data.SqlDbType.VarChar);
            p4.Value = idioma;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@tipoNomina", System.Data.SqlDbType.Int);
            p5.Value = tipoNomina;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@salarMin", System.Data.SqlDbType.Decimal);
            p6.Value = salarMin;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@PorcSEmple", System.Data.SqlDbType.Decimal);
            p7.Value = PorcSEmple;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@PorcSEmpresa", System.Data.SqlDbType.Decimal);
            p8.Value = PorcSEmpresa;
            cmd.Parameters.Add(p8);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@PorcEdcEmp", System.Data.SqlDbType.Decimal);
            p9.Value = PorcEdcEmp;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@SalarMaxSS", System.Data.SqlDbType.Decimal);
            p10.Value = SalarMaxSS;
            cmd.Parameters.Add(p10);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@MaxSSEmp", System.Data.SqlDbType.Decimal);
            p11.Value = MaxSSEmp;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@MaxSSEmpr", System.Data.SqlDbType.Decimal);
            p12.Value = MaxSSEmpr;
            cmd.Parameters.Add(p12);
            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@MinS4Sempl", System.Data.SqlDbType.Decimal);
            p13.Value = MinS4Sempl;
            cmd.Parameters.Add(p13);
            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@MinS5Sem", System.Data.SqlDbType.Decimal);
            p14.Value = MinS5Sem;
            cmd.Parameters.Add(p14);
            System.Data.SqlClient.SqlParameter p15 = new SqlParameter("@ValorS4Sem", System.Data.SqlDbType.Decimal);
            p15.Value = ValorS4Sem;
            cmd.Parameters.Add(p15);
            System.Data.SqlClient.SqlParameter p16 = new SqlParameter("@ValorS5Sem", System.Data.SqlDbType.Decimal);
            p16.Value = ValorS5Sem;
            cmd.Parameters.Add(p16);
            System.Data.SqlClient.SqlParameter p17 = new SqlParameter("@MinS4Sempresa", System.Data.SqlDbType.Decimal);
            p17.Value = MinS4Sempresa;
            cmd.Parameters.Add(p17);
            System.Data.SqlClient.SqlParameter p18 = new SqlParameter("@MinS5Sempresa", System.Data.SqlDbType.Decimal);
            p18.Value = MinS5Sempresa;
            cmd.Parameters.Add(p18);
            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@ValorSS4Semprs", System.Data.SqlDbType.Decimal);
            p19.Value = ValorSS4Semprs;
            cmd.Parameters.Add(p19);
            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@ValorSS5Semprs", System.Data.SqlDbType.Decimal);
            p20.Value = ValorSS5Semprs;
            cmd.Parameters.Add(p20);
            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@FactCamb", System.Data.SqlDbType.Decimal);
            p21.Value = FactCamb;
            cmd.Parameters.Add(p21);
            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@GerRrhh", System.Data.SqlDbType.VarChar);
            p22.Value = GerRrhh;
            cmd.Parameters.Add(p22);
            System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@user", System.Data.SqlDbType.VarChar);
            p23.Value = user;
            cmd.Parameters.Add(p23);
            ///NUEVOS PARAMETROS
            System.Data.SqlClient.SqlParameter p24 = new SqlParameter("@pagaAntig", System.Data.SqlDbType.Bit);
            p24.Value = pagaAntig;
            cmd.Parameters.Add(p24);
            System.Data.SqlClient.SqlParameter p25 = new SqlParameter("@RedondearPago", System.Data.SqlDbType.Bit);
            p25.Value = RedondearPago;
            cmd.Parameters.Add(p25);
            System.Data.SqlClient.SqlParameter p26 = new SqlParameter("@PromVac", System.Data.SqlDbType.Bit);
            p26.Value = PromVac;
            cmd.Parameters.Add(p26);
            System.Data.SqlClient.SqlParameter p27 = new SqlParameter("@moneda", System.Data.SqlDbType.Int);
            p27.Value = moneda;
            cmd.Parameters.Add(p27);

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
       public bool AgregarEmpresa(int idemp,string nombEmpresa, int pais, 
            int tipoNomina, decimal salarMin,  string GerRrhh, string user, bool PromVac,int moneda, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);

            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DetalleEmpresaAgregar";
            cmd.Connection = sqlConnection;
            
            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@idemp", System.Data.SqlDbType.Int);
            p1.Value = idemp;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@nombEmpresa", System.Data.SqlDbType.VarChar);
            p2.Value = nombEmpresa;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@pais", System.Data.SqlDbType.Int);
            p3.Value = pais;
            cmd.Parameters.Add(p3);
            //System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@idioma", System.Data.SqlDbType.VarChar);
            //p4.Value = idioma;
            //cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@tipoNomina", System.Data.SqlDbType.Int);
            p5.Value = tipoNomina;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@salarMin", System.Data.SqlDbType.Decimal);
            p6.Value = salarMin;
            cmd.Parameters.Add(p6);
            //System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@PorcSEmple", System.Data.SqlDbType.Decimal);
            //p7.Value = PorcSEmple;
            //cmd.Parameters.Add(p7);
            //System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@PorcSEmpresa", System.Data.SqlDbType.Decimal);
            //p8.Value = PorcSEmpresa;
            //cmd.Parameters.Add(p8);
            //System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@PorcEdcEmp", System.Data.SqlDbType.Decimal);
            //p9.Value = PorcEdcEmp;
            //cmd.Parameters.Add(p9);
            //System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@SalarMaxSS", System.Data.SqlDbType.Decimal);
            //p10.Value = SalarMaxSS;
            //cmd.Parameters.Add(p10);
            //System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@MaxSSEmp", System.Data.SqlDbType.Decimal);
            //p11.Value = MaxSSEmp;
            //cmd.Parameters.Add(p11);
            //System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@MaxSSEmpr", System.Data.SqlDbType.Decimal);
            //p12.Value = MaxSSEmpr;
            //cmd.Parameters.Add(p12);
            //System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@MinS4Sempl", System.Data.SqlDbType.Decimal);
            //p13.Value = MinS4Sempl;
            //cmd.Parameters.Add(p13);
            //System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@MinS5Sem", System.Data.SqlDbType.Decimal);
            //p14.Value = MinS5Sem;
            //cmd.Parameters.Add(p14);
            //System.Data.SqlClient.SqlParameter p15 = new SqlParameter("@ValorS4Sem", System.Data.SqlDbType.Decimal);
            //p15.Value = ValorS4Sem;
            //cmd.Parameters.Add(p15);
            //System.Data.SqlClient.SqlParameter p16 = new SqlParameter("@ValorS5Sem", System.Data.SqlDbType.Decimal);
            //p16.Value = ValorS5Sem;
            //cmd.Parameters.Add(p16);
            //System.Data.SqlClient.SqlParameter p17 = new SqlParameter("@MinS4Sempresa", System.Data.SqlDbType.Decimal);
            //p17.Value = MinS4Sempresa;
            //cmd.Parameters.Add(p17);
            //System.Data.SqlClient.SqlParameter p18 = new SqlParameter("@MinS5Sempresa", System.Data.SqlDbType.Decimal);
            //p18.Value = MinS5Sempresa;
            //cmd.Parameters.Add(p18);
            //System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@ValorSS4Semprs", System.Data.SqlDbType.Decimal);
            //p19.Value = ValorSS4Semprs;
            //cmd.Parameters.Add(p19);
            //System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@ValorSS5Semprs", System.Data.SqlDbType.Decimal);
            //p20.Value = ValorSS5Semprs;
            //cmd.Parameters.Add(p20);
            //System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@FactCamb", System.Data.SqlDbType.Decimal);
            //p21.Value = FactCamb;
            //cmd.Parameters.Add(p21);
            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@GerRrhh", System.Data.SqlDbType.VarChar);
            p22.Value = GerRrhh;
            cmd.Parameters.Add(p22);
            System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@user", System.Data.SqlDbType.VarChar);
            p23.Value = user;
            cmd.Parameters.Add(p23);
            //nuevos parametros
            //System.Data.SqlClient.SqlParameter p24 = new SqlParameter("@pagaAntig", System.Data.SqlDbType.Bit);
            //p24.Value = pagaAntig;
            //cmd.Parameters.Add(p24);
            //System.Data.SqlClient.SqlParameter p25 = new SqlParameter("@RedondearPago", System.Data.SqlDbType.Bit);
            //p25.Value = RedondearPago;
            //cmd.Parameters.Add(p25);
            System.Data.SqlClient.SqlParameter p26 = new SqlParameter("@PromVac", System.Data.SqlDbType.Bit);
            p26.Value = PromVac;
            cmd.Parameters.Add(p26);
            System.Data.SqlClient.SqlParameter p27 = new SqlParameter("@moneda", System.Data.SqlDbType.Int);
            p27.Value = moneda;
            cmd.Parameters.Add(p27);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;

        }

       public bool EliminarEmpresa(string nombEmpresa, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);

            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EmpresasEliminar";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@nombEmpresa", System.Data.SqlDbType.VarChar);
            p2.Value = nombEmpresa;
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
    }
}
