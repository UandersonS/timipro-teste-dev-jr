﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Teste.Timipro.Models
{
    public class Product
    {
        //Produto ID
        [Key]
        [Display(Name = "Produto")]
        public int ProductID { get; set; }

        //PRODUTO
        [Required(ErrorMessage = "O Campo 'Nome' é Obrigatório!")]
        [MaxLength(100, ErrorMessage = "O Campo 'Nome' recebe no máximo 100 caracteres")]
        [Display(Name = "Nome Do Produto")]

        public string ProductName { get; set; }
        //BOTAO ACTIVE
        [Display(Name = "Ativo")]
        [Required(ErrorMessage = "Selecione uma das opções")]

        public bool Active { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}