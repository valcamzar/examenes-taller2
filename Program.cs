using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenP1_T2
{
    internal class Program
    {
        static Random rng = new Random();
        static void Main(string[] args)
        {
            bool seguir = true;

            while (seguir)
            {
                Console.Clear();
                Console.WriteLine(" Welcome to RPG Combat! :3 ");
                Console.WriteLine();

                Console.Write("Nombre del jugador: ");
                string nombreJ = Console.ReadLine();
                int vidaJ = LeerEnteroRango("Vida del jugador (1-100): ", 1, 100);
                int damageJ = LeerEnteroRango("Daño del jugador (1-100): ", 1, 100);

                Jugador jugador = new Jugador(nombreJ, vidaJ, damageJ);

                Console.WriteLine();
                int cantidadEnemigos = LeerEnteroRango("Cuántos enemigos habrá? (1-20): ", 1, 20);

                List<Enemigo> enemigos = new List<Enemigo>();
                for (int i = 0; i < cantidadEnemigos; i++)
                {
                    Console.WriteLine();
                    Console.WriteLine("Enemigo #" + (i + 1));
                    int vidaE = LeerEnteroRango("  Vida (1-100): ", 1, 100);
                    int damageE = LeerEnteroRango("  Damage (1-100): ", 1, 100);
                    Enemigo e = new Enemigo("Enemigo " + (i + 1), vidaE, damageE);
                    enemigos.Add(e);
                }

                Console.WriteLine();
                Console.WriteLine("Let the battle begin!");
                Console.WriteLine();

                while (jugador.vida > 0 && HayEnemigosVivos(enemigos))
                {
                    MostrarEstado(jugador, enemigos);

                    List<int> indicesVivos = IndicesEnemigosVivos(enemigos);
                    Console.WriteLine("Elige un enemigo para atacar (escribe el índice):");
                    for (int i = 0; i < indicesVivos.Count; i++)
                    {
                        int idx = indicesVivos[i];
                        Console.WriteLine("  [" + i + "] " + enemigos[idx].nombre + "  Vida: " + enemigos[idx].vida);
                    }

                    int opcion = LeerEnteroRango("Tu elección: ", 0, indicesVivos.Count - 1);
                    int idxObjetivo = indicesVivos[opcion];

                    int damage = jugador.damageObtained();
                    enemigos[idxObjetivo].damageTaken(damage);

                    Console.WriteLine();
                    Console.WriteLine(jugador.nombre + " ataca a " + enemigos[idxObjetivo].nombre + " por " + damage + " de daño.");
                    Console.WriteLine("Vida de " + enemigos[idxObjetivo].nombre + ": " + enemigos[idxObjetivo].vida);

                    if (!enemigos[idxObjetivo].estaVivo())
                    {
                        Console.WriteLine("-> " + enemigos[idxObjetivo].nombre + " ha muerto.");
                    }

                    if (!HayEnemigosVivos(enemigos))
                    {
                        break;
                    }

                    int idxAtacante = ElegirEnemigoVivoAlAzar(enemigos);
                    int damageEnemigo = enemigos[idxAtacante].damageObtained();
                    jugador.damageTaken(damageEnemigo);

                    Console.WriteLine(enemigos[idxAtacante].nombre + " ataca a " + jugador.nombre + " por " + damageEnemigo + ".");
                    Console.WriteLine("Vida de " + jugador.nombre + ": " + jugador.vida);
                    Console.WriteLine();
                }

                if (jugador.vida <= 0)
                {
                    Console.WriteLine("DEFEAT!.");
                }
                else
                {
                    Console.WriteLine("VICTORY!.");
                }

                Console.WriteLine();
                Console.Write("¿Reiniciar partida? (S/N): ");
                string r = Console.ReadLine();
                if (r == null || r.Trim().ToUpper() != "S")
                {
                    seguir = false;
                }
            }

            Console.WriteLine();
            Console.WriteLine("¡Fin!");
        }

        static int LeerEnteroRango(string texto, int min, int max)
        {
            int valor;
            bool ok = false;
            do
            {
                Console.Write(texto);
                string s = Console.ReadLine();
                ok = int.TryParse(s, out valor);
                if (!ok || valor < min || valor > max)
                {
                    Console.WriteLine("  Ingresa un número entre " + min + " y " + max + ".");
                    ok = false;
                }
            } while (!ok);
            return valor;
        }

        static bool HayEnemigosVivos(List<Enemigo> enemigos)
        {
            for (int i = 0; i < enemigos.Count; i++)
            {
                if (enemigos[i].estaVivo()) return true;
            }
            return false;
        }

        static List<int> IndicesEnemigosVivos(List<Enemigo> enemigos)
        {
            List<int> vivos = new List<int>();
            for (int i = 0; i < enemigos.Count; i++)
            {
                if (enemigos[i].estaVivo()) vivos.Add(i);
            }
            return vivos;
        }

        static int ElegirEnemigoVivoAlAzar(List<Enemigo> enemigos)
        {
            while (true)
            {
                int idx = rng.Next(0, enemigos.Count);
                if (enemigos[idx].estaVivo()) return idx;
            }
        }

        static void MostrarEstado(Jugador jugador, List<Enemigo> enemigos)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Jugador: " + jugador.nombre + " | Vida: " + jugador.vida + " | Daño: " + jugador.damageObtained());
            Console.WriteLine("Enemigos:");
            for (int i = 0; i < enemigos.Count; i++)
            {
                string estado = enemigos[i].estaVivo() ? "Vivo" : "Muerto";
                Console.WriteLine("  " + (i + 1) + ". " + enemigos[i].nombre + " | Vida: " + enemigos[i].vida + " | Daño: " + enemigos[i].damageObtained() + " | " + estado);
            }
            Console.WriteLine("----------------------------------");
            Console.WriteLine();
        }
    }
}
