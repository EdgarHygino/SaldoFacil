using SaldoFacil.Model.Models.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaldoFacil.Model.Models.Entities
{
    public class Transacao
    {
        public Transacao()
        {
            Data = DateTime.Now;
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Necessario Informar um cliente")]
        public int ClienteID { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public TipoTransacao Tipo { get; set; }
    }
}
