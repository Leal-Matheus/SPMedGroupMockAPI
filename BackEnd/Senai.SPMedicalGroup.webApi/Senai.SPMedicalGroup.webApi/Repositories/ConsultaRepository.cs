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
    public class ConsultaRepository : IConsultaRepository
    {
        SPMedicalGroupContext ctx = new SPMedicalGroupContext();
        public void AlterarDescricao(string descricao, int id)
        {
            Consultum consultaBuscada = BuscarPorId(id);

            if (descricao != null)
            {
                consultaBuscada.DescricaoConsulta = descricao;

                ctx.Consulta.Update(consultaBuscada);

                ctx.SaveChanges();
            };
        }

        public Consultum BuscarPorId(int id)
        {
            return ctx.Consulta.FirstOrDefault(u => u.IdConsulta == id);
        }

        public void CadastrarConsulta(Consultum novaConsulta)
        {
            novaConsulta.DescricaoConsulta = "";
            novaConsulta.IdSituacao = 2;

            ctx.Consulta.Add(novaConsulta);

            ctx.SaveChanges();
        }

        public void CancelarConsulta(int Id)
        {
            Consultum consultaBuscada = BuscarPorId(Id);

            consultaBuscada.IdSituacao = 3;
            consultaBuscada.DescricaoConsulta = "Consulta Cancelada";

            ctx.Consulta.Update(consultaBuscada);
            ctx.SaveChanges();
        }

        public List<Consultum> ListarMinhasConsultas(int id, int idTipo)
        {
            if (idTipo == 2)
            {
                Medico medico = ctx.Medicos.FirstOrDefault(u => u.IdUsuario == id);

                int idMedico = medico.IdMedico;

                return ctx.Consulta
                                .Where(c => c.IdMedico == idMedico)
                                .AsNoTracking()
                                .Select(p => new Consultum()
                                {
                                    DataConsulta = p.DataConsulta,
                                    IdConsulta = p.IdConsulta,
                                    DescricaoConsulta = p.DescricaoConsulta,
                                    IdMedicoNavigation = new Medico()
                                    {
                                        Crm = p.IdMedicoNavigation.Crm,
                                        IdUsuarioNavigation = new Usuario()
                                        {
                                            NomeUsuario = p.IdMedicoNavigation.IdUsuarioNavigation.NomeUsuario,
                                            EmailUsuario = p.IdMedicoNavigation.IdUsuarioNavigation.EmailUsuario
                                        },
                                        IdInstituicaoNavigation = new Instituicao()
                                        {
                                            NomeFantasia = p.IdMedicoNavigation.IdInstituicaoNavigation.NomeFantasia
                                        }
                                    },
                                    IdPacienteNavigation = new Paciente()
                                    {
                                        Cpf = p.IdPacienteNavigation.Cpf,
                                        Telefone = p.IdPacienteNavigation.Telefone,
                                        IdUsuarioNavigation = new Usuario()
                                        {
                                            NomeUsuario = p.IdPacienteNavigation.IdUsuarioNavigation.NomeUsuario,
                                            EmailUsuario = p.IdPacienteNavigation.IdUsuarioNavigation.EmailUsuario
                                        }
                                    },
                                    IdSituacaoNavigation = new Situacao()
                                    {
                                        Descricao = p.IdSituacaoNavigation.Descricao
                                    }


                                })
                                .ToList();
            }
            else if (idTipo == 3)
            {
                Paciente paciente = ctx.Pacientes.FirstOrDefault(u => u.IdUsuario == id);

                int idPaciente = paciente.IdPaciente;
                return ctx.Consulta
                                .Where(c => c.IdPaciente == idPaciente)
                                .AsNoTracking()
                                .Select(p => new Consultum()
                                {
                                    DataConsulta = p.DataConsulta,
                                    IdConsulta = p.IdConsulta,
                                    IdMedicoNavigation = new Medico()
                                    {
                                        Crm = p.IdMedicoNavigation.Crm,
                                        IdUsuarioNavigation = new Usuario()
                                        {
                                            NomeUsuario = p.IdMedicoNavigation.IdUsuarioNavigation.NomeUsuario,
                                            EmailUsuario = p.IdMedicoNavigation.IdUsuarioNavigation.EmailUsuario,

                                        },
                                        IdInstituicaoNavigation = new Instituicao()
                                        {
                                            NomeFantasia = p.IdMedicoNavigation.IdInstituicaoNavigation.NomeFantasia
                                        }
                                    },
                                    IdPacienteNavigation = new Paciente()
                                    {
                                        Cpf = p.IdPacienteNavigation.Cpf,
                                        Telefone = p.IdPacienteNavigation.Telefone,
                                        IdUsuarioNavigation = new Usuario()
                                        {
                                            NomeUsuario = p.IdPacienteNavigation.IdUsuarioNavigation.NomeUsuario,
                                            EmailUsuario = p.IdPacienteNavigation.IdUsuarioNavigation.EmailUsuario
                                        }
                                    },
                                    IdSituacaoNavigation = new Situacao()
                                    {
                                        Descricao = p.IdSituacaoNavigation.Descricao
                                    }



                                })
                                .ToList();
            }

            else
            {

                return null;
            }
        }

        public List<Consultum> ListarTodas()
        {
            return ctx.Consulta
            .Select(p => new Consultum()
            {
                DataConsulta = p.DataConsulta,
                IdConsulta = p.IdConsulta,
                DescricaoConsulta = p.DescricaoConsulta,
                IdMedicoNavigation = new Medico()
                {
                    Crm = p.IdMedicoNavigation.Crm,
                    IdUsuarioNavigation = new Usuario()
                    {
                        NomeUsuario = p.IdMedicoNavigation.IdUsuarioNavigation.NomeUsuario,
                        EmailUsuario = p.IdMedicoNavigation.IdUsuarioNavigation.EmailUsuario
                    },
                    IdInstituicaoNavigation = new Instituicao()
                    {
                        NomeFantasia = p.IdMedicoNavigation.IdInstituicaoNavigation.NomeFantasia
                    }
                },
                IdPacienteNavigation = new Paciente()
                {
                    Cpf = p.IdPacienteNavigation.Cpf,
                    Telefone = p.IdPacienteNavigation.Telefone,
                    IdUsuarioNavigation = new Usuario()
                    {
                        NomeUsuario = p.IdPacienteNavigation.IdUsuarioNavigation.NomeUsuario,
                        EmailUsuario = p.IdPacienteNavigation.IdUsuarioNavigation.EmailUsuario
                    }
                },
                IdSituacaoNavigation = new Situacao()
                {
                    Descricao = p.IdSituacaoNavigation.Descricao
                }



            })
            .ToList();
        }

        public void RemoverConsulta(int id)
        {
            ctx.Consulta.Remove(BuscarPorId(id));
            ctx.SaveChanges();
        }
    }
}
