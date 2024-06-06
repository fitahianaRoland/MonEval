using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace dotnetEval.Models;
[Table("v_etape_coureur_equipe")]
public class VEtapeCoureurEquipe
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

}
