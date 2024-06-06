using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetEval.Models;

[Table("dataimport")]
public class DataImport{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public DataImport(){
    }
    [Key]
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("iddata")]
    public int idData { get; set; }

    [Column("societe")]
    public string Societe{ get; set; }
    [Column("numeroemploye")]
    public double NumeroEmploye{ get; set; }
    [Column("ville")]
    public string Ville{ get; set; }
    [Column("pays")]
    public string Pays{ get; set; }
    [Column("datecommande")]
    public DateTime? DateCommande{ get; set; }
    [Column("numerocommande")]
    public double NumeroCommande{ get; set; }
    [Column("nomproduit")]
    public string NomProduit{ get; set; }
    [Column("prixtotal")]
    public double PrixTotal{ get; set; }


}
