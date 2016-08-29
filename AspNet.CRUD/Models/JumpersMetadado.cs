using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNet.CRUD.Models
{
    [MetadataType(typeof(JumpersMetadataValidation))]
    public partial class Jumpers
    {

    }

    public class JumpersMetadataValidation
    {
        [Required(ErrorMessage = "Obrigatório informar o tipo")]
        [StringLength(30, ErrorMessage = "O tipo deve possuir no máximo 30 caracteres")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a quantidade")]
        public int Qntd { get; set; }

    }
}