using Microsoft.AspNetCore.Http;
using Senai.SPMedicalGroup.webApi.Contexts;
using Senai.SPMedicalGroup.webApi.Domains;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SPMedicalGroup.webApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        SPMedicalGroupContext ctx = new SPMedicalGroupContext();
        public void Atualizar(int id, Usuario UsuarioAtualizado)
        {
            Usuario usuarioBuscado = BuscarPorId(id);

            if (UsuarioAtualizado.NomeUsuario != null || UsuarioAtualizado.SenhaUsuario != null || UsuarioAtualizado.EmailUsuario != null)
            {
                usuarioBuscado.NomeUsuario = UsuarioAtualizado.NomeUsuario;
                usuarioBuscado.EmailUsuario = UsuarioAtualizado.EmailUsuario;
                usuarioBuscado.SenhaUsuario = UsuarioAtualizado.SenhaUsuario;

                ctx.Usuarios.Update(usuarioBuscado);

                ctx.SaveChanges();
            }
        }

        public Usuario BuscarPorId(int id)
        {
            return ctx.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
        }

        public void Cadastrar(Usuario novoUsuario)
        {
            ctx.Usuarios.Add(novoUsuario);

            ctx.SaveChanges();
        }

        public string ConsultarPerfilBD(int id)
        {
            Imagemusuario imagemUsuario = new Imagemusuario();
            imagemUsuario = ctx.Imagemusuarios.FirstOrDefault(i => i.IdUsuario == id);

            if (imagemUsuario != null)
            {
                return Convert.ToBase64String(imagemUsuario.Binario);
            }
            return null;
        }

        public void Deletar(int id)
        {
            ctx.Usuarios.Remove(BuscarPorId(id));

            ctx.SaveChanges();
        }

        public List<Usuario> ListarUsuarios()
        {
            return ctx.Usuarios
                .Select(u => new Usuario
                {
                    EmailUsuario = u.EmailUsuario,
                    NomeUsuario = u.NomeUsuario,
                    IdTipoUsuarioNavigation = new TipoUsuario()
                    {
                        TituloTipoUsuario = u.IdTipoUsuarioNavigation.TituloTipoUsuario
                    }
                })
                .ToList();
        }

        public Usuario Login(string email, string senha)
        {
            return ctx.Usuarios.FirstOrDefault(e => e.EmailUsuario == email && e.SenhaUsuario == senha);
        }

        public void SalvarPerfilBD(IFormFile foto, int id)
        {
            Imagemusuario imagemUsuario = new Imagemusuario();

            using (var ms = new MemoryStream())
            {
                foto.CopyTo(ms);

                imagemUsuario.Binario = ms.ToArray();

                imagemUsuario.NomeArquivo = foto.FileName;
                imagemUsuario.MimeType = foto.FileName.Split('.').Last();
                imagemUsuario.IdUsuario = id;
            }

            Imagemusuario imagemExistente = new Imagemusuario();
            imagemExistente = ctx.Imagemusuarios.FirstOrDefault(i => i.IdUsuario == id);

            if (imagemExistente != null)
            {
                imagemExistente.Binario = imagemUsuario.Binario;
                imagemExistente.NomeArquivo = imagemUsuario.NomeArquivo;
                imagemExistente.MimeType = imagemUsuario.MimeType;
                imagemExistente.IdUsuario = id;

                ctx.Imagemusuarios.Update(imagemExistente);
            }
            else
            {
                ctx.Imagemusuarios.Add(imagemUsuario);
            }

            ctx.SaveChanges();
        }
    }
}
