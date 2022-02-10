using Senai.SPMedicalGroup.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SPMedicalGroup.webApi.Interfaces
{
    interface IInstituicaoRepository
    {
        void CadastrarClinica(Instituicao novaClinica);

        void Atualizar(int id, Instituicao ClinicaAtualizada);

        List<Instituicao> ListarTodas();

        void Deletar(int id);

        Instituicao BuscarClinica(int id);
    }
}
