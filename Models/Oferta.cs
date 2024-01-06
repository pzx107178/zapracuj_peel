using Microsoft.AspNetCore.Identity;

namespace mapkowanie.Models
{
    public class Oferta
    {
        public int Id { get; set; }
        public Konto? Konto { get; set; }
        public string Opis { get; set; }
        public uint WynagrodzenieMin { get; set; }
        public uint WynagrodzenieMax { get; set; }
        public DateTime PracaStart { get; set; }
        public DateTime PracaStop { get; set; }
        public bool widocznosc { get; set; }



        public string? userId { get; set; }
        public virtual IdentityUser? user { get; set; }
    }
}
