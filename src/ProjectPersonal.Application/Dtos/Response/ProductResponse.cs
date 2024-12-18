using AutoMapper;
using ProjectPersonal.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Dtos.Response
{
    public class ProductResponse 
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? Stock { get; set; }
        public Guid CategoryID { get; set; }
        public string ImageURL { get; set; }
    }
}
