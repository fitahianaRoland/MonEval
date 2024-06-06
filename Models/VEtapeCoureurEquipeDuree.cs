using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace dotnetEval.Models;
[Table("v_etape_coureur_equipe_duree")]
public class VEtapeCoureurEquipeDuree
{
    
    [Column("id")]
    public string? Id { get; set; }

    [Column("id_etape")]
    public string? IdEtape { get; set; }

    [Column("id_coureur")]
    public string? IdCoureur { get; set; }

    [Column("nom")]
    public string? Nom { get; set; }

    [Column("numero_dossard")]
    public int? NumeroDossard { get; set; }

    [Column("date_de_naissance")]
    public DateTime? DateDeNaissance { get; set; }

    [Column("id_genre")]
    public string? IdGenre { get; set; }

    [Column("id_equipe")]
    public string? IdEquipe { get; set; }

    [Column("duree")]
    public TimeSpan? Duree { get; set; }

    [ForeignKey("IdEquipe")]
    public virtual Equipe? Equipes{ get; set; }

    [ForeignKey("IdEtape")]
    public virtual Etape? Etapes{get; set;}

    [ForeignKey("IdCoureur")]
    public virtual Coureur? Coureur{get; set; } 

}
