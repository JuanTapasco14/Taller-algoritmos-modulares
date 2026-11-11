using System;

namespace TallerAlgoritmosModulares
{
    class Program
    {
        // Menú principal (Orquestador)
        static void Main(string[] Args)
        {
            int Opcion = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("=== MENÚ PRINCIPAL ===");
                Console.WriteLine("1. Turnos IPS");
                Console.WriteLine("2. Control Académico");
                Console.WriteLine("3. Producción Fábrica");
                Console.WriteLine("4. Salir");
                Console.Write("Opción: ");
                Opcion = int.Parse(Console.ReadLine());

                if (Opcion == 1) Caso1_IPS();
                else if (Opcion == 2) Caso3_Academico();
                else if (Opcion == 3) Caso5_Produccion();

            } while (Opcion != 4);
        }

        // Caso 1: IPS (Uso de Vectores)
        static void Caso1_IPS()
        {
            string[] Doc = new string[30];
            string[] Nom = new string[30];
            int[] Edad = new int[30];
            int[] Tipo = new int[30];
            int[] Prio = new int[30];
            int Total = 0;
            int SubOpcion = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("--- CASO 1: IPS ---");
                Console.WriteLine("1. Registrar | 2. Listar | 3. Buscar | 4. Conteo | 5. Mayor Prioridad | 6. Volver");
                Console.Write("Opción: ");
                SubOpcion = int.Parse(Console.ReadLine());

                if (SubOpcion == 1 && Total < 30)
                {
                    Console.Write("Documento: "); Doc[Total] = Console.ReadLine();
                    Console.Write("Nombre: "); Nom[Total] = Console.ReadLine();
                    Console.Write("Edad: "); Edad[Total] = int.Parse(Console.ReadLine());
                    Console.Write("Tipo (1-Urg, 2-Gen, 3-Prio): "); Tipo[Total] = int.Parse(Console.ReadLine());
                    Console.Write("Prioridad (1-5): "); Prio[Total] = int.Parse(Console.ReadLine());
                    Total++;
                }
                else if (SubOpcion == 2)
                {
                    for (int I = 0; I < Total; I++)
                        Console.WriteLine(Doc[I] + " | " + Nom[I] + " | Edad: " + Edad[I] + " | Tipo: " + Tipo[I] + " | Prio: " + Prio[I]);
                }
                else if (SubOpcion == 3)
                {
                    Console.Write("Documento a buscar: ");
                    string Buscado = Console.ReadLine();
                    for (int I = 0; I < Total; I++)
                    {
                        if (Doc[I] == Buscado)
                            Console.WriteLine("Encontrado: " + Nom[I] + " | Tipo: " + Tipo[I] + " | Prio: " + Prio[I]);
                    }
                }
                else if (SubOpcion == 4)
                {
                    int Urg = 0, Gen = 0, Pri = 0;
                    for (int I = 0; I < Total; I++)
                    {
                        if (Tipo[I] == 1) Urg++;
                        else if (Tipo[I] == 2) Gen++;
                        else if (Tipo[I] == 3) Pri++;
                    }
                    Console.WriteLine("Urgencias: " + Urg + " | General: " + Gen + " | Prioritaria: " + Pri);
                }
                else if (SubOpcion == 5 && Total > 0)
                {
                    int PosMayor = 0;
                    for (int I = 1; I < Total; I++)
                        if (Prio[I] > Prio[PosMayor]) PosMayor = I;
                    Console.WriteLine("Mayor Prioridad: " + Nom[PosMayor] + " (Nivel " + Prio[PosMayor] + ")");
                }

                if (SubOpcion != 6) { Console.WriteLine("Presione ENTER para continuar..."); Console.ReadLine(); }
            } while (SubOpcion != 6);
        }

        // Caso 3: Control Académico (Vectores y Matriz)
        static void Caso3_Academico()
        {
            string[] Cod = new string[25];
            string[] Nom = new string[25];
            double[,] Notas = new double[25, 3];
            double[] Asis = new double[25];
            int Total = 0;
            int SubOpcion = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("--- CASO 3: ACADÉMICO ---");
                Console.WriteLine("1. Registrar | 2. Consolidado | 3. Promedio Grupo | 4. Mejor/Peor | 5. Volver");
                Console.Write("Opción: ");
                SubOpcion = int.Parse(Console.ReadLine());

                if (SubOpcion == 1 && Total < 25)
                {
                    Console.Write("Código: "); Cod[Total] = Console.ReadLine();
                    Console.Write("Nombre: "); Nom[Total] = Console.ReadLine();
                    for (int J = 0; J < 3; J++)
                    {
                        Console.Write("Nota " + (J + 1) + ": ");
                        Notas[Total, J] = double.Parse(Console.ReadLine());
                    }
                    Console.Write("Asistencia %: "); Asis[Total] = double.Parse(Console.ReadLine());
                    Total++;
                }
                else if (SubOpcion == 2)
                {
                    for (int I = 0; I < Total; I++)
                    {
                        double Def = CalcularDefinitiva(Notas, I);
                        Console.WriteLine(Cod[I] + " | " + Nom[I] + " | Def: " + Math.Round(Def, 2) + " | " + DeterminarEstado(Def) + " | Asis: " + Asis[I] + "%");
                    }
                }
                else if (SubOpcion == 3 && Total > 0)
                {
                    double Suma = 0;
                    for (int I = 0; I < Total; I++) Suma += CalcularDefinitiva(Notas, I);
                    Console.WriteLine("Promedio Grupo: " + Math.Round(Suma / Total, 2));
                }
                else if (SubOpcion == 4 && Total > 0)
                {
                    int Mejor = 0, Peor = 0;
                    for (int I = 1; I < Total; I++)
                    {
                        if (CalcularDefinitiva(Notas, I) > CalcularDefinitiva(Notas, Mejor)) Mejor = I;
                        if (CalcularDefinitiva(Notas, I) < CalcularDefinitiva(Notas, Peor)) Peor = I;
                    }
                    Console.WriteLine("Mejor: " + Nom[Mejor] + " (" + Math.Round(CalcularDefinitiva(Notas, Mejor), 2) + ")");
                    Console.WriteLine("Peor:  " + Nom[Peor] + " (" + Math.Round(CalcularDefinitiva(Notas, Peor), 2) + ")");
                }

                if (SubOpcion != 5) { Console.WriteLine("Presione ENTER para continuar..."); Console.ReadLine(); }
            } while (SubOpcion != 5);
        }

        static double CalcularDefinitiva(double[,] Notas, int Fila)
        {
            return (Notas[Fila, 0] + Notas[Fila, 1] + Notas[Fila, 2]) / 3.0;
        }

        static string DeterminarEstado(double Def)
        {
            if (Def >= 3.0) return "Aprobado";
            if (Def >= 2.0) return "Habilita";
            return "Reprueba";
        }

        // Caso 5: Producción (Vectores y Matriz)
        static void Caso5_Produccion()
        {
            string[] Cod = new string[10];
            string[] Nom = new string[10];
            int[,] Matriz = new int[10, 5];
            int Total = 0;
            int SubOpcion = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("--- CASO 5: PRODUCCIÓN ---");
                Console.WriteLine("1. Registrar Op | 2. Cargar Prod | 3. Reporte | 4. Mejor Op | 5. Alertas | 6. Volver");
                Console.Write("Opción: ");
                SubOpcion = int.Parse(Console.ReadLine());

                if (SubOpcion == 1 && Total < 10)
                {
                    Console.Write("Código: "); Cod[Total] = Console.ReadLine();
                    Console.Write("Nombre: "); Nom[Total] = Console.ReadLine();
                    Total++;
                }
                else if (SubOpcion == 2 && Total > 0)
                {
                    for (int I = 0; I < Total; I++)
                    {
                        Console.WriteLine("Operario: " + Nom[I]);
                        for (int J = 0; J < 5; J++)
                        {
                            Console.Write("Día " + (J + 1) + ": ");
                            Matriz[I, J] = int.Parse(Console.ReadLine());
                        }
                    }
                }
                else if (SubOpcion == 3)
                {
                    for (int I = 0; I < Total; I++)
                        Console.WriteLine(Cod[I] + " | " + Nom[I] + " | Total Semanal: " + CalcularTotalSemana(Matriz, I));
                }
                else if (SubOpcion == 4 && Total > 0)
                {
                    int Mejor = 0;
                    for (int I = 1; I < Total; I++)
                        if (CalcularTotalSemana(Matriz, I) > CalcularTotalSemana(Matriz, Mejor)) Mejor = I;
                    Console.WriteLine("Mejor Operario: " + Nom[Mejor] + " (" + CalcularTotalSemana(Matriz, Mejor) + " unids)");
                }
                else if (SubOpcion == 5 && Total > 0)
                {
                    double SumaTotal = 0;
                    double[] SumaDia = new double[5];

                    for (int I = 0; I < Total; I++)
                    {
                        for (int J = 0; J < 5; J++)
                        {
                            SumaTotal += Matriz[I, J];
                            SumaDia[J] += Matriz[I, J];
                        }
                    }

                    double PromedioGeneral = SumaTotal / (Total * 5);
                    Console.WriteLine("Promedio Diario General: " + Math.Round(PromedioGeneral, 2));

                    string[] Dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes" };
                    for (int J = 0; J < 5; J++)
                    {
                        double PromDia = SumaDia[J] / Total;
                        if (PromDia < PromedioGeneral)
                            Console.WriteLine(Dias[J] + ": " + Math.Round(PromDia, 2) + " -> ALERTA BAJA PRODUCCIÓN");
                        else
                            Console.WriteLine(Dias[J] + ": " + Math.Round(PromDia, 2) + " -> NORMAL");
                    }
                }

                if (SubOpcion != 6) { Console.WriteLine("Presione ENTER para continuar..."); Console.ReadLine(); }
            } while (SubOpcion != 6);
        }

        static int CalcularTotalSemana(int[,] Matriz, int Fila)
        {
            int Suma = 0;
            for (int J = 0; J < 5; J++) Suma += Matriz[Fila, J];
            return Suma;
        }
    }
}