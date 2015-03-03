namespace Intime.OPC.Domain.Models
{
    public class ShipVia
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Status { get; set; }
        public string TemplateName { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}