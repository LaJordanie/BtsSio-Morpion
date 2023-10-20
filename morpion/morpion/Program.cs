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
        // Crée une grille de jeu de morpion de taille 3x3.
		public static int[,] grille = new int[3, 3];
        
        // Cette fonction affiche la grille de jeu actuelle.
        public static void AfficherMorpion(int j, int k)
        {
        	for(int y=0;y<grille.GetLength(1);y++)
        	{
	        	for(int x=0;x<grille.GetLength(0);x++)
	        	{
                    // Affiche "[O]" si le joueur 1 a joué dans cette cellule.
	        		if(grille[y,x]==1){
	        			Console.Write("[O]");
	        		}
                    // Affiche "[X]" si le joueur 2 a joué dans cette cellule.
	        		else if(grille[y,x]==2){
	        			Console.Write("[X]");
	        		}
                    // Affiche "[ ]" si la cellule est vide.
	        		else{
	        			Console.Write("[ ]");
	        		}
	        	}
	        	Console.WriteLine();
        	}
        }
        
        // Cette fonction permet à un joueur de jouer à une position spécifique (j, k) sur la grille.
        public static bool AJouer(int j, int k, int joueur)
        {
            grille[j,k] = joueur;
            return false;
        }
        
        // Cette fonction vérifie si un joueur a gagné le jeu.
        public static bool Gagner(int l, int c, int joueur)
        {
            // Calcule la somme des valeurs pour chaque ligne, colonne et diagonale.
            int TopRow = grille[0, 0] + grille[0, 1] + grille[0, 2];
            int MidRow = grille[1, 0] + grille[1, 1] + grille[1, 2];
            int BotRow = grille[2, 0] + grille[2, 1] + grille[2, 2];
            int FirCol = grille[0, 0] + grille[1, 0] + grille[2, 0];
            int SecCol = grille[0, 1] + grille[1, 1] + grille[2, 1];
            int ThiCol = grille[0, 2] + grille[1, 2] + grille[2, 2];
            int Diagon = grille[0, 0] + grille[1, 1] + grille[2, 2];
            int RevDia = grille[0, 2] + grille[1, 1] + grille[2, 0];

            // Calcule la somme des valeurs pour un joueur ayant trois marques dans une ligne.
            int playerTriple = joueur + joueur + joueur;

            // Vérifie si l'un des joueurs a trois de ses marques dans une ligne.
            if (TopRow.Equals(playerTriple) || MidRow.Equals(playerTriple)|| BotRow.Equals(playerTriple)|| FirCol.Equals(playerTriple)|| SecCol.Equals(playerTriple)|| ThiCol.Equals(playerTriple)|| Diagon.Equals(playerTriple)|| RevDia.Equals(playerTriple)){
                return true;
            }
            else{
                return false;
            }
        }
        
        // Point d'entrée du programme. Le corps de cette fonction semble manquer dans le code fourni.
        static void Main()

        {	// Initialisation des varirables pour le jeu
            int LigneDébut = Console.CursorTop+1;
            int ColonneDébut = Console.CursorLeft;

            int essais = 0;
	        int joueur = 1;
	        int l, c = 0;
            int j, k = 0;
            bool gagner = false;
            bool bonnePosition = false;
            
			// Initialisation de la grille de jeu
            for (j=0; j < grille.GetLength(0); j++)
            {
            	for (k=0; k < grille.GetLength(1); k++)
            	{
			        grille[j,k] = 10;
        		}			
            }
			// Boucle Principale du jeu
			while(!gagner && essais != 9)
			{
				Console.Clear();
				try
				{
					Console.WriteLine("Playing : Joueur {0}", joueur);
					AfficherMorpion(j,k);

					// Boucle pour obtenir une entrée valide pour la ligne
					while(true){
						Console.Write("Ligne (1 - 3):");
						try 
						{
							l = int.Parse(Console.ReadLine()) - 1; 
							if(l>=0 & l<=2){break;}
							else{Console.WriteLine("Valeur non valide!");}
						} catch (Exception) {Console.WriteLine("Valeur non valide!");}
					}
					// Boucle pour obtenir une entrée valide pour la colonne
					while(true){
						Console.Write("Colonne (1 - 3):");
						try 
						{
							c = int.Parse(Console.ReadLine()) - 1; 
							if(c>=0 & c<=2){break;}
							else{Console.WriteLine("Valeur non valide!");}
						} catch (Exception) {Console.WriteLine("Valeur non valide!");}
					}
					// Vérifie si la position est libre
					if(grille[l,c]==10){bonnePosition=true;}
					else{
						Console.ForegroundColor=ConsoleColor.DarkRed;
						Console.WriteLine("Invalid Position!");
						Console.ForegroundColor=ConsoleColor.White;
						System.Threading.Thread.Sleep(400);
					}
					// Si la position est libre, le joueur joue à cette position
					if(bonnePosition)
					{
						AJouer(l,c,joueur);
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.ToString());
				}
				// Vérifie si le joueur a gagné
				if(Gagner(0,0,joueur))
				{
					Console.Clear();
				   	gagner=true;
					Console.WriteLine("Joueur {0} à gagner!\nEssais: {1}", joueur, essais);					   
					AfficherMorpion(0,0);
					break;
				}
				// Si le joueur a joué à une position valide, passe au joueur suivant
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
				// Si tous les mouvements possibles ont été joués sans qu'un joueur gagne, le jeu se termine en match nul
				if(essais>=9)
				{
					Console.Clear();
					Console.WriteLine("Match Nul! Personne ne gagne!\nEssais: {0}", essais);
					AfficherMorpion(0,0);
					break;
				}
			};
			 // Attend une touche pour redémarrer le jeu
            Console.ReadKey();
            Main();
		}
	}
}