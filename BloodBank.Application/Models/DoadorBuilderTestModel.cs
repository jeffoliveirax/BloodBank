using BloodBank.Core.Entities;
using BloodBank.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BloodBank.Application.Models
{
    public class DoadorBuilderTestModel
    {
        private int _id = 1;
        private string _nomeCompleto = "John Doe";

        public DoadorBuilderTestModel ComId(int id)
        {
            _id = id;
            return this;
        }

        public DoadorBuilderTestModel ComNome(string nome)
        {
            _nomeCompleto = nome;
            return this;
        }

        public Doador Build()
        {
            var doador = new Doador
            {
                NomeCompleto = _nomeCompleto
            };

            // Configura o Id usando reflexão
            typeof(Doador).GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(doador, _id); // Verificação para evitar NullReferenceException

            return doador;
        }

    }
}
