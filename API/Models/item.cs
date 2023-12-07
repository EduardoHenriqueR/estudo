namespace API.Models;

public class Item
{
    public Item() => 
        CriadoEm = DateTime.Now;
    public string? Nome { get; set; }
    public int ItemId { get; set; }
    public int Quantidade { get; set; }
    public string? Efeito { get; set; }
    public string? Durabilidade { get; set; }
    public Categoria? Categoria { get; set; }
    public int CategoriaId { get; set; }
    public DateTime CriadoEm {get; set;}
}