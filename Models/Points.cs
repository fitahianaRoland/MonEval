using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace dotnetEval.Models;
[Table("points")]
public class Points
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("rang")]
    public int? Rang { get; set; }

    [Column("point")]
    public double? Point { get; set; }

}
