using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;


[Table("coefficient_etape")]
public class CoefficientEtape
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("id_etape")]
    public string? IdEtape { get; set; }

    [Column("coefficient")]
    public double? Coefficient { get; set; }

}
