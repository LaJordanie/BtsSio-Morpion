﻿/*
 * Created by SharpDevelop.
 * User: m.becker
 * Date: 13/10/2023
 * Time: 11:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace Morpion
{
    class Program
    {
		public static int[,] grille = new int[3, 3];
        
        public static void AfficherMorpion(int j, int k)
        {
        	for(int y=0;y<grille.GetLength(1);y++)
        	{
	        	for(int x=0;x<grille.GetLength(0);x++)
	        	{
	        		if(grille[y,x]==1){
	        			Console.Write("[O]");
	        		}
	        		else if(grille[y,x]==2){
	        			Console.Write("[X]");
	        		}
	        		else{
	        			Console.Write("[ ]");
	        		}
	        	}
	        	Console.WriteLine();
        	}
        }
        public static bool AJouer(int j, int k, int joueur)
        {
            grille[j,k] = joueur;
            return false;
        }
        public static bool Gagner(int l, int c, int joueur)
        {
            int TopRow = grille[0, 0] + grille[0, 1] + grille[0, 2];
            int MidRow = grille[1, 0] + grille[1, 1] + grille[1, 2];
            int BotRow = grille[2, 0] + grille[2, 1] + grille[2, 2];
            int FirCol = grille[0, 0] + grille[1, 0] + grille[2, 0];
            int SecCol = grille[0, 1] + grille[1, 1] + grille[2, 1];
            int ThiCol = grille[0, 2] + grille[1, 2] + grille[2, 2];
            int Diagon = grille[0, 0] + grille[1, 1] + grille[2, 2];
            int RevDia = grille[0, 2] + grille[1, 1] + grille[2, 0];

            int playerTriple = joueur + joueur + joueur;

            if (TopRow.Equals(playerTriple) || MidRow.Equals(playerTriple)|| BotRow.Equals(playerTriple)|| FirCol.Equals(playerTriple)|| SecCol.Equals(playerTriple)|| ThiCol.Equals(playerTriple)|| Diagon.Equals(playerTriple)|| RevDia.Equals(playerTriple)){
                return true;
            }
            else{
                return false;
            }
        }
        static void Main()
        {
            int LigneDébut = Console.CursorTop+1;
            int ColonneDébut = Console.CursorLeft;

            int essais = 0;
	        int joueur = 1;
	        int l, c = 0;
            int j, k = 0;
            bool gagner = false;
            bool bonnePosition = false;
            
            for (j=0; j < grille.GetLength(0); j++)
            {
            	for (k=0; k < grille.GetLength(1); k++)
            	{
			        grille[j,k] = 10;
        		}			
            }
			while(!gagner && essais != 9)
			{
				Console.Clear();
				try
				{
					Console.WriteLine("Playing : Joueur {0}", joueur);
					AfficherMorpion(j,k);

					while(true){
						Console.Write("Ligne (1 - 3):");
						try 
						{
							l = int.Parse(Console.ReadLine()) - 1; 
							if(l>=0 & l<=2){break;}
							else{Console.WriteLine("Valeur non valide!");}
						} catch (Exception) {Console.WriteLine("Valeur non valide!");}
					}
					while(true){
						Console.Write("Colonne (1 - 3):");
						try 
						{
							c = int.Parse(Console.ReadLine()) - 1; 
							if(c>=0 & c<=2){break;}
							else{Console.WriteLine("Valeur non valide!");}
						} catch (Exception) {Console.WriteLine("Valeur non valide!");}
					}
					if(grille[l,c]==10){bonnePosition=true;}
					else{
						Console.ForegroundColor=ConsoleColor.DarkRed;
						Console.WriteLine("Invalid Position!");
						Console.ForegroundColor=ConsoleColor.White;
						System.Threading.Thread.Sleep(400);
					}
					if(bonnePosition)
					{
						AJouer(l,c,joueur);
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.ToString());
				}
				if(Gagner(0,0,joueur))
				{
					Console.Clear();
				   	gagner=true;
					Console.WriteLine("Joueur {0} à gagner!\nEssais: {1}", joueur, essais);					   
					AfficherMorpion(0,0);
					break;
				}
				if(bonnePosition)
				{
					bonnePosition=false;
					essais++;
					if(joueur==1)
					{
						joueur=2;
					}
					else
					{
						joueur=1;
					}
				}
				if(essais>=9)
				{
					Console.Clear();
					Console.WriteLine("Match Nul! Personne ne gagne!\nEssais: {0}", essais);
					AfficherMorpion(0,0);
					break;
				}
			};
            Console.ReadKey();
            Main();
		}
	}
}