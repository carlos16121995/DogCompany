using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Dog_Company.Padroes;

namespace Dog_Company.Models
{
    public class Animal : ISujeito
    {
           

    private List<IObserver> _lo;
        private int _id;
        private string _nome;
        private float _valor;
        private string _mes_nascimento;
        private string _ano_nascimento;
        private string _cor;
        private bool _adotado;
        private string _especie;
        private string _raca;

        public Animal()
        {
            _lo = new List<IObserver>();
            _id = 0;
            _nome = "";
            _valor = 0;
            _mes_nascimento = "";
            _ano_nascimento = "";
            _cor = "";
            _adotado = false;
            _especie = "";
            _raca = "";
        }

        public Animal(int id, string nome, float valor, string mes_nascimento, string ano_nascimento, string cor, bool adotado, string especie, string raca)
        {
            _id = id;
            _nome = nome;
            _valor = valor;
            _mes_nascimento = mes_nascimento;
            _ano_nascimento = ano_nascimento;
            _cor = cor;
            _adotado = adotado;
            _especie = especie;
            _raca = raca;
        }

        internal List<IObserver> Lo { get => _lo; set => _lo = value; }
        public int Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public float Valor { get => _valor; set => _valor = value; }
        public string Mes_nascimento { get => _mes_nascimento; set => _mes_nascimento = value; }
        public string Ano_nascimento { get => _ano_nascimento; set => _ano_nascimento = value; }
        public string Cor { get => _cor; set => _cor = value; }
        public bool Adotado { get => _adotado; set => _adotado = value; }
        public string Especie { get => _especie; set => _especie = value; }
        public string Raca { get => _raca; set => _raca = value; }
        

        


        public void Adicionar(IObserver ob)
    {
            string sql = "";
            DAL.MySqlPersistenceSingleton bd = DAL.MySqlPersistenceSingleton.Conectar();


            Dictionary<string, string> parametros = new Dictionary<string, string>
            {
                { "cliente", ob.Cpf },
                { "animal", this._id.ToString() }
            };
            sql = "select count(*) from observer where cliente=@cliente and animal = @animal";
            object count = bd.ExecutarScalar(sql, parametros, out string msg);

            if(Convert.ToInt32(count) == 0)
            {
                sql = "insert into observer(cliente, animal) values(@cliente, @animal)";
                int linhas = bd.ExecutarNonQuery(sql, parametros, out msg);

            }

            //"select n.*, a.nome 
            //       from noticia n, autor a
            //       where a.id = n.autorid and
            //             n.titulo like @titulo";


            
        }

        public void Remover(IObserver ob)
        {
            string sql = "delete from observer where cliente=@cliente and animal = @animal";
            DAL.MySqlPersistenceSingleton bd = DAL.MySqlPersistenceSingleton.Conectar();

            Dictionary<string, string> parametros = new Dictionary<string, string>{
                { "cliente", ob.Cpf },
                { "animal", this._id.ToString() }
            };
            int linhas = bd.ExecutarNonQuery(sql, parametros, out string msg);
        }

        public List<string> Notificar()
        {
            string sql = "select count(*) from observer where animal = @id";
            DAL.MySqlPersistenceSingleton bd = DAL.MySqlPersistenceSingleton.Conectar();
            Dictionary<string, string> parametros = new Dictionary<string, string>{
                { "id", _id.ToString() }
            };
            List<string> lista = new List<string>();

            object count = bd.ExecutarScalar(sql, parametros, out string msg);

            if(Convert.ToInt32(count) > 0)
            {
                sql = "Select * from observer where animal = @id";
                DataTable dt = bd.ExecutarSelect(sql, parametros, out msg);

                foreach(DataRow row in dt.Rows)
                {
                    Cliente cliente = new Cliente();
                    cliente.Cpf = row["cliente"].ToString();
                    Dictionary<string, string> parametros2 = new Dictionary<string, string>{
                        { "cpf", cliente.Cpf.ToString() }
                    };
                    sql = "Select * from cliente where cpf = @cpf";
                    DataTable dt2 = bd.ExecutarSelect(sql, parametros2, out msg);
                    foreach(DataRow row2 in dt2.Rows)
                    {
                        cliente.Nome = row2["nome"].ToString();
                        cliente.Sobrenome = row2["sobrenome"].ToString();
                        cliente.Celular = row2["celular"].ToString();
                        cliente.Cidade = row2["cidade"].ToString();
                        cliente.Bairro = row2["bairro"].ToString();
                        cliente.Rua = row2["rua"].ToString();
                        cliente.Numero = Convert.ToInt32(row2["numero"]);
                    }

                    _lo.Add(cliente);
                    this.Remover(cliente);
                    msg = cliente.Update(_nome, _adotado);
                    lista.Add(msg);


                }
                
            }

            return lista;
        }


