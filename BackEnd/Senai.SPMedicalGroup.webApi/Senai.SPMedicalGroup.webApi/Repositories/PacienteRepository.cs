using Microsoft.EntityFrameworkCore;
using Senai.SPMedicalGroup.webApi.Contexts;
using Senai.SPMedicalGroup.webApi.Domains;
using Senai.SPMedicalGroup.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SPMedicalGroup.webApi.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        SPMedicalGroupContext ctx = new SPMedicalGroupContext();
        public void Atualizar(int idPaciente, Paciente pacienteAtualizado)
        {
            Paciente pacienteBuscado = BuscarPorId(idPaciente);

            if (pacienteAtualizado.Cpf != null || pacienteAtualizado.Rg != null || pacienteAtualizado.Telefone != null || pacienteAtualizado.EnderecoPaciente != null || pacienteAtualizado.DataNasc < DateTime.Now)
            {
                pacienteBuscado.Rg = pacienteBuscado.Rg;
                pacienteBuscado.IdUsuario = pacienteBuscado.IdUsuario;
                pacienteBuscado.Cpf = pacienteBuscado.Cpf;
                pacienteBuscado.Telefone = pacienteAtualizado.Telefone;
                pacienteBuscado.EnderecoPaciente = pacienteAtualizado.EnderecoPaciente;
                pacienteBuscado.DataNasc = pacienteAtualizado.DataNasc;

                ctx.Pacientes.Update(pacienteBuscado);

                ctx.SaveChanges();
            }
        }

        public Paciente BuscarPorId(int idPaciente)
        {
            return ctx.Pacientes.FirstOrDefault(p => p.IdPaciente == idPaciente);
        }

        public void Cadastrar(Paciente novoPaciente)
        {
            ctx.Pacientes.Add(novoPaciente);

            ctx.SaveChanges();
        }

        public void Deletar(int idPaciente)
        {
            ctx.Pacientes.Remove(BuscarPorId(idPaciente));

            ctx.SaveChanges();
        }

        public List<Paciente> Listar()
        {
            return ctx.Pacientes
                        .AsNoTracking()
                        .Select(p => new Paciente()
                        {
                            IdPaciente = p.IdPaciente,
                            Rg = p.Rg,
                            EnderecoPaciente = p.EnderecoPaciente,
                            DataNasc = p.DataNasc,
                            Telefone = p.Telefone,
                            IdUsuarioNavigation = new Usuario()
                            {
                                NomeUsuario = p.IdUsuarioNavigation.NomeUsuario,
                                EmailUsuario = p.IdUsuarioNavigation.EmailUsuario,
                            }
                        })
                        .ToList();
        }
    }
}
