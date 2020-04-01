using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FastDevIOProject.Models
{
    public class Fornecedor : Entity
    {
        public string Documento { get; set; }
        public TipoFornecedor TipoFornecedor { get; set; }

        public Endereco Endereco { get; set; }

        public bool Ativo { get; set; }

        /*EF Relations*/
        public IEnumerable<Produto> Produtos { get; set; }
    }
}
