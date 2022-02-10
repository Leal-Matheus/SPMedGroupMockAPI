using System;
using System.Collections.Generic;

#nullable disable

namespace Senai.SPMedicalGroup.webApi.Domains
{
    public partial class Imagemusuario
    {
        public int IdImagem { get; set; }
        public int IdUsuario { get; set; }
        public byte[] Binario { get; set; }
        public string MimeType { get; set; }
        public string NomeArquivo { get; set; }
        public DateTime? DataInclusao { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
