using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.DTOs
{

    public class OrderUpdateDTO
    {
        [Required]
        public string Name { get; set; }
    }

    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int ClientId{ get; set; }
        public ClientDTO Client { get; set; }
        public decimal Iva { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public List<OrderDetailDTO> items { get; set; }
    }
    public class OrderCreateDTO
    {
        public int ClientId { get; set; }
        public List<OrderDetailCreateDTO> items { get; set; }
    }

    public class OrderDetailCreateDTO {
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

    }
    public class OrderDetailDTO {
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
        public decimal Iva { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

    }
}
