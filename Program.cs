using System;
using System.Collections.Generic;
using System.Linq;


class Carchejelero_Elecciones
{
    static List<Distrito> distritos = new List<Distrito>
    {
        new Distrito(1, 194, 48, 206, 45),
        new Distrito(2, 180, 20, 320, 16),
        new Distrito(3, 221, 90, 140, 20),
        new Distrito(4, 432, 50, 821, 14),
        new Distrito(5, 820, 61, 946, 18)
    };

    static Dictionary<string, int> candidatos = new Dictionary<string, int>();
    static int totalGeneral = 0;


    // Método principal
    static void Main()
    {
        // Calcular totales una vez al inicio
        CalcularTotales();

        bool salir = false;
        while (!salir)
        {
            Console.Clear();
            MostrarMenu();

            var opcion = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (opcion)
            {
                case '1':
                    Console.Clear();
                    MostrarTabla();
                    VolverAlMenu();
                    break;
                case '2':
                    Console.Clear();
                    MostrarResultados();
                    VolverAlMenu();
                    break;
                case '3':
                    Console.Clear();
                    MostrarMasVotado();
                    VolverAlMenu();
                    break;
                case '4':
                    Console.Clear();
                    VerificarGanador();
                    VolverAlMenu();
                    break;
                case '5':
                    Console.Clear();
                    MostrarSegundaRonda();
                    VolverAlMenu();
                    break;
                case '6':
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                    System.Threading.Thread.Sleep(1000);
                    break;
            }
        }
    }
    // Método para calcular los totales de votos por candidato
    static void CalcularTotales()
    {
        candidatos["A"] = distritos.Sum(d => d.VotosA);
        candidatos["B"] = distritos.Sum(d => d.VotosB);
        candidatos["C"] = distritos.Sum(d => d.VotosC);
        candidatos["D"] = distritos.Sum(d => d.VotosD);
        totalGeneral = candidatos.Values.Sum();
    }
    // Método para mostrar el menú principal
    static void MostrarMenu()
    {
        Console.WriteLine("=== MENÚ DE ANÁLISIS DE VOTACIÓN ===");
        Console.WriteLine("1. Mostrar tabla de votos por distrito");
        Console.WriteLine("2. Mostrar resultados totales y porcentajes");
        Console.WriteLine("3. Mostrar candidato más votado");
        Console.WriteLine("4. Verificar si hay ganador directo");
        Console.WriteLine("5. Mostrar candidatos para segunda ronda");
        Console.WriteLine("6. Salir");
        Console.Write("Seleccione una opción: ");
    }
    // Método para mostrar la tabla de votos por distrito
    static void MostrarTabla()
    {
        Console.WriteLine("| Distrito , Candidato A , Candidato B , Candidato C , Candidato D |");
        Console.WriteLine("|----------|-------------|-------------|-------------|-------------|");

        foreach (var distrito in distritos)
        {
            Console.WriteLine($"| {distrito.Numero,-8} | {distrito.VotosA,-11} | {distrito.VotosB,-11} | {distrito.VotosC,-11} | {distrito.VotosD,-11} |");
        }
    }
    // Método para mostrar los resultados totales y porcentajes
    static void MostrarResultados()
    {
        Console.WriteLine("RESULTADOS DE LA VOTACIÓN:");
        Console.WriteLine($"Total de votos emitidos: {totalGeneral}\n");

        foreach (var candidato in candidatos)
        {
            double porcentaje = (double)candidato.Value / totalGeneral * 100;
            Console.WriteLine($"Candidato {candidato.Key}: {candidato.Value} votos ({porcentaje:F2}%)");
        }
    }
    //
    static void MostrarMasVotado()
    {
        var masVotado = candidatos.OrderByDescending(x => x.Value).First();
        Console.WriteLine($"El candidato más votado es: {masVotado.Key} con {masVotado.Value} votos");
    }
    /// Método para verificar si hay un ganador directo
    static void VerificarGanador()
    {
        var masVotado = candidatos.OrderByDescending(x => x.Value).First();
        double porcentaje = (double)masVotado.Value / totalGeneral * 100;

        if (porcentaje > 50)
        {
            Console.WriteLine($"¡El candidato {masVotado.Key} ha ganado las elecciones con {porcentaje:F2}% de los votos!");
        }
        else
        {
            Console.WriteLine($"Ningún candidato obtuvo más del 50% de los votos.");
            Console.WriteLine($"El candidato más votado ({masVotado.Key}) obtuvo {porcentaje:F2}%.");
        }
    }









    /// Método para mostrar los candidatos que pasan a segunda ronda
    static void MostrarSegundaRonda()
    {
        var masVotados = candidatos.OrderByDescending(x => x.Value).Take(2).ToList();
        Console.WriteLine("CANDIDATOS QUE PASAN A SEGUNDA RONDA:");

        for (int i = 0; i < masVotados.Count; i++)
        {
            double porcentaje = (double)masVotados[i].Value / totalGeneral * 100;
            Console.WriteLine($"{i + 1}. Candidato {masVotados[i].Key} con {masVotados[i].Value} votos ({porcentaje:F2}%)");
        }
    }













    /// Método para pausar la consola y volver al menú  
    static void VolverAlMenu()
    {
        Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
        Console.ReadKey();
    }
}

class Distrito
{
    public int Numero { get; }
    public int VotosA { get; }
    public int VotosB { get; }
    public int VotosC { get; }
    public int VotosD { get; }

    public Distrito(int numero, int votosA, int votosB, int votosC, int votosD)
    {
        Numero = numero;
        VotosA = votosA;
        VotosB = votosB;
        VotosC = votosC;
        VotosD = votosD;
    }
}