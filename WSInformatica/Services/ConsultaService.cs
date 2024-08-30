using WSInformatica.Models;
using WSInformatica.Models.Request;

namespace WSInformatica.Services
{
    public class ConsultaService : IConsultaService
    {
        public void Add(ConsultaRequest model)
        {
            int flag = 0;
            using (InfoContext db = new InfoContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var consulta = new Consulta();
                        consulta.Id = model.IdConsulta;
                        consulta.IdDespachante = model.IdDespachante;
                        consulta.IdSolicitante = model.IdSolicitante;
                        consulta.Movil = model.Movil;
                        consulta.Lugar = model.Lugar;
                        consulta.Idjuridiccion = model.IdJuridiccion;
                        consulta.Fecha = DateTime.Now;

                        db.Consulta.Add(consulta);
                        db.SaveChanges();

                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw new Exception("Ocurrio un error en insercion");
                    }
                }
            }
        }
    }
}
