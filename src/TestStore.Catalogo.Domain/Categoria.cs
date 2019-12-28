using TestStore.Core.DomainObjects;

namespace TestStore.Catalogo.Domain
{
    ///entidade: possui identidade
    ///contudo, a categoria atende a produto
    ///categoria e' uma entidade imutavel
    public class Categoria: Entity
    {
        public string Nome { get; private set; }
        public int Codigo { get; private set; }

        public Categoria(string nome, int codigo)
        {
            Nome = nome;
            Codigo = codigo;
        }

        public override string ToString()
        {
            return $"{Nome} - {Codigo}";
        }
    }
}
