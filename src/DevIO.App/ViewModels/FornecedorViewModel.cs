﻿using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.App.ViewModels
{
    public class FornecedorViewModel
    {
        [Key]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {1} e {2} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {1} e {2} caracteres", MinimumLength = 11)]
        public string Documento { get; set; }

        [DisplayName("Tipo")]
        public int TipoFornecedor { get; set; }

        public EnderecoViewModel Endereco { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }

        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
    }
}
