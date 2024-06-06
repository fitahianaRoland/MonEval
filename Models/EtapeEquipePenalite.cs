using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace dotnetEval.Models;
[Table("etape_equipe_penalite")]
public class EtapeEquipePenalite
{
    public EtapeEquipePenalite(){
        
    }
    public EtapeEquipePenalite(string? idEtape, string? idEquipe,TimeSpan? tempsPenalite)
    {
        IdEtape = idEtape;
        IdEquipe = idEquipe;
        TempsPenalite = tempsPenalite;
    }

    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("id_etape")]
    public string? IdEtape { get; set; }
    public virtual Etape Etape { get; set; }

    [Column("id_equipe")]
    public string? IdEquipe { get; set; }
    public virtual Equipe Equipe { get; set; }


    [Column("temps_penalite")]
    public TimeSpan? TempsPenalite { get; set; }

}
