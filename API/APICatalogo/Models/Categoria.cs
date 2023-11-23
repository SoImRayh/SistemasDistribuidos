using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models;

[Table("categoria")]//Especifica qual será o nome da tabela
public class Categoria
{
    public Categoria()
    { }

    [Key]//Especifica que esta será nossa chave primária da tabela. Neste caso é opcional
    //pois caso um atributo tenha nome com o padrão nomeDaClasseId o EntityFramework já detecta
    //este atributo como sendo a chave primária
    [Column("categoriaid")]
    public int CategoriaId { get; set; }

    [Required]//Diz que este campo é notnull na tabela e não pode ser nulo na tabela
    [StringLength(80)]//Diz que o campo na tabela será varchar(80)
    [Column("nome")]
    public string? Nome { get; set; }

    [Required]//Diz que este campo é notnull na tabela e não pode ser nulo na tabela
    [StringLength(300)]//Diz que o campo na tabela será varchar(300)
    [Column("imagemurl")]
    public string? ImagemUrl { get; set; }
}
