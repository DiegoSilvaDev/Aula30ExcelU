using System;
using System.Collections.Generic;

namespace Excel_Alterar
{
    class Program
    {
        static void Main(string[] args)
        {
           Console.WriteLine("Hello World!");
            Produto p1 = new Produto();
            p1.Codigo =  4;
            p1.Nome = "Tagima";
            p1.Preco = 5500;

            p1.Cadastrar(p1);
            // p1.Remover("Fender");

            Produto alterado = new Produto();
            alterado.Codigo = 3;
            alterado.Nome = "Ibanez";
            alterado.Preco = 6800;

            p1.Alterar(alterado);

            List<Produto> lista = new List<Produto>();
            lista = p1.Ler();
            p1.Remover("Gibson");

            foreach (Produto item in lista)
            {
                Console.WriteLine("{0:c} - {1} ",item.Preco, item.Nome);

            }
        }
    }
}
