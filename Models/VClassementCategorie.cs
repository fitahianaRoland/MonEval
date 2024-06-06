using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace dotnetEval.Models;
[Table("v_classement_categorie")]
public class VClassementCategorie
{
    
    [Column("id_etape")]
    public string? IdEtape { get; set; }

    [Column("id_coureur")]
    public string? IdCoureur { get; set; }

    [Column("id_equipe")]
    public string? IdEquipe { get; set; }

    [Column("id_categorie")]
    public string? IdCategorie { get; set; }

    [Column("heure_depart")]
    public DateTime? HeureDepart { get; set; }

    [Column("heure_arrive")]
    public DateTime? HeureArrive { get; set; }

    [Column("duree")]
    public TimeSpan? Duree { get; set; }

    [Column("rang")]
    public int? Rang { get; set; }

    [Column("point")]
    public double? Point { get; set; }

}
