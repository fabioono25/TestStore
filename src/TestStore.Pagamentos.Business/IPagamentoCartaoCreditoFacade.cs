namespace TestStore.Pagamentos.Business
{
    //implementacao da facade sera na camada de anti-corrupcao
    public interface IPagamentoCartaoCreditoFacade
    {
        Transacao RealizarPagamento(Pedido pedido, Pagamento pagamento);
    }
}