using System.ComponentModel.DataAnnotations;

namespace ApiCadastroReact.Models
{
    public class UsuarioModels
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; } = string.Empty;


        [Required]
        [Range(1,120)]
        public int Idade { get; set; }


        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email{ get; set; } = string.Empty;
    }
}
