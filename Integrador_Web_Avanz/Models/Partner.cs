namespace Integrador_Web_Avanz.Models
{
    public class Partner
    {
        public string Marca { get; set; }

        public string Imagen { get; set; }

        public Partner(string marca, string imagen) 
        {
            this.Marca = marca;
            this.Imagen = imagen;
        }
    }

    
}
