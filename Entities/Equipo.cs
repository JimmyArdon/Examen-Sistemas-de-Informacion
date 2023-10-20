using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiEquipo.Entities
{
    [Table("equipo")]
    public class Equipo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("telefono")]
        public string Telefono { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("Rol")]
        public string Rol { get; set; }

        [ForeignKey("jefe_id")]
        public int? JefeId { get; set; }

        [ForeignKey("JefeId")]
        public Equipo? Jefe { get; set; }

    }

}
