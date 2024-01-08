using System;
using System.Threading;

class Morpion
{
    static void Main()
    {
        int[,] grille = new int[3, 3];
        bool gagner = false;
        int joueur = 1;
        int essais = 0;
        bool bonnePosition = false;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                grille[i, j] = 10;
            }
        }

        while (!gagner)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Joueur {0}, choisissez votre position:", joueur);
                AfficherMorpion(0, 0);

                int l = -1;
                int c = -1;

                do
                {
                    Console.Write("Ligne (1 - 3):");
                    try
                    {
                        l = int.Parse(Console.ReadLine()) - 1;
                        if (l >= 0 & l <= 2) { break; }
                        else { Console.WriteLine("Valeur non valide!"); }
                    }
                    catch (Exception) { Console.WriteLine("Valeur non valide!"); }
                } while (true);

                do
                {
                    Console.Write("Colonne (1 - 3):");
                    try
                    {
                        c = int.Parse(Console.ReadLine()) - 1;
                        if (c >= 0 & c <= 2) { break; }
                        else { Console.WriteLine("Valeur non valide!"); }
                    }
                    catch (Exception) { Console.WriteLine("Valeur non valide!"); }
                } while (true);

                if (grille[l, c] == 10) { bonnePosition = true; }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Position invalide!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(400);
                }

                if (bonnePosition)
                {
                    AJouer(l, c, joueur);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            if (Gagner(0, 0, joueur))
            {
                Console.Clear();
                gagner = true;
                Console.WriteLine("Joueur {0} à gagner!\nEssais: {1}", joueur, essais);
                AfficherMorpion(0, 0);
                break;
            }

            if (bonnePosition)
            {
                bonnePosition = false;
                essais++;
                if (joueur == 1)
                {
                    joueur = 2;
                }
                else
                {
                    joueur = 1;
                }
            }

            if (essais >= 9)
            {
                Console.Clear();
                Console.WriteLine("Match Nul! Personne ne gagne!\nEssais: {0}", essais);
                AfficherMorpion(0, 0);
                break;
            }
        }

        Console.ReadKey();
        Main();
    }

	static void AJouer(int l, int c, int joueur)
	{
	    grille[l, c] = joueur;
	}
	
	static bool Gagner(int l, int c, int joueur)
	{
	    // Vérification des lignes
	    for (int i = 0; i < 3; i++)
	    {
	        if (grille[i, 0] == joueur && grille[i, 1] == joueur && grille[i, 2] == joueur)
	        {
	            return true;
	        }
	    }
	
	    // Vérification des colonnes
	    for (int i = 0; i < 3; i++)
	    {
	        if (grille[0, i] == joueur && grille[1, i] == joueur && grille[2, i] == joueur)
	        {
	            return true;
	        }
	    }
	
	    // Vérification des diagonales
	    if (grille[0, 0] == joueur && grille[1, 1] == joueur && grille[2, 2] == joueur)
	    {
	        return true;
	    }
	
	    if (grille[0, 2] == joueur && grille[1, 1] == joueur && grille[2, 0] == joueur)
	    {
	        return true;
	    }
	
	    return false;
	}
	
	static void AfficherMorpion(int l, int c)
	{
	    Console.ForegroundColor = ConsoleColor.DarkCyan;
	    Console.WriteLine("\n");
	
	    for (int i = 0; i < 3; i++)
	    {
	        for (int j = 0; j < 3; j++)
	        {
	            if (grille[i, j] == 10)
	            {
	                Console.Write(".");
	            }
	            else if (grille[i, j] == 1)
	            {
	                Console.Write("X");
	            }
	            else if (grille[i, j] == 2)
	            {
	                Console.Write("O");
	            }
	
	            if (j < 2)
	            {
	                Console.Write(" | ");
	            }
	        }
	
	        if (i < 2)
	        {
	            Console.WriteLine("\n-------------");
	        }
	    }
	
	    Console.ForegroundColor = ConsoleColor.White;
	}
}