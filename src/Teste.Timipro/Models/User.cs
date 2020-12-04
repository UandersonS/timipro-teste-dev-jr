﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Teste.Timipro.Models
{
    public class User
    {
        [Key]//definir como chave primaria
        
        [Display(Name = "Usuário")]
        public int UserId { get; set; }

        //EMAIL
        [Required(ErrorMessage = "O Campo 'Email' é Obrigatório!")]
        [MaxLength(100, ErrorMessage = "O campo 'Email' recebe no máximo 100 caracteres")]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        [Index("User_UserName_Index", IsUnique = true)]
        public string UserName { get; set; }

        //NOME
        [Required(ErrorMessage = "O Campo 'Nome' é Obrigatório!")]
        [MaxLength(100, ErrorMessage = "O Campo 'Nome' recebe no máximo 100 caracteres")]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        //CPF
        [Required(ErrorMessage = "O Campo 'CPF' é Obrigatório!")]
        [MaxLength(15, ErrorMessage = "O campo Telefone recebe no máximo 15 caracteres")]
        [Display(Name = "CPF")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Digite somente os números do seu CPF!")]
        [Index("Name_Index", IsUnique = true)]
        public string CPF { get; set; }

        [Display(Name = "Produto Associado")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione Um Produto")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}