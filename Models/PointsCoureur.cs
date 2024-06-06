using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace dotnetEval.Models;
[Table("points_coureur")]
public class PointsCoureur
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("id_etape")]
    public string? IdEtape { get; set; }
    public Etape Etape { get; set; }

    [Column("id_coureur")]
    public string? IdCoureur { get; set; }
    public Coureur Coureur { get; set; }

    [Column("id_points")]
    public string? IdPoints { get; set; }
    public Points Points{ get; set; }

}
