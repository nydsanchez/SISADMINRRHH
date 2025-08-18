using System.Data;
using Datos;

namespace Negocios
{
    public class Neg_TablaIncentivos
    {
        public Neg_TablaIncentivos()
        {
            
        }
        public DataTable SelectAll()
        {
            return new Dato_TablaIncentivo().SelectAll();
        }

        public DataTable SelectByEffic(decimal eficienciadesde, decimal eficienciahasta)
        {
            return new Dato_TablaIncentivo().SelectByEffic(eficienciadesde,eficienciahasta);
        }
        public DataTable SelectByConstruccion(int idconstruccion)
        {
            return new Dato_TablaIncentivo().SelectByConstruccion(idconstruccion);
        }
        public bool Update(int id, int idArea, int idConstIncent, decimal bonoAsist, decimal meta5dias, int idRangoOql, decimal incentivosemanal, decimal eficdesde, decimal efichasta, decimal bonoCalidad)
        {
            int idtablaprecio = 1;

            decimal dzdesde = (meta5dias * (eficdesde/100))/5;
            decimal dzhasta = (meta5dias * (efichasta/100))/5;
            //efic desde
            decimal meta4dias = ((meta5dias * (eficdesde / 100))/5)*4;
            decimal costoDz = incentivosemanal / (meta5dias * (eficdesde / 100));
            meta5dias = (meta5dias * (eficdesde / 100));
            return new Dato_TablaIncentivo().Update(id, idArea, idConstIncent, bonoAsist, dzdesde, dzhasta, idRangoOql, costoDz, eficdesde, efichasta, meta5dias, meta4dias, bonoCalidad, incentivosemanal,idtablaprecio);
        }

        public bool UpdateByEffic(decimal bonoAsist, decimal incentivosemanal, decimal eficdesde, decimal efichasta, decimal bonoCalidad)
        {
            decimal costoDz = incentivosemanal;

            return new Dato_TablaIncentivo().UpdateByEffic(bonoAsist, eficdesde, efichasta, bonoCalidad, incentivosemanal);
        }
        public bool Insert(int idArea, int idConstIncent, decimal bonoAsist, decimal meta5dias, int idRangoOql, decimal incentivosemanal, decimal eficdesde, decimal efichasta, decimal bonoCalidad)
        {
            int idtablaprecio = 1;
            decimal dzdesde = (meta5dias / 5) * (eficdesde / 100);
            decimal dzhasta = (meta5dias / 5) * (efichasta / 100);

            decimal meta4dias = (meta5dias / 5) * 4;
            decimal costoDz = incentivosemanal / meta5dias;
            return new Dato_TablaIncentivo().Insert(idArea, idConstIncent, bonoAsist, dzdesde, dzhasta, idRangoOql, costoDz, eficdesde, efichasta, meta5dias, meta4dias, bonoCalidad, incentivosemanal, idtablaprecio);
        }
        
        public bool InsertByEffic(int idArea, decimal bonoAsist, decimal meta5dias, int idRangoOql, decimal incentivosemanal, decimal eficdesde, decimal efichasta, decimal bonoCalidad)
        {
            int idtablaprecio = 1;
            decimal dzdesde = (meta5dias / 5) * (eficdesde / 100);
            decimal dzhasta = (meta5dias / 5) * (efichasta / 100);

            decimal meta4dias = (meta5dias / 5) * 4;
            decimal costoDz = incentivosemanal / meta5dias;
            return new Dato_TablaIncentivo().InsertByEffic(idArea, bonoAsist, dzdesde, dzhasta, idRangoOql, costoDz, eficdesde, efichasta, meta5dias, meta4dias, bonoCalidad, incentivosemanal, idtablaprecio);
        }

        public bool Delete(int id)
        {
            return new Dato_TablaIncentivo().Delete(id);
        }

        public bool DeleteByEffic(decimal eficdesde, decimal efichasta)
        {
            return new Dato_TablaIncentivo().DeleteByEffic(eficdesde,efichasta);
        }

    }
}
