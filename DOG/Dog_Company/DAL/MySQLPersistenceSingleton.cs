using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Dog_Company.DAL
{
    public class MySqlPersistenceSingleton
    {
        private static MySqlPersistenceSingleton inst = null;
        private static MySqlConnection _conexao = null;

        private MySqlPersistenceSingleton()
        {
          
                string strcon = "Server=den1.mysql5.gear.host; Port=3306;Database=dog;Uid=dog;pwd=P@123456789;SslMode=none";
                _conexao = new MySqlConnection(strcon);
 
        }

        public static MySqlPersistenceSingleton Conectar()
        {
            if(inst == null)
            {
                inst = new MySqlPersistenceSingleton();
            }
            return inst;

        }

        public void Abrir()
        {
            _conexao.Open();
        }

        public void Fechar()
        {
            _conexao.Close();
        }

        /// <summary>
        /// Executa um select com os parametros já definidos
        /// </summary>
        /// <param name="select">String com o select</param>
        /// <param name="msgErro">Menssagem caso ocorra algum erro</param>
        /// <returns>Datatable com o dados</returns>
        public DataTable ExecutarSelect(string select, out string msgErro)
        {
            msgErro = "";

            DataTable dt = new DataTable();
            MySqlCommand cmd = _conexao.CreateCommand();
            cmd.CommandText = select;

            try
            { 
                Abrir();
                dt.Load(cmd.ExecuteReader());
            }
            catch(Exception ex){
                msgErro = ex.Message;
            }
            finally
            {
                Fechar();
            }
            return dt;
        }

        /// <summary>
        /// Executa um SELECT com parametros
        /// </summary>
        /// <param name="select">String com Select</param>
        /// <param name="parametros">Lista de parametos</param>
        /// <param name="msgErro">Menssagem caso aconteça algum erro</param>
        /// <returns>DataTable com os dados</returns>
        /// 
        public DataTable ExecutarSelect(string select, Dictionary<string, string> parametros, out string msgErro)
        {
            msgErro = "";
            MySqlCommand cmd = _conexao.CreateCommand(); ;
           

            foreach (string key in parametros.Keys)
            {
                cmd.Parameters.AddWithValue(key, parametros[key]);
            }
            cmd.CommandText = select;
            DataTable dt = new DataTable();

            try
            {
                Abrir();
                dt.Load(cmd.ExecuteReader());
            }
            catch(Exception ex)
            {
                msgErro = ex.Message;
            }
            finally
            {
                Fechar();
            }
            return dt;
        }

        /// <summary>
        /// Executa Insert, Update e delete
        /// </summary>
        /// <param name="sql">String com o Insert, Update ou Delete</param>
        /// <param name="msgErro">Menssagem caso ocorra algum erro</param>
        /// <returns>DataTable com os dados</returns>
        public int ExecutarNonQuery(string sql, out string msgErro)
        {
            msgErro = "";
            int linhasAfetadas = 0;
            MySqlCommand cmd = _conexao.CreateCommand();
            cmd.CommandText = sql;
            try
            {
                Abrir();
                linhasAfetadas = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                msgErro = ex.Message;
            }
            finally
            {
                Fechar();
            }
            return linhasAfetadas;
        }

        /// <summary>
        /// Executa Insert,Update e Delete com parametros
        /// </summary>
        /// <param name="sql"> String com Insert, Update e Delete</param>
        /// <param name="parametros">Lista de parametros</param>
        /// <param name="msgErro">Menssagem caso ocorra algum erro</param>
        /// <returns> Quantidade de linhas afetadas</returns>
        public int ExecutarNonQuery(string sql, Dictionary<string, string> parametros, out string msgErro)
        {
            msgErro = "";
            MySqlCommand cmd = _conexao.CreateCommand();
            
            foreach (string Key in parametros.Keys)
            {
                cmd.Parameters.AddWithValue(Key, parametros[Key]);
            }
            cmd.CommandText = sql;
            int linhasAfetadas = 0;
            try
            {
                Abrir();
                linhasAfetadas = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                msgErro = ex.Message;
            }
            finally
            {
                Fechar();
            }
            return linhasAfetadas;
        }

        /// <summary>
        /// Executa um select que retorna um unico objeto
        /// </summary>
        /// <param name="select">String com um select</param>
        /// <param name="msgErro">Menssagem caso ocorra algum erro</param>
        /// <returns>Retorna um unico objeto</returns>
        public object ExecutarScalar(string select, out string msgErro)
        {
            msgErro = "";
            MySqlCommand cmd = _conexao.CreateCommand();
            cmd.CommandText = select;
            object obj = null;
            try
            {
                Abrir();
                obj = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                msgErro = ex.Message;
            }
            finally
            {
                Fechar();
            }
            return obj;

        }

        /// <summary>
        /// Executa um Select com parametros que retorna apenas um resultado
        /// </summary>
        /// <param name="select">String com o select</param>
        /// <param name="parametros">Lista de parametros</param>
        /// <param name="msgErro">Messagem caso ocorra algum erro</param>
        /// <returns>Um objeto</returns>
        public object ExecutarScalar(string select, Dictionary<string,string> parametros, out string msgErro)
        {
            msgErro = "";
            MySqlCommand cmd = _conexao.CreateCommand();
            cmd.CommandText = select;
            object obj = null;
            try
            {
                Abrir();
                foreach (string Key in parametros.Keys)
                {
                    cmd.Parameters.AddWithValue(Key, parametros[Key]);
                }

                obj = cmd.ExecuteScalar();
                
            }
            catch(Exception ex)
            {
                msgErro = ex.Message;
            }
            finally
            {
                Fechar();
            }

            return obj;
            
        }
    }
}
