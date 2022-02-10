using Senai.SPMedicalGroup.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SPMedicalGroup.webApi.Interfaces
{
    interface IConsultaRepository
    {
        List<Consultum> ListarMinhasConsultas(int id, int idTipo);
        List<Consultum> ListarTodas();

        void CancelarConsulta(int Id);

        void CadastrarConsulta(Consultum novaConsulta);

        void AlterarDescricao(string descricao, int id);

        void RemoverConsulta(int id);


        Consultum BuscarPorId(int id);
    }
}
