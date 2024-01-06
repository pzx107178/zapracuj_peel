using Microsoft.AspNetCore.Identity;


namespace mapkowanie.Models
{
    public class Konto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public virtual Branza? Branza { get; set; }
        public bool Zablokowane { get; set; }


        public int RodzajKonta { get; set; } //0 dla pracownika, 1 dla zleceniodawcy
        public virtual Rodzaje? Rodzaje { get; set; }


        public uint WynagrodzenieMinimalne { get; set; }
        public DateTime GodzinaStart { get; set; }
        public DateTime GodzinaStop { get; set; }

        
        public string Adres { get; set; }
        public string Nip { get; set; }


        public string? userId { get; set; }
        public virtual IdentityUser? user { get; set; }

    }
}
