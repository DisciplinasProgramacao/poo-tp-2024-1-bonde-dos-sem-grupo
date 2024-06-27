using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Danilo_sFood
{
    public class Mesa
    {
        private int idMesa;
        private int lugaresMesa;
        private bool status;

        public Mesa(int idMesa, int lugaresMesa, bool status)
        {
            this.idMesa = idMesa;
            this.lugaresMesa = lugaresMesa;
            this.status = status;
        }

        public int GetIdMesa()
        {
            return idMesa;
        }

        public int GetLugaresMesa()
        {
            return lugaresMesa;
        }

        public bool GetStatus()
        {
            return status;
        }

        public void SetStatus(bool status)
        {
            this.status = status;
        }

        public bool VerificaDisponibilidade()
        {
            return status;
        }
    }

}
