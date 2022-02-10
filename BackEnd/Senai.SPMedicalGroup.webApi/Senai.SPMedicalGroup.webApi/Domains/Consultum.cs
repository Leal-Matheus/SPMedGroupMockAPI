using System;
using System.Collections.Generic;

#nullable disable

namespace Senai.SPMedicalGroup.webApi.Domains
{
    public partial class Consultum
    {
        public int IdConsulta { get; set; }
        public int? IdMedico { get; set; }
        public int? IdSituacao { get; set; }
        public int? IdPaciente { get; set; }
        public DateTime DataConsulta { get; set; }
        public string DescricaoConsulta { get; set; }

        public virtual Medico IdMedicoNavigation { get; set; }
        public virtual Paciente IdPacienteNavigation { get; set; }
        public virtual Situacao IdSituacaoNavigation { get; set; }
    }
}
