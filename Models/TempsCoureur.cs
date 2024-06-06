using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace dotnetEval.Models;
[Table("temps_coureur")]
public class TempsCoureur
{

    public TempsCoureur(){

    }
    public TempsCoureur(string? id_etape, string v, DateTime dateTime1, DateTime dateTime2)
    {
        Id_etape = id_etape;
        V = v;
        DateTime1 = dateTime1;
        DateTime2 = dateTime2;
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

    [Column("heure_depart")]
    public DateTime HeureDepart { get; set; }

    [Column("heure_arrive")]
    public DateTime HeureArrive { get; set; }
    public string? Id_etape { get; }
    public string V { get; }
    public DateTime DateTime1 { get; }
    public DateTime DateTime2 { get; }
}
