using System;
using System.Collections.Generic;

#nullable disable

namespace Senai.SPMedicalGroup.webApi.Domains
{
    public partial class Medico
    {
        public Medico()
        {
            Consulta = new HashSet<Consultum>();
        }

        public int IdMedico { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdEspecialidade { get; set; }
        public int? IdInstituicao { get; set; }
        public string Crm { get; set; }

        public virtual Especialidade IdEspecialidadeNavigation { get; set; }
        public virtual Instituicao IdInstituicaoNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<Consultum> Consulta { get; set; }
    }
}
