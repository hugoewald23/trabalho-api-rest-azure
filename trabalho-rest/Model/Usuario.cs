using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Pkcs;
using System.ServiceModel.Channels;

namespace trabalho_rest.Model
{
    [Table("USUARIOS")]
    public class Usuario
    {
        [Column("ID")]
        [Key]
        public int Id { get; set; }

        [Column("NOME")]
        [Required (ErrorMessage = "Nome é obrigatório"),  MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Column("IDADE")]
        [Required]
        [Range(18, int.MinValue, ErrorMessage = "Idade mínima é 18 anos")]
        public int Idade { get; set; }

        [Column("EMAIL")]
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email deve ser válido")]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Column("TELEFONE")]
        [Required(ErrorMessage = "Telefone é obrigatório")]
        [MaxLength(15)]
        public string Telefone { get; set; } = string.Empty;

        [Column("DESCRICAO")]
        [MaxLength(500)]
        public string Descricao { get; set; } = string.Empty;

    }
}
