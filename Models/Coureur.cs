using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace dotnetEval.Models;
[Table("coureur")]
public class Coureur
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("nom")]
    public string? Nom { get; set; }

    [Column("numero_dossard")]
    public int? NumeroDossard { get; set; }

    [Column("date_de_naissance")]
    public DateTime? DateDeNaissance { get; set; }

    [Column("id_genre")]
    public string? IdGenre { get; set; }
    public Genre Genre { get; set; } 

    [Column("id_equipe")]
    public string? IdEquipe { get; set; }
    public Equipe Equipe{ get; set; }

}
