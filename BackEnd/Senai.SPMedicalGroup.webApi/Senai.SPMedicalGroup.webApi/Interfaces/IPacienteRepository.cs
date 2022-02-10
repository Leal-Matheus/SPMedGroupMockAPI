using Senai.SPMedicalGroup.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SPMedicalGroup.webApi.Interfaces
{
    interface IPacienteRepository
    {
        List<Paciente> Listar();

        void Cadastrar(Paciente novoPaciente);

        void Deletar(int idPaciente);
        void Atualizar(int idPaciente, Paciente pacienteAtualizado);

        Paciente BuscarPorId(int idPaciente);
    }
}
