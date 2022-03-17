using System;
using System.Collections.Generic;
using System.Linq;
namespace jeu_du_pendu
{
    class Program
    {
        private static List<string> m_wordlist = new List<string> // liste des différents mots pour la réponse du pendu
        {
           
        };

        private static readonly Random index = new Random();        

        private static void Main()
        {
            Game();            
        }

        private static void Game()
        // gère le déroulement de la partie
        {
            bool playing = true;   // continue le jeu tant que true

            while (playing == true)

            {
                bool game_over = false;    // continue la partie tant que true

                int remaining_attempts = 6;  // nombre de "vies" du joueur


                m_read_textfile();  

                string m_mystery_word = m_wordlist[index.Next(m_wordlist.Count)];  // prends un mot aléatoire dans la liste de mots "wordlist"                               

                m_take_chars_from_mystery_word(m_mystery_word);               

                while (game_over == false)
                {
                    m_print_lifes(remaining_attempts);
                    
                    m_print_wrong_letters();

                    m_print_tree(remaining_attempts);

                    m_print_mystery_word();
                    Console.WriteLine("\n");
                    
                    if (m_test_guess() == false)
                    {
                        remaining_attempts--;
                    }
                    
                    Console.Clear();

                    if (remaining_attempts == 0)
                    {
                        Console.WriteLine("\n     Tu as perdu! Le mot c'était: " + m_mystery_word + " !");
                        game_over = true;
                    }
                    else if (!m_mystery_letters.Except(m_guessed_letters).Any())
                    {
                        Console.WriteLine("\n     Tu as gagné!");
                        game_over = true;
                    }
                }

                Console.WriteLine("\nTu veux rejouer ?  \n 1 - Oui\n 2 - Non");

                int answer;
                while (!int.TryParse(Console.ReadLine(), out answer) || answer != 1 && answer != 2)
                {
                    Console.WriteLine("Il faut rentrer soit 1 (oui) soit 2 (non).");
                }

                if (answer == 1)
                { 
                    m_mystery_letters.Clear();
                    m_wrong_letters.Clear();
                    m_guessed_letters.Clear();
                    Console.Clear();                
                }

                else if (answer == 2)
                {
                    playing = false;
                }
            }          
        }

        private static List<char> m_mystery_letters = new List<char>
        {

        };

        private static void m_read_textfile()
        // lit le textfile "gamelist" et ajoute chaque liste à la liste de string "m_wordlist"
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Victor\Desktop\projets GDC\c#\gamelist.txt");
            foreach (string line in lines)
            {
                //Console.WriteLine("/t" + line);
                m_wordlist.Add(line);
            }
            
            //Console.WriteLine("Contents of gamelist.txt = {0}", words);
        }

        private static void m_take_chars_from_mystery_word(string m_mystery_word)
        // prend les charactères du mot mystère et les ajoute à la liste de lettres mystères ainsi qu'au tableau qui va afficher les lettres dans le jeu
        {
            foreach (char c in m_mystery_word)
            {                             
                m_mystery_letters.Add(c);
                
                if (c == ' ')
                {
                    m_guessed_letters.Add(c);
                }
            }
            
        }

        private static List<char> m_wrong_letters = new List<char>
        {
            
        };

        private static void m_print_lifes(int remaining_attempts)
        {
            Console.WriteLine("Vies restantes : " + remaining_attempts);
        }



        private static void m_print_wrong_letters()
        // affiche la liste des lettres qui ne sont pas dans le mot mystère
        {           
            Console.WriteLine("Lettres qui ne sont pas dans le mot:  ");
            foreach (char c in m_wrong_letters)
            {
                Console.Write(c + ", ");
            }
        }
               
