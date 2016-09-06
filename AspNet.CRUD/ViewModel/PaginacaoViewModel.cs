using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNet.CRUD.ViewModel
{
    public class PaginacaoViewModel
    {
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public string Form { get; set; }
        public string PageIndexName { get; set; }
    }
}