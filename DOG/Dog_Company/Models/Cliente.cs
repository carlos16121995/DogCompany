using Dog_Company.Padroes;
using System;
using System.Collections.Generic;
using System.Data;

namespace Dog_Company.Models
{
    public class Cliente : IObserver
    {
        private string _cpf;
        private string _nome;
        private string _sobrenome;
        private string _celular;
        private string _cidade;
        private string _bairro;
        private string _rua;
        private int _numero;

        public Cliente()
        {
            _cpf = "";
            _nome = "";
            _sobrenome = "";
            _celular = "";
            _cidade = "";
            _bairro = "";
            _rua = "";
            _numero = 0;
        }

        public Cliente(string cpf, string nome, string sobrenome, string celular, string cidade, string bairro, string rua, int numero)
        {
            this.Cpf = cpf;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Celular = celular;
            this.Cidade = cidade;
            this.Bairro = bairro;
            this.Rua = rua;
            this.Numero = numero;
        }


        public string Cpf { get => _cpf; set => _cpf = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public string Sobrenome { get => _sobrenome; set => _sobrenome = value; }
        public string Celular { get => _celular; set => _celular = value; }
        public string Cidade { get => _cidade; set => _cidade = value; }
        public string Bairro { get => _bairro; set => _bairro = value; }
        public string Rua { get => _rua; set => _rua = value; }
        public int Numero { get => _numero; set => _numero = value; }

        public string Update(string nomeAnimal, bool adotado)
        {
            string msg = "";
            if (adotado == true)
            {
                msg = this._nome + ": O " + nomeAnimal + " esta pronto para ser adotado!";
            }
            
            return msg;    
        }


        public bool IsCpf()
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            _cpf = _cpf.Trim();
            _cpf = _cpf.Replace(".", "").Replace("-", "");
            if (_cpf.Length != 11)
                return false;
            tempCpf = _cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)

                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return _cpf.EndsWith(digito);
        }
   
        public void ValidaDados(out List<string> msg){
            msg = new List<string>();
            if (_cpf.Length > 11)
            {
                msg.Add("Insira um Cpf válido");
            }
            if (_nome == "")
            {
                msg.Add("Campo Nome não pode estar vazio!");
            }
            if(_sobrenome == "")
            {
                msg.Add("Campo Sobrenome não pode estar vazio!");
            }
            if(_celular == "")
            {
                msg.Add("Campo Celular não pode estar vazio!");
            }
            if(_cidade == "")
            {
                msg.Add("Campo Cidade não pode estar vazio!");
            }
            if(_bairro == "")
            {
                msg.Add("Campo Bairro não pode estar vazio!");
            }
            if(_rua == "")
            {
                msg.Add("Campo Rua não pode estar vazio!");
            }
            if(_numero == 0 || _numero < 0)
            {
                msg.Add("Insira um Número válido");
            }
        }

        public bool GravarDados()
        {
            string sql = "";
            DAL.MySqlPersistence bd = new DAL.MySqlPersistence();

            Dictionary<string, string> parametros = new Dictionary<string, string>();
            sql = "select count(*) from cliente where cpf = @cpf";
            parametros.Add("@cpf", _cpf);

            object count = bd.ExecutarScalar(sql, parametros, out string msg);

            if (Convert.ToInt32(count) == 0)
            {
                //insert
                sql = "insert into cliente (cpf, nome, sobrenome, celular, cidade, bairro, rua, numero) values (@cpf, @nome, @sobrenome, @celular, @cidade, @bairro, @rua, @numero)";

            }
            else
            {
                //update
                sql = "update  cliente set cpf = @cpf, nome = @nome, sobrenome = @sobrenome, celular = @celular, bairro = @bairro, rua = @rua, numero = @numero where cpf = @cpf";
            }

            parametros.Add("cpf", _cpf);
            parametros.Add("nome", _nome);
            parametros.Add("sobrenome", _sobrenome);
            parametros.Add("celular", _celular);
            parametros.Add("cidade", _cidade);
            parametros.Add("bairro", _bairro);
            parametros.Add("rua", _rua);
            parametros.Add("numero", _numero.ToString());

            int linhas = bd.ExecutarNonQuery(sql, parametros, out msg);
            
            
            return linhas > 0 ;
        }

        public void PesqByCpf(out bool existe)
        {
            existe = false;
            DAL.MySqlPersistence bd = new DAL.MySqlPersistence();
            Dictionary<string, string> parametros = new Dictionary<string, string>();
            string sql = "Select * from cliente where cpf = @cpf";
            parametros.Add("cpf", _cpf);

            DataTable dt = bd.ExecutarSelect(sql, parametros, out string msg);

            if(dt.Rows.Count == 1)
            {
                _nome = dt.Rows[0]["nome"].ToString();
                _sobrenome = dt.Rows[0]["sobrenome"].ToString();
                _celular = dt.Rows[0]["celular"].ToString();
                _cidade = dt.Rows[0]["cidade"].ToString();
                _bairro = dt.Rows[0]["bairro"].ToString();
                _rua = dt.Rows[0]["rua"].ToString();
                _numero = Convert.ToInt32(dt.Rows[0]["numero"]);

                existe = true;
            }
             
        }

        public void Remover (out bool excluiu)
        {
            excluiu = false;
            DAL.MySqlPersistence bd = new DAL.MySqlPersistence();
            Dictionary<string, string> parametros = new Dictionary<string, string>();
            string sql = "Delete from cliente where cpf = @cpf";
            parametros.Add("cpf", _cpf);
            excluiu = bd.ExecutarNonQuery(sql, parametros, out string msg) > 0;
        }
    }
}
