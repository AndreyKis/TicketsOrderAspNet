//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AccesToTicketsDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Main
    {
        public int m_id { get; set; }
        public int pers_id { get; set; }
        public int ticket_id { get; set; }
        public System.DateTime month { get; set; }
        public int amount { get; set; }
        public Nullable<int> pledge { get; set; }
    
        public virtual Person Person { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
