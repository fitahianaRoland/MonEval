using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using dotnetEval.Models;

namespace dotnetEval.Models;
[Table("etape_coureur")]
public class EtapeCoureur
{
    public EtapeCoureur(string? idEtape, string? idCoureur)
    {
        IdEtape = idEtape;
        IdCoureur = idCoureur;
    }

    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("id_etape")]
    public string? IdEtape { get; set; }
    public Etape Etape { get; set; }

    [Column("id_coureur")]
    public string? IdCoureur { get; set; }
    public Coureur Coureur{ get; set; }

}
