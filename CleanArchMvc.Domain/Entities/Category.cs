using CleanArchMvc.Domain.Validation;

//aula 29, 30
/*
 BUG [✓] sdfjuhjkh dsjkfh jkghfkjghfdkjghfd kjghdfgjkfdhg
 */

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Category //:BaseEntity
    {
        public int Id { get; private set; } //herdada de BaseEntity 
        public string Name { get; private set; }
        public ICollection<Product> Products { get; set; }

        public Category(string name)
        {
            ValidateDomain(name);
        }

        public Category(int id, string name)
        {
            //Id 
            DomainExceptionValidation.When(id < 0, "Id inválido.");
            this.Id = id;

            //Name 
            ValidateDomain(name);

        }

        public void Update(string name)
        {
            ValidateDomain(name);
            //this.Name = name;   
        }

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Nome inválido. Nome requerido.");

            DomainExceptionValidation.When(name.Length < 3, "Nome inválido. Tamanho minimo 3 caracteres");

            this.Name = name;
        }
    }
}