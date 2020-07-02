using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Excel_Alterar
{
    public class Produto : IProduto
    {
        public Produto(int codigo, string nome, float preco) 
        {
            this.Codigo = codigo;
                this.Nome = nome;
                this.Preco = preco;
               
        }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        
        private const string PATH = "Database/produto.csv";

        /// <summary>
        /// Cria um diretório ou arquivo ou os dois, se não existirem
        /// </summary>
        public Produto(){
            if(!File.Exists(PATH)){
                Directory.CreateDirectory("Database");
                File.Create(PATH).Close();
            }
        }

        /// <summary>
        /// Cadastra produtos
        /// </summary>
        /// <param name="prod">serve para registrar um produto</param>
        public void Cadastrar(Produto prod){
            var linha = new string[] { PrepararLinha(prod) };
            File.AppendAllLines(PATH, linha);
        }
        
        /// <summary>
        /// Lê os produtos
        /// </summary>
        /// <returns>retorna os produtos espaçados</returns>
        public List<Produto> Ler(){
            // Criamos uma lista que servirá como nosso retorno
            List<Produto> produtos = new List<Produto>();;

            // Lemos o arquivo e transformamos em um array de linhas
            // [0] = codigo 1;nome = Fender; preco 5500;
            // [1] = codigo 2;nome = Gibson; preco 7500;
            string[] linhas  =  System.IO.File.ReadAllLines(PATH); 

            foreach(string linha in linhas){
                // Separamos os dados de cada linha com o Split()
                // [0] = codigo = 1
                // [1] = nome = Fender
                // [2] = preco = 5500
                string[] dado = linha.Split(";");

                // Criamos instâncias de produtos para serem colocados na lista 
                Produto p   = new Produto();
                p.Codigo    = Int32.Parse( Separar(dado[0]) );
                p.Nome      = Separar(dado[1]);
                p.Preco     = float.Parse( Separar(dado[2]) );

                produtos.Add(p);
            }
            
            produtos = produtos.OrderBy(y => y.Nome).ToList();
            return produtos;
        }
        // ExcelU

        /// <summary>
        /// Remove uma ou mais linhas 
        /// </summary>
        /// <param name="_termo">termo que será buscado</param>
        public void Remover(string _termo){
            // Criamos uma lista que servirá como uma espécie de backup para as linhas do csv
            List<string> linhas =  new List<string>();

            // Utilizamos a biblioteca StreamReader para ler nosso .csv
            using(StreamReader arquivo = new StreamReader(PATH)){
                string linha;
                while((linha = arquivo.ReadLine()) != null){
                    linhas.Add(linha);
                }
            }

            // Removemos as linhas que tiverem com o termo passado como argumento 
            // codigo = 1;nome = Tagima;preco = 5500;
            // Tagima
            linhas.RemoveAll(l => l.Contains(_termo));

            // Reescrevemos nosso csv do zero
            ReescreverCSV(linhas);
        }

        public void Alterar(Produto produtoAlterado){
            // Criamos uma lista que servirá como uma espécie de backup para as linhas do csv
            List<string> linhas =  new List<string>();

            // 
            using(StreamReader arquivo = new StreamReader(PATH)){
                string linha;
                while((linha = arquivo.ReadLine()) != null){
                    linhas.Add(linha);
                }
            }

            // linhas.RemoveAll(x => x.Split(";")[0].Contains(produtoAlterado.Codigo.ToString()));

            linhas.RemoveAll(z => z.Split(";")[0].Split("=")[1] == produtoAlterado.Codigo.ToString());

            linhas.Add(PrepararLinha(produtoAlterado));

            // Criamos uma forma de reescrever o arquivo sem as linhas removidas
            ReescreverCSV(linhas);
        }

        /// <summary>
        /// Reescreve o CSV
        /// </summary>
        /// <param name="lines">lista de linhas</param>
        public void ReescreverCSV(List<string> lines){
            // Criamos uma forma de reescrever o arquivo sem as linhas removidas
            using(StreamWriter output = new StreamWriter(PATH)){
                // output.Write(String.Join(Environment.NewLine, linhas.ToArray()));
                foreach(string ln in lines){
                    output.Write(ln + "\n");
                }
            }
        }

        /// <summary>
        /// Filtra os produtos 
        /// </summary>
        /// <param name="_nome">Serve para filtrar pelo nome</param>
        /// <returns>Produtos filtrados pelo nome</returns>
        public List<Produto> Filtrar(string _nome){
            return Ler().FindAll(x => x.Nome == _nome );
        }

        /// <summary>
        /// Serve para separar os itens por colunas
        /// </summary>
        /// <param name="_coluna">serve para separar os itens do .csv</param>
        /// <returns>valor de indice [1] da coluna</returns>
        private string Separar(string _coluna){
            return _coluna.Split("=")[1];
        }
        // 1; Celular; 600;

        /// <summary>
        /// Prepara as linhas
        /// </summary>
        /// <param name="p">itens colocados no console</param>
        /// <returns>itens</returns>
        private string PrepararLinha(Produto p){
          return   $"Codigo = {p.Codigo}; Nome = {p.Nome}; Preço = {p.Preco}";
        }

    }
}