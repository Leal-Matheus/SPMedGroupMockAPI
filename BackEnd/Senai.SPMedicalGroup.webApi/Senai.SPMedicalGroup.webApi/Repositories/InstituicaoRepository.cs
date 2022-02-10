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
    public class InstituicaoRepository : IInstituicaoRepository
    {
        SPMedicalGroupContext ctx = new SPMedicalGroupContext();

        public void Atualizar(int id, Instituicao ClinicaAtualizada)
        {
            Instituicao clinicaBuscada = BuscarClinica(id);
            if (ClinicaAtualizada.Endereco != null || ClinicaAtualizada.Cnpj != null || ClinicaAtualizada.NomeFantasia != null || ClinicaAtualizada.RazaoSocial != null)
            {
                clinicaBuscada.Endereco = ClinicaAtualizada.Endereco;
                clinicaBuscada.Cnpj = ClinicaAtualizada.Cnpj;
                clinicaBuscada.NomeFantasia = ClinicaAtualizada.NomeFantasia;
                clinicaBuscada.RazaoSocial = ClinicaAtualizada.RazaoSocial;

                ctx.Instituicaos.Update(clinicaBuscada);

                ctx.SaveChanges();
            }
        }

        public Instituicao BuscarClinica(int id)
        {
            return ctx.Instituicaos.FirstOrDefault(c => c.IdInstituicao == id);
        }

        public void CadastrarClinica(Instituicao novaClinica)
        {
            ctx.Instituicaos.Add(novaClinica);

            ctx.SaveChanges();
        }

        public void Deletar(int id)
        {
            ctx.Instituicaos.Remove(BuscarClinica(id));

            ctx.SaveChanges();
        }

        public List<Instituicao> ListarTodas()
        {
            return ctx.Instituicaos
                    .AsNoTracking()
                    .Select(c => new Instituicao
                    {
                        Cnpj = c.Cnpj,
                        Endereco = c.Endereco,
                        NomeFantasia = c.NomeFantasia,
                        Medicos = ctx.Medicos.Where(m => m.IdInstituicao == c.IdInstituicao).ToList()
                    })
                    .ToList();
        }
    }
}
