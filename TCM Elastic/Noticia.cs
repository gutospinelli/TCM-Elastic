using System;
using System.Collections.Generic;
using System.Text;

namespace TCM_Elastic
{
    public class Noticia
    {
        public int Id { get; set; }
        public string Categoria { get; set; }

        public int MatriculaAutor { get; set; }
        public string NomeAutor { get; set; }

        public DateTime AtualizadaEm {get;set;}
        
        public DateTime PublicadaEm {get;set;}

        public string Titulo { get; set; }
        public string Ementa { get; set; }
        public string Status { get; set; }

        public string URLNoticia 
        {
            get => "http://www.tcm.rj.gov.br/WEB/Site/Noticia_Detalhe.aspx?noticia=" + Id;
        }
    }
}
