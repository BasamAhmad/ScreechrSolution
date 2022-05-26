namespace Screechr.Api.Models;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


[Index(nameof(Id), IsUnique = true)]
public class Screech
{
    /// <summary>
    /// The user identifier.
    /// </summary>
    [Key]
    [Required(ErrorMessage = "Id is Required")]
    [Range(1, 1000000000000000000, ErrorMessage = "Value beyond accepted range")]
    public int Id { get; set; }


    [Required(ErrorMessage = "Content is Required")]
    [StringLength(1024, ErrorMessage = "Content cannot be more than 1024 character")]
    public string Content { get; set; }

    /// <summary>
    /// Gets or sets the creator user id.
    /// </summary>
    [Required(ErrorMessage = "CreatorUserId is Required")]
    [Range(1, 10000000000, ErrorMessage = "Value beyond accepted range")]
    public int CreatorUserId { get; set; }

    [Required(ErrorMessage = "CreatedDate is Required")]
    public DateTimeOffset CreatedDate { get; set; }

    [Required(ErrorMessage = "ModifiedDate is Required")]
    public DateTimeOffset ModifiedDate { get; set; }
}