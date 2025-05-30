using System;
namespace Desafio.API.Models
{
    public class Machine
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
    }
}
