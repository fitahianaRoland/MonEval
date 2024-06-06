using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace dotnetEval.Models;
[Table("categorie_coureur")]
public class CategorieCoureur
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("id_coureur")]
    public string? IdCoureur { get; set; }
    public Coureur Coureur { get; set; }

    [Column("id_categorie")]
    public string? IdCategorie { get; set; }
    public Categorie Categorie{ get; set; }

}
