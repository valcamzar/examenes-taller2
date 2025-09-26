using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExamenP1_T2
{
    internal class Jugador
    {
        public string nombre;
        public int vida;
        private int damageDealt;

        public Jugador(string nombre, int vida, int damage) 
        {
            nombre = nombre;
            vida = vida;
            if (vida < 0) vida = 0;
            if (vida > 100) vida = 100;

            damageDealt = damage;
            if (damageDealt < 0) damageDealt = 0;
            if (damageDealt > 100) damageDealt = 100;
        }

        public void damageTaken(int cantidad) 
        {
            if (cantidad < 0) cantidad = 0;
            vida = vida - cantidad;
            if (vida < 0) vida = 0;
        }
        public int damageObtained()
        {
            return damageDealt;
        }

    }
}
