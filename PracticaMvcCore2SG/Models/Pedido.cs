using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace PracticaMvcCore2SG.Models
{

    [Table("PEDIDOS")]
    public class Pedido
    {

        [Key]
        [Column("IDPEDIDO")]
        public int IdPedido { get; set; }
        [Column("IDFACTURA")]
        public string IdFactura { get; set; }
        [Column("FECHA")]
        public string Fecha { get; set; }
        [Column("IDLIBRO")]
        public string IdLibro { get; set; }
        [Column("IDUSUARIO")]
        public string IdUsuario { get; set; }
        [Column("CANTIDAD")]
        public string Cantidad { get; set; }
    



    }
}
