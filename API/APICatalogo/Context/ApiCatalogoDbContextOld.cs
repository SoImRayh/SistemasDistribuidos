using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Context;

/*
 * Classe auxiliar para relacionamento entre as entidades Models e o banco de dados da aplicação
 * Nela deve conter todos os objetos dos Models nos DbSet, para o EntityFremework relacionar/criar tabelas
 */
public class ApiCatalogoDbContextOld : DbContext
{
    public ApiCatalogoDbContextOld(DbContextOptions<ApiCatalogoDbContextOld> options) : base(options) { }

    public DbSet<Categoria>? Categorias { get; set; }//o nome do parâmetro será dado à tabela (caso a classe
                                                    //Model não esteja especificando qual o nome da tabela
                                                    //através de DataAnnotations). Neste caso o nome da tabela
                                                    //seria catalogos, porém, na classe Categoria estou especificando
                                                    //que o nome será catalogo
    public DbSet<Produto>? Produtos { get; set; }
}
