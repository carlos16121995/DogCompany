using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dog_Company.Models
{
    public class Venda
    {
        private int _id;
        private Cliente _cliente;
        private float _vl_total;
        private DateTime _data;
        private List<Animal> ItensVenda;

        public Venda(int id, Cliente cliente, float vl_total, DateTime data, List<Animal> itensVenda)
        {
            _id = id;
            _cliente = cliente;
            _vl_total = vl_total;
            _data = data;
            ItensVenda = itensVenda;
        }

        public int Id { get => _id; set => _id = value; }
        public Cliente Cliente { get => _cliente; set => _cliente = value; }
        public float Vl_total { get => _vl_total; set => _vl_total = value; }
        public DateTime Data { get => _data; set => _data = value; }
        public List<Animal> ItensVenda1 { get => ItensVenda; set => ItensVenda = value; }
    }
}
