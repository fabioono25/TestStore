using System;
using TestStore.Core.DomainObjects;

namespace TestStore.Catalogo.Domain
{
    ///entidade: possui identidade
    ///produto e' uma raiz de agregacao
    ///a entidade deveria ser ignorante ao meio de persistencia (DB)
    ///contudo precisamos de uma implementacao para antender ao modelo de ORM
    public class Produto : Entity, IAggregateRoot
    {
        //necessario por conta do EF
        public Guid CategoriaId { get; private set; }
        //set privado como boa pratica
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Imagem { get; private set; }
        public int QuantidadeEstoque { get; private set; } //poderia ser um modulo de estoque
        public Categoria Categoria { get; private set; }

        public Produto(string nome, string descricao, bool ativo, decimal valor, Guid categoriaId, DateTime dataCadastro, string imagem)
        {
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Valor = valor;
            CategoriaId = categoriaId;
            DataCadastro = dataCadastro;
            Imagem = imagem;
        }

        //ad-hoc setters: trocar estado da entidade
        //comportamentos: alteram o estado da entidade
        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        public void AlterarCategoria(Categoria categoria) { 
            Categoria = categoria;
            CategoriaId = categoria.Id;
        }

        public void AlterarDescricao(string descricao)
        {
            Descricao = descricao;
        }

        public void DebitarEstoque(int quantidade)
        {
            if (quantidade < 0) quantidade *= -1;

            QuantidadeEstoque -= quantidade;
        }

        public void ReporEstoque(int quantidade)
        {
            QuantidadeEstoque += quantidade;
        }

        public bool PossuiEstoque(int quantidade)
        {
            return QuantidadeEstoque >= quantidade;
        }

        //a entidade possui a capacidade de se auto-validar
        //consistencia da entidade
        public void Validar()
        {

        }
    }
}
