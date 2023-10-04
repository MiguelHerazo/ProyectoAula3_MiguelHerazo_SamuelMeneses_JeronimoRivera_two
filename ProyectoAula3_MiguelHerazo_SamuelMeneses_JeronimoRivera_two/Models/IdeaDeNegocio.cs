namespace ProyectoAula3_MiguelHerazo_SamuelMeneses_JeronimoRivera.Models
{
    public class IdeaDeNegocio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Impacto { get; set; }
        public int ValorInversion { get; set; }
        public int Ingresos3Anios { get; set; }
        public List<IntegranteEquipo> IntegrantesEquipo { get; set; }
        public List<Departamento> DepartamentosBeneficiados { get; set; }
        public List<string> Herramientas4RI { get; set; }
    }


}