        private static void m_print_tree(int remaining_attempts)
        // print l'arbre du pendu
        {            
            Console.Write("\n \n");
            int i = 10;
            while (i != 0)
            {
                Console.Write(" ");
                i--;
            }
            i = 5;
            while (i != 0)
            {
                Console.Write("_");
                i--;
            }            
            Console.Write("\n");
           
            
            i = 9;
            while (i != 0)
            {
                Console.Write(" ");
                i--;
            }
            Console.Write("|");
            i = 5;
            while (i != 0)
            {
                Console.Write(" ");
                i--;
            }
            Console.Write("|");
            Console.Write("\n");


            i = 9;
            while (i != 0)
            {
                Console.Write(" ");
                i--;
            }
            // Si le nombre d'essais est de 5 ou moins (donc si le joueur a perdu au moins une vie) print la tête du pendu. Sinon print un espace.
            if (remaining_attempts <= 5)
            {
                Console.Write("0");
            }
            else { Console.Write(" "); }
            i = 5;
            while (i != 0)
            {
                Console.Write(" ");
                i--;
            }
            Console.Write("|");
            Console.Write("\n");
            
            
            i = 8;
            while (i != 0)
            {
                Console.Write(" ");
                i--;
            }
            // Si le nombre d'essais est de 3 ou moins print le bras gauche du pendu. Sinon print un espace.
            if (remaining_attempts <= 3)
            {
                Console.Write("/");
            }
            else { Console.Write(" "); }
            // Si le nombre d'essais est de 4 ou moins print le corps du pendu. Sinon print un espace.
            if (remaining_attempts <= 4)
            {
                Console.Write("|");
            }            
            else { Console.Write(" "); }
            // Si le nombre d'essais est de 2 ou moins print le bras droit du pendu. Sinon print un espace.
            if (remaining_attempts <= 2)
            {
                Console.Write("\\");
            }
            else { Console.Write(" "); }
            i = 4;
            while (i != 0)
            {
                Console.Write(" ");
                i--;
            }
            Console.Write("|");
            Console.Write("\n");

            
            i = 8;
            while (i != 0)
            {
                Console.Write(" ");
                i--;
            }
            // Si le nombre d'essais est de 1 ou moins print la jambe gauche du pendu. Sinon print un espace.
            if (remaining_attempts <= 1)
            {
                Console.Write("/");
            }
            else { Console.Write(" "); }
            Console.Write(" ");
            // Si le nombre d'essais est de 0 ou moins print la jambe droite du pendu. Sinon print un espace.
            if (remaining_attempts <= 0)
            {
                Console.Write("\\");
            }
            else { Console.Write(" "); }
            i = 4;
            while (i != 0)
            {
                Console.Write(" ");
                i--;
            }
            Console.Write("|");
            Console.Write("\n");


            i = 15;
            while (i != 0)
            {
                Console.Write(" ");
                i--;
            }
            Console.Write("|");
            Console.Write("\n");


            i = 4;
            while (i != 0)
            {
                Console.Write(" ");
                i--;
            }
            i = 11;
            while (i != 0)
            {
                Console.Write("_");
                i--;
            }
            Console.Write("|");
            Console.Write("\n");


            i = 5;
            while (i != 0)
            {
                Console.Write(" ");
                i--;
            }
            Console.Write("||");
            i = 5;
            while (i != 0)
            {
                Console.Write(" ");
                i--;
            }
            Console.Write("|| \n \n");

            /*
                   _____
                  |     |
                  0     |
                 /|\    |
                 / \    |
                        |
             ___________|
              ||     ||
            */
        }


        private static List<char> m_guessed_letters = new List<char>
        {

        };

        private static void m_print_mystery_word()
        // print le mot mystère
        {
            Console.Write(" ");

            foreach (char c in m_mystery_letters)
            {
                if (m_guessed_letters.Contains(c))
                {
                    Console.Write(c + "  ");
                }
                else { Console.Write("_  "); }
            }
        }

        
        private static bool m_test_guess()
        // test l'entrée du joueur pour voir si elle correspond à une lettre mystère. retourne le nombre d'essais que le joueur 
        {
            char guess = m_players_guess();
            
            if (m_mystery_letters.Contains(guess))
            {
                if (m_guessed_letters.Contains(guess))
                {                    
                    return true;                    
                }
                else
                {
                    m_guessed_letters.Add(guess);
                    return true;
                }              
            }
            else if (m_wrong_letters.Contains(guess))
            {                
                return true;
            }
            else
            {
                m_wrong_letters.Add(guess);
                return false;
            }            
        }

        private static char m_players_guess()
        // prend une entrée du joueur pour deviner une lettre du mot mystère
        {
            char chosen_letter;
            while(!char.TryParse(Console.ReadLine(), out chosen_letter) || !Char.IsLetter(chosen_letter) || m_guessed_letters.Contains(Char.ToUpper(chosen_letter)) || m_wrong_letters.Contains(Char.ToUpper(chosen_letter)))
            {                              
                Console.WriteLine("Il faut rentrer une lettre (que tu n'as pas déjà rentré).");               

            }
            chosen_letter = Char.ToUpper(chosen_letter);
            Console.WriteLine(chosen_letter);
            return chosen_letter;
        }
    }
}
