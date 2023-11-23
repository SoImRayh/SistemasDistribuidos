using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("Produto")]//Especifica o nome da tabela
public class Produto
{
    [Key]//Especifica que esta será nossa chave primária da tabela. Neste caso é opcional
    //pois caso um atributo tenha nome com o padrão nomeDaClasseId o EntityFramework já detecta
    //este atributo como sendo a chave primária
    public int ProdutoId { get; set; }

    [Required]//Diz que este campo é notnull na tabela e não pode ser nulo na tabela
    [StringLength(80)]//Diz que o campo na tabela será varchar(80)
    public string? Nome { get; set; }

    [Required]//Diz que este campo é notnull na tabela e não pode ser nulo na tabela
    [StringLength(300)]//Diz que o campo na tabela será varchar(300)
    public string? Descricao { get; set; }

    [Required]//Diz que este campo é notnull na tabela e não pode ser nulo na tabela
    [Column(TypeName = "decimal(10,2)")]//Diz que o campo na tabela será decimal(10,2)
    public decimal Preco { get; set; }

    [Required]//Diz que este campo é notnull na tabela e não pode ser nulo na tabela
    [StringLength(300)]//Diz que o campo na tabela será varchar(300)
    public string? ImagemUrl { get; set; }

    public float Estoque { get; set; }

    public DateTime DataCadastro { get; set; }

    public int CategoriaId { get; set; }

    [JsonIgnore]//Especifica que esta propriedade não será serializada/desserializada na conversão para Json deste objeto, ou seja, não será exibida
    public Categoria? Categoria { get; set; }
}
