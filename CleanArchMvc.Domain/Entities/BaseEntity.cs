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
