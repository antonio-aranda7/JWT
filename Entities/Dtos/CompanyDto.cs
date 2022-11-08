using System;

namespace CompanyEmployees.Entities.DataTransferObjects
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FullAddress { get; set; }
    }
}
