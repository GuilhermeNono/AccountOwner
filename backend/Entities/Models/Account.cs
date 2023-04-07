using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models;
[Table("Account")]
public class Account
{
[Column("AccountId")]
public Guid Id { get; set; }
[Required]
public DateTime DateCreated { get; set; }
[Required]
public string AccountType { get; set; }
[ForeignKey(nameof(Owner))]
[Required]
public Guid OwnerId { get; set; }
public Owner Owner { get; set; }
}