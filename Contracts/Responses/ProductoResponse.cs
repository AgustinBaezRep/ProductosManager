﻿using Domain.Entities;

namespace Contracts.Responses
{
    public class ProductoResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