        public void PesquisarById(out bool existe)
        {
            existe = false;
            DAL.MySqlPersistenceSingleton bd = DAL.MySqlPersistenceSingleton.Conectar();
            string sql = "select * from animal where id =" + _id;
            DataTable dt = bd.ExecutarSelect(sql, out string msg);
            if (dt.Rows.Count == 1)
            {
                _nome = dt.Rows[0]["nome"].ToString();
                _valor = Convert.ToSingle(dt.Rows[0]["valor"]);
                _mes_nascimento = dt.Rows[0]["mes_nascimento"].ToString();
                _ano_nascimento = dt.Rows[0]["ano_nascimento"].ToString();
                _cor = dt.Rows[0]["cor"].ToString();
                _adotado = Convert.ToBoolean(dt.Rows[0]["adotado"]);
                _especie = dt.Rows[0]["especie"].ToString();
                _raca = dt.Rows[0]["raca"].ToString();
                existe = true;
            }
           
        }

        public void GravarBd()
        {
            string sql = "";
            DAL.MySqlPersistenceSingleton bd = DAL.MySqlPersistenceSingleton.Conectar();

            string temp = "0";
            if (_adotado)
            {
                temp = "1";
            }
            
            Dictionary<string, string> parametros = new Dictionary<string, string> {
                {"id", _id.ToString() },
                {"nome", _nome.ToString() },
                {"valor", Convert.ToString(_valor).Replace(',','.') },
                {"mes_nascimento", _mes_nascimento.ToString() },
                {"ano_nascimento", _ano_nascimento.ToString() },
                {"cor", _cor.ToString() },
                {"adotado", temp },
                {"especie", _especie.ToString() },
                {"raca", _raca.ToString() }
            };

            if(_id == 0)
            {
                sql = "insert into animal(nome, valor, mes_nascimento, ano_nascimento, cor, adotado, especie, raca) values(@nome, @valor, @mes_nascimento, @ano_nascimento, @cor, @adotado, @especie, @raca)";
            }
            else
            {
                sql = "update animal set nome = @nome, valor = @valor, mes_nascimento = @mes_nascimento, ano_nascimento = @ano_nascimento, cor = @cor, adotado = @adotado, especie = @especie, raca = @raca where id = @id";
            }
            int linhas = bd.ExecutarNonQuery(sql, parametros, out string msg);
            

        }

        public List<object> PegarTodos(out bool existe)
        {
            existe = false;   
            DAL.MySqlPersistenceSingleton bd = DAL.MySqlPersistenceSingleton.Conectar();
            List<object> an = new List<object>();
               
            string sql = "select * from animal";
            DataTable dt = bd.ExecutarSelect(sql, out string msg);
            if(dt.Rows.Count > 0){
                foreach (DataRow row in dt.Rows)
                {
                    Animal a = new Animal
                    {
                        _id = Convert.ToInt16(row["id"]),
                        _nome = row["nome"].ToString(),
                        _valor = Convert.ToSingle(dt.Rows[0]["valor"]),
                        _mes_nascimento = row["mes_nascimento"].ToString(),
                        _ano_nascimento = row["ano_nascimento"].ToString(),
                        _cor = row["cor"].ToString(),
                        _adotado = Convert.ToBoolean(row["adotado"]),
                        _especie = row["especie"].ToString(),
                        _raca = row["raca"].ToString()
                    };
                    an.Add(a);
                }
            existe = true;
            }
                
            return an;
        }


        
    }
}
