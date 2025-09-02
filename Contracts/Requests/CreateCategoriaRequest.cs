namespace Contracts.Requests
{
    public class CreateCategoriaRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
