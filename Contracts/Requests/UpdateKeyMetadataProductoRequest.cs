﻿namespace Contracts.Requests
{
    public class UpdateKeyMetadataProductoRequest
    {
        public string? Nombre { get; set; }
        public decimal? Precio { get; set; }
        public int? Stock { get; set; }
    }
}
