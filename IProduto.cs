using System.Collections.Generic;

namespace Excel_Alterar
{
    public interface IProduto
    {
         void Cadastrar(Produto prod);

         List<Produto> Ler();

         void Alterar(Produto produtoAlterado);

         void Remover(string _termo);
    }
}