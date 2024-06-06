using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace dotnetEval.Models;
[Table("etape")]
public class Etape
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("nom")]
    public string? Nom { get; set; }

    [Column("longueur")]
    public double? Longueur { get; set; }

    [Column("nombre_coureur_equipe")]
    public int? NombreCoureurEquipe { get; set; }

    [Column("rang_etape")]
    public int? RangEtape { get; set; }

}
