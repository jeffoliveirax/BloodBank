﻿namespace BloodBank.Core.Entities
{
    public class Doacao : BaseEntity
    {
        protected Doacao() { }
        public Doacao(int doadorId, decimal volume) : base()
        {
            DoadorId = doadorId;
            Data = DateTime.Now;
            Volume = volume;
        }

        public int DoadorId { get; private set; }
        public DateTime Data { get; private set; }
        public decimal Volume { get; private set; }
        public Doador Doador { get; private set; }

        public void Update(int doadorId, decimal volume)
        {
            DoadorId = doadorId;
            Data = DateTime.Now;
            Volume = volume;
        }

    }

}
