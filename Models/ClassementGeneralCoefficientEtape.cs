using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

[Table("classement_general_coefficient_etape")]
public class ClassementGeneralCoefficientEtape
{
    
    [Column("id_etape")]
    public string? IdEtape { get; set; }

    [Column("id_coureur")]
    public string? IdCoureur { get; set; }

    [Column("id_equipe")]
    public string? IdEquipe { get; set; }

    [Column("heure_depart")]
    public DateTime HeureDepart { get; set; }

    [Column("heure_arrive")]
    public DateTime HeureArrive { get; set; }

    [Column("duree_totale")]
    public TimeSpan? DureeTotale { get; set; }

    [Column("rang")]
    public int? Rang { get; set; }

    [Column("temps_penalite")]
    public TimeSpan? TempsPenalite { get; set; }

    [Column("point")]
    public double? Point { get; set; }

    [Column("etape")]
    public string? Etape { get; set; }

    [Column("longueur")]
    public double? Longueur { get; set; }

    [Column("nombre_coureur_equipe")]
    public int? NombreCoureurEquipe { get; set; }

    [Column("rang_etape")]
    public int? RangEtape { get; set; }

    [Column("coureur")]
    public string? Coureur { get; set; }

    [Column("numero_dossard")]
    public int? NumeroDossard { get; set; }

    [Column("date_de_naissance")]
    public DateTime? DateDeNaissance { get; set; }

    [Column("id_genre")]
    public string? IdGenre { get; set; }

    [Column("genre")]
    public string? Genre { get; set; }

    [Column("equipe")]
    public string? Equipe { get; set; }

    [Column("coefficient")]
    public double? Coefficient { get; set; }

}
