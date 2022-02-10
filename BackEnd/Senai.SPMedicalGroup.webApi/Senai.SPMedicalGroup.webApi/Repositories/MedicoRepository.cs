using Senai.SPMedicalGroup.webApi.Contexts;
using Senai.SPMedicalGroup.webApi.Domains;
using Senai.SPMedicalGroup.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SPMedicalGroup.webApi.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        SPMedicalGroupContext ctx = new SPMedicalGroupContext();

        public void Cadastrar(Medico novoMedico)
        {
            ctx.Medicos.Add(novoMedico);

            ctx.SaveChanges();
        }

        public List<Medico> Listar()
        {
            return ctx.Medicos.Select(u => new Medico()
            {
                IdUsuario = u.IdUsuario,
                IdMedico = u.IdMedico,
                IdUsuarioNavigation = new Usuario()
                {
                    NomeUsuario = u.IdUsuarioNavigation.NomeUsuario
                }
            }).ToList();
        }
    }
}
