using Senai.SPMedicalGroup.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SPMedicalGroup.webApi.Interfaces
{
    interface IMedicoRepository
    {
        List<Medico> Listar();


        void Cadastrar(Medico novoMedico);
    }
}
