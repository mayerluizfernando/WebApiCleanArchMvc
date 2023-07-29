using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Entities
{
    //aula 30 
    //TODO jdshfjkh dsjkh sdfjkhdfjkh gkjfhjk hfdgjk  hfgjkh gfdjkhg fdjkghf
    public abstract class BaseEntity
    {
        
        public int Id { get; protected set; } 
        public DateTime CreatedAt { get; protected set; }
        public DateTime ModifiedAt { get; protected set; }

    }
}
