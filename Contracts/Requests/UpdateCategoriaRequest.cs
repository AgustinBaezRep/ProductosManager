namespace Contracts.Requests
{
    public class UpdateCategoriaRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
