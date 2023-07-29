using CleanArchMvc.Domain.Validation;

//aula 30

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Product //: BaseEntity
    {
        public int Id { get; private set; } //herdada de BaseEntity 
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }

        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            //Id 
            DomainExceptionValidation.When(id < 0, "Id inválido.");
            this.Id = id;

            //Outras propriedades 
            ValidateDomain(name, description, price, stock, image);
        }

        public Product(string name, string description, decimal price, int stock, string image)
        {
            ValidateDomain(name, description, price, stock, image);
        }

        public void Update(string name, string description, decimal price, int stock, string image, int categoryID)
        {
            ValidateDomain(name, description, price, stock, image);
            this.CategoryId = categoryID;
        }

        private void ValidateDomain(string name, string description, decimal price, int stock, string image)
        {
            //Regras para a propriedade Name 
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                "Nome do Produto inválido. Nome do produto não pode ser nulo");
            DomainExceptionValidation.When(name.Length < 3, "Nome inválido. Tamanho minimo 3 caracteres");

            //outras regras aula 30 

            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Stock = stock;
            this.Image = image;

        }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}