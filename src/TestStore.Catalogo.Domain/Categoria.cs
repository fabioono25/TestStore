using System.Collections.Generic;
using TestStore.Core.DomainObjects;

namespace TestStore.Catalogo.Domain
{
    ///entidade: possui identidade
    ///contudo, a categoria atende a produto
    ///categoria e' uma entidade imutavel
    public class Categoria: Entity
    {
        protected Categoria()
        {

        }

        public string Nome { get; private set; }
        public int Codigo { get; private set; }
        
        //por conta do EF
        public ICollection<Produto> Produtos { get; set; }

        public Categoria(string nome, int codigo)
        {
            Nome = nome;
            Codigo = codigo;

            Validar();
        }

        public override string ToString()
        {
            return $"{Nome} - {Codigo}";
        }

        public void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome da categoria não pode estar vazio");
            Validacoes.ValidarSeIgual(Codigo, 0, "O campo Codigo não pode ser 0");
        }
    }
}
