using System;
using TestStore.Core.DomainObjects;

namespace TestStore.Catalogo.Domain
{
    ///entidade: possui identidade
    ///produto e' uma raiz de agregacao
    ///a entidade deveria ser ignorante ao meio de persistencia (DB)
    ///contudo precisamos de uma implementacao para antender ao modelo de ORM
    ///objeto de valor: agrega valor a entidade
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
        public Dimensoes Dimensoes { get; set; }

        public Categoria Categoria { get; private set; }
        protected Produto()
        {

        }
        public Produto(string nome, string descricao, bool ativo, decimal valor, Guid categoriaId, DateTime dataCadastro, string imagem, Dimensoes dimensoes)
        {
            //fail fast validation - uma das estrategias
            //if (string.IsNullOrEmpty(nome)) throw new Exception("um tipo de validacao");

            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Valor = valor;
            CategoriaId = categoriaId;
            DataCadastro = dataCadastro;
            Imagem = imagem;
            Dimensoes = dimensoes;

            Validar();
        }

        //ad-hoc setters: trocar estado da entidade
        //comportamentos: alteram o estado da entidade
        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        //categoria ja esta valida neste momento
        public void AlterarCategoria(Categoria categoria) { 
            Categoria = categoria;
            CategoriaId = categoria.Id;
        }

        public void AlterarDescricao(string descricao)
        {
            Validacoes.ValidarSeVazio(descricao, "o campo descricao do produto nao pode estar vazio");
            Descricao = descricao;
        }

        public void DebitarEstoque(int quantidade)
        {
            if (quantidade < 0) quantidade *= -1;

            if (!PossuiEstoque(quantidade)) throw new DomainException("Estoque insuficiente");

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
        //consistencia da entidade. estrategia melhor por conta de ser possivel ser chamada externamente
        public void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome do produto não pode estar vazio");
            Validacoes.ValidarSeVazio(Descricao, "O campo Descricao do produto não pode estar vazio");
            Validacoes.ValidarSeIgual(CategoriaId, Guid.Empty, "O campo CategoriaId do produto não pode estar vazio");
            Validacoes.ValidarSeMenorQue(Valor, 1, "O campo Valor do produto não pode se menor igual a 0");
            Validacoes.ValidarSeVazio(Imagem, "O campo Imagem do produto não pode estar vazio");
        }
    }
}
