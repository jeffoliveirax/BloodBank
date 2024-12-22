using BloodBank.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BloodBank.Application.Models
{
    public class DoacaoBuilderTestModel
    {
        private int _id = 1;
        private int _doadorId = 1;
        private DateTime _data = DateTime.Today;
        private decimal _volume = 450;
        private Doador _doador = new Doador { NomeCompleto = "John Doe" };

        public DoacaoBuilderTestModel ComId(int id)
        {
            _id = id;
            return this;
        }

        public DoacaoBuilderTestModel ComDoadorId(int doadorId)
        {
            _doadorId = doadorId;
            return this;
        }

        public DoacaoBuilderTestModel ComData(DateTime data)
        {
            _data = data;
            return this;
        }

        public DoacaoBuilderTestModel ComVolume(decimal volume)
        {
            _volume = volume;
            return this;
        }

        public DoacaoBuilderTestModel ComDoador(Doador doador)
        {
            _doador = doador;
            _doadorId = doador.Id;
            return this;
        }

        public Doacao Build()
        {
            var doacao = new Doacao(_doadorId, _volume)
            {
                Data = _data,
                Doador = _doador
            };

            typeof(Doacao).GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(doacao, _id); // Verificação para evitar NullReferenceException

            return doacao;
        }
    }


}
