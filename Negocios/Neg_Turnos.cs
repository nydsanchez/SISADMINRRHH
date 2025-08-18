using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Datos;

namespace Negocios
{
    public class Neg_Turnos
    {
        #region Referencias
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Dato_Turnos Dato_Turnos = new Dato_Turnos();
        #endregion
       public bool AgregarTurno(string nombTurno, string minComodin, int lunes, string horaEntLu, string horaSalLu, string horaComLu,
            int martes, string horaEntMar, string horaSalMar, string horaComMar, int miercoles, string horaEntMie,
            string horaSalMie, string horaComMie, int jueves, string horaEntJue, string horaSalJue, string horaComJue,
            int viernes, string horaEntVie, string horaSalVie, string horaComVie, int sabado, string horaEntSab, string horaSalSab,
            string horaComSab, int domingo, string horaEntDom, string horaSalDom, string horaComDom)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Dato_Turnos.insertarTurno(lunes, horaEntLu, horaSalLu, horaComLu, martes, horaEntMar, horaSalMar, horaComMar, miercoles, horaEntMie,
              horaSalMie, horaComMie, jueves, horaEntJue, horaSalJue, horaComJue, viernes, horaEntVie, horaSalVie, horaComVie, sabado, horaEntSab,
              horaSalSab, horaComSab, domingo, horaEntDom, horaSalDom, horaComDom, nombTurno, minComodin, userDetail.getIDEmpresa());
            try
            {

            }
            catch (SystemException)
            {

                return false;
            }
            return true;
        }

       public bool EditarTurno(string nombTurno, string minComodin, int lunes, string horaEntLu, string horaSalLu, string horaComLu,
            int martes, string horaEntMar, string horaSalMar, string horaComMar, int miercoles, string horaEntMie,
            string horaSalMie, string horaComMie, int jueves, string horaEntJue, string horaSalJue, string horaComJue,
            int viernes, string horaEntVie, string horaSalVie, string horaComVie, int sabado, string horaEntSab, string horaSalSab,
            string horaComSab, int domingo, string horaEntDom, string horaSalDom, string horaComDom, string tempNombTurno)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Dato_Turnos.EditarTurno(lunes, horaEntLu, horaSalLu, horaComLu, martes, horaEntMar, horaSalMar, horaComMar, miercoles, horaEntMie,
              horaSalMie, horaComMie, jueves, horaEntJue, horaSalJue, horaComJue, viernes, horaEntVie, horaSalVie, horaComVie, sabado, horaEntSab,
              horaSalSab, horaComSab, domingo, horaEntDom, horaSalDom, horaComDom, nombTurno, minComodin, tempNombTurno, userDetail.getIDEmpresa());
            try
            {

            }
            catch (SystemException)
            {

                return false;
            }
            return true;
        }

       public bool EliminarTurno(string tempNombTurno)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Dato_Turnos.EliminarTurno(tempNombTurno, userDetail.getIDEmpresa());
            try
            {

            }
            catch (SystemException)
            {

                return false;
            }
            return true;
        }

       public DataSet turnos()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Turnos.obtenerTurnos(userDetail.getIDEmpresa());
            return ds;
        }
        //AGREGADO POR WBRAVO
        public dsPlanilla.dtTurnoDiaDataTable Todos(int userDetail)
        {
            //IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return Dato_Turnos.Todos(userDetail);
        }
        //AGREGADOS POR WMEMBRENO
        public DataTable obtenerTodosTurnos()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = Dato_Turnos.obtenerTodosTurnos(userDetail.getIDEmpresa());
            return ds;
        }

        public bool TurnosInsert(string nombre, decimal hrsturno,int bonus, int tipo, int horasseptimo,bool consolidar)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Boolean hecho;
            hecho = Dato_Turnos.TurnosInsert(userDetail.getIDEmpresa(), nombre,hrsturno, bonus, tipo, horasseptimo,consolidar);
            return hecho;
        }

        public bool PlnTurnosUpdate(string nombre,decimal hrsturno, int id, int bonus, int tipo, int horasseptimo,bool consolidar)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Boolean hecho;
            hecho = Dato_Turnos.PlnTurnosUpdate(userDetail.getIDEmpresa(), id, nombre,hrsturno, bonus, tipo, horasseptimo,consolidar);
            return hecho;
        }

        public bool PlnTurnosDelete(int id)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Boolean hecho;
            hecho = Dato_Turnos.PlnTurnosDelete(userDetail.getIDEmpresa(), id);
            return hecho;
        }

        public bool PlnTurnosDiasinsert(int id, int diasemana, DateTime horaIni, DateTime HoraFin, int almuerzo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Boolean hecho;
            hecho = Dato_Turnos.PlnTurnosDiasinsert(userDetail.getIDEmpresa(), id, diasemana, horaIni, HoraFin, almuerzo);
            return hecho;
        }

        public bool PlnTurnosDiasUpdate(int id, int diasemana, DateTime horaIni, DateTime HoraFin, int almuerzo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Boolean hecho;
            hecho = Dato_Turnos.PlnTurnosDiasUpdate(userDetail.getIDEmpresa(), id, diasemana, horaIni, HoraFin, almuerzo);
            return hecho;
        }

        public bool PlnTurnosDiasDelete(int id, int diasemana)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Boolean hecho;
            hecho = Dato_Turnos.PlnTurnosDiasDelete(userDetail.getIDEmpresa(), id, diasemana);
            return hecho;
        }


        public bool PlnTurnosDiasDeleteAll(int id)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Boolean hecho;
            hecho = Dato_Turnos.PlnTurnosDiasDeleteAll(userDetail.getIDEmpresa(), id);
            return hecho;
        }

        public DataTable PlnTurnosDiasSelect(int id)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = Dato_Turnos.PlnTurnosDiasSelect(userDetail.getIDEmpresa(), id);
            return ds;
        }
        public bool validarhora(string horaIP, string horaFP)
        {
            string horaI = horaIP;
            string horaF = horaFP;
            // Split string on spaces.
            // ... This will separate all the words.
            string[] AHoraI = horaI.Split(':');
            string[] AHoraF = horaF.Split(':');
            int c = 0;
            if (AHoraI.Length < 2 || AHoraF.Length < 2 || AHoraI.Length > 3 || AHoraF.Length > 3)
            {
                c = c + 1;
            }
            else
            {
                if (int.Parse(AHoraI[0]) < 0 || int.Parse(AHoraI[0]) > 23 || int.Parse(AHoraF[0]) < 0 || int.Parse(AHoraF[0]) > 23)
                {
                    c = c + 1;
                }

                if (int.Parse(AHoraI[1]) < 0 || int.Parse(AHoraI[1]) > 59 || int.Parse(AHoraF[1]) < 0 || int.Parse(AHoraF[1]) > 59)
                {
                    c = c + 1;

                }
            }

            if (c > 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public bool ExisteDia(int id, int dia)
        {
            DataTable dtv = new DataTable();
            dtv = PlnTurnosDiasSelect(id);
            if (dtv != null)
            {
                DataView dtvw = dtv.DefaultView;
                dtvw.RowFilter = string.Empty;
                dtvw.RowFilter = "diasemana ='" + dia + "'";
                if (dtvw.Count > 0)
                { return false; }
                else
                { return true; }

            }
            else
            { return true; }
        }
    }
}
