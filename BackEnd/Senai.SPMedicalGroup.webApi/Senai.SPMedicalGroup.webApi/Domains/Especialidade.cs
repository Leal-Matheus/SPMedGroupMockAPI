using System;
using System.Collections.Generic;

#nullable disable

namespace Senai.SPMedicalGroup.webApi.Domains
{
    public partial class Especialidade
    {
        public Especialidade()
        {
            Medicos = new HashSet<Medico>();
        }

        public int IdEspecialidade { get; set; }
        public string TipoEspecialidade { get; set; }

        public virtual ICollection<Medico> Medicos { get; set; }
    }
}
