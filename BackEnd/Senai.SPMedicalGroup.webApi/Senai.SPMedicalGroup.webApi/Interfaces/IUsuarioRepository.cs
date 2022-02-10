using Microsoft.AspNetCore.Http;
using Senai.SPMedicalGroup.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SPMedicalGroup.webApi
{
    interface IUsuarioRepository
    {
        List<Usuario> ListarUsuarios();
        void Cadastrar(Usuario novoUsuario);
        Usuario Login(string email, string senha);
        void Deletar(int id);
        void SalvarPerfilBD(IFormFile foto, int id);
        string ConsultarPerfilBD(int id);
        Usuario BuscarPorId(int id);
        void Atualizar(int id, Usuario UsuarioAtualizado);
    }
}
