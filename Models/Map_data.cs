using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetEval;

 [Table("map_data")]
public class Map_data
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("numero")]
    public double Numero { get; set; }
    [Column("total")]
    public double Total { get; set; }
}
