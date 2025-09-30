namespace Domain.Entities;

public class Rol : BaseEntity
{
    public string Nombre { get; set; }
}

public enum TipoRol
{
    Administrador = 1,
    Cliente
}
