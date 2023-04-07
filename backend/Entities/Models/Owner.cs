using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models;
[Table("owner")]
public class Owner
{
    [Column("OwnerId")]
    public Guid Id { get; set; }
    [Required]
    [StringLength(60)]
    public string Name { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
    [Required]
    [StringLength(100)]
    public string Address { get; set; }
    public ICollection<Account> Accounts { get; set; }
}