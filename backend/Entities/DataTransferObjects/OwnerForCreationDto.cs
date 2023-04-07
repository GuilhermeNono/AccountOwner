using System.ComponentModel.DataAnnotations;
namespace Entities.DataTransferObjects;
public class OwnerForCreationDto
{
    [Required(ErrorMessage = "Campo obrigatório: Nome")]
    [StringLength(60, ErrorMessage = "O Nome não pode ter mais de 60 caracteres")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Campo obrigatório: Data de Nascimento")]
    public DateTime DateOfBirth { get; set; }
    [Required(ErrorMessage = "Campo obrigatório: Endereço")]
    [StringLength(100, ErrorMessage = "O Endereço não pode ter mais de 100 caracteres")]
    public string Address { get; set; }
}