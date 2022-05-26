namespace Screechr.Api.Models;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


[Index(nameof(Id), IsUnique = true)]
public class User
{
    /// <summary>
    /// The user identifier.
    /// </summary>
    [Key]
    [Required(ErrorMessage = "Id is Required")]
    [Range(1, 10000000000, ErrorMessage = "Value beyond accepted range")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    [Required(ErrorMessage = "Username is Required")]
    [StringLength(80, ErrorMessage = "Username cannot be more than 80 character")]
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    [Required(ErrorMessage = "FirstName is Required")]
    [StringLength(100, ErrorMessage = "FirstName cannot be more than 100 character")]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    [Required(ErrorMessage = "LastName is Required")]
    [StringLength(100, ErrorMessage = "LastName cannot be more than 100 character")]
    public string LastName { get; set; }

    public string? ProfileImageUri { get; set; }
    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    [Required(ErrorMessage = "CreatedDate is Required")]
    public DateTimeOffset CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the modified date.
    /// </summary>
    [Required(ErrorMessage = "ModifiedDate is Required")]
    public DateTimeOffset ModifiedDate { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    [JsonIgnore]
    [DataType(DataType.Password)]
    [DefaultValue("ScreechrPassword")]
    public string? Password { get; set; }
}