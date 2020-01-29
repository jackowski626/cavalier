using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cavalier {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Cavalier");
            Console.WriteLine("Ecrit par Jacek Wikiera, gymnomeur au gymnase du Bugnon, site de Sévelin - 25/01/2020\n");
 
            int[] position = new int[2];
 
            int[,] damier = new int[8, 8] ;

            Boolean justStarted = true;
            Boolean justFailed = false;
            Boolean justEnteredCommand = false;

            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    damier[i, j] = 0;
                }
            }

            void printTable(int[,] table) {
                Console.WriteLine("   A  B  C  D  E  F  G  H");
                Console.WriteLine("   ______________________");
                string temp = "";
                for (int i = 0; i < 8; i++) {
                    for (int j = 0; j < 8; j++) {
                        if (j == 0) {
                            temp += (i+1) + " |";
                        } if (table[i, j] == 2)
                        
                            {
                        temp += "C  ";
                        } else {
                            temp += table[i, j] + "  ";
                        }
                    }
                    Console.WriteLine(temp);
                    temp = "";
                }
            }

            int canMove(string coords, int[,] table) {
                int x = (int)getCoords(coords)[0];
                int y = (int)getCoords(coords)[1];
                if (x == -1) {
                    return 0;
                }
                if ((y-position[1] == 1 && x-position[0] == 2) || (position[1]-y == 1 && x-position[0] == 2) || (position[1]-y == 2 && x-position[0] == 1) || (position[1] - y == 2 && position[0]-x == 1) || (position[1] - y == 1 && position[0]-x == 2) || (y - position[1] == 1 && position[0]-x == 2) || (y - position[1] == 2 && x - position[0] == 1) || (y - position[1] == 2 && position[0] - x == 1)) {
                    return 4;
                } else if (table[x, y] == 1) {
                    return 2;
                } else if (table[x, y] == 2) {
                    return 3;
                }
                return 1;
            }

            Boolean finished(int[,] table) {   
                Boolean flag = false;
                int counter = 0;
                for (int i = 0; i < 8; i++) {
                    for (int j = 0; j < 8; j++) {
                        if (Convert.ToInt32(Convert.ToChar(table[i, j])) == 1) {
                            counter++;
                        }
                    }
                }
                if (counter == 63) {
                    flag = true;
                }
                return flag;
            }

            int[] getCoords(string coords) {
                int[] temp = new int[2];
                if (coords == "exit") {
                    Console.WriteLine("Exiting");
                    temp[0] = -2;
                } else if (coords == "vik") {
                    Console.WriteLine("chhh vik");
                    temp[0] = -2;
                } else if (coords == "reset") {
                    //Console.WriteLine("chhh vik");
                    temp[0] = -2;
                    justEnteredCommand = false;
                    justFailed = false;
                    justStarted = true;
                    damier = new int[8, 8];
                    printTable(damier);
                } else if (coords == "coords") {
                    Console.WriteLine("x: "+position[0]+", y: "+position[1]);
                    temp[0] = -2;
                } else if (coords.Length != 2 || !char.IsLetter(Convert.ToChar(coords.Substring(0, 1))) || !int.TryParse(coords.Substring(1), out _) || Convert.ToInt32(coords.Substring(1)) > 8 || Convert.ToInt32(coords.Substring(1)) < 0 || Convert.ToInt32(Convert.ToChar(coords.Substring(0, 1).ToUpper())) < 65 || Convert.ToInt32(Convert.ToChar(coords.Substring(0, 1).ToUpper())) > 72) {
                    temp[0] = -1;
                } else {
                    temp[0] = Convert.ToInt32(coords.Substring(1).ToUpper()) - 1;
                    temp[1] = Convert.ToInt32(Convert.ToChar(coords.Substring(0, 1).ToUpper())) - 65;   
                }
                return temp;
            }

            do {
                if (justStarted) {
                    if (!justFailed && !justEnteredCommand) {
                        printTable(damier);
                    }
                    if (!justFailed && !justEnteredCommand) {
                        Console.WriteLine("-------------------------");
                        Console.WriteLine("Choisissez la position initiale du cavalier. (Ex: A1)");
                        Console.WriteLine("Le cavalier est representé par 'C'");
                        Console.WriteLine("Utilisez la commande 'reset' pour recommencer");
                    }
                    string input = Console.ReadLine();
                    if ((int)getCoords(input)[0] == -2) {
                        //justStarted = false;
                        justEnteredCommand = true;
                    } else if ((int)getCoords(input)[0] == -1) {
                        justFailed = true;
                        justEnteredCommand = false;
                        Console.WriteLine("Rentrez des coordonnées valides");
                    } else {
                        justFailed = false;
                        justStarted = false;
                        justEnteredCommand = false;
                        position[0] = (int)getCoords(input)[0];
                        position[1] = (int)getCoords(input)[1];
                        damier[position[0], position[1]] = 1;
                        damier[(int)getCoords(input)[0], (int)getCoords(input)[1]] = 2;
                        
                        Console.WriteLine("-------------------------");
                    }
                } else {
                    if (!justFailed && !justEnteredCommand) {
                        printTable(damier);
                    }
                    Console.WriteLine("-------------------------");
                    Console.WriteLine("Entrez les coordonées du prochain mouvement.");
                    string input = Console.ReadLine();
                    if ((int)getCoords(input)[0] == -2) {
                        justEnteredCommand = true;
                    } else if (canMove(input, damier) == 4) {
                        damier[position[0], position[1]] = 1;
                        damier[(int)getCoords(input)[0], (int)getCoords(input)[1]] = 2;
                        position[0] = (int)getCoords(input)[0];
                        position[1] = (int)getCoords(input)[1];
                        justFailed = false;
                        justEnteredCommand = false;
                    } else if (canMove(input, damier) == 0) {
                        Console.WriteLine("Veuillez entrer des coordonnées correctes");
                        justFailed = true;
                    } else if (canMove(input, damier) == 1) {
                        Console.WriteLine("Le cavalier ne peut pas se déplacer sur cette case");
                        justFailed = true;
                    } else if (canMove(input, damier) == 2) {
                        Console.WriteLine("Le cavalier ne peut pas être positionné dans une case déjà visitée");
                        justFailed = true;
                    } else if (canMove(input, damier) == 3) {
                        Console.WriteLine("Le cavalier doit changer de position");
                        justFailed = true;
                    }
                    //Console.ReadLine();    
                }
            } while (!finished(damier));
            Console.WriteLine("Félicitations! Vous avez réussi à résoudre le problème du cavalier.");
            Console.WriteLine("Vous avez droit à un camembert.");
            Console.ReadLine();
            }
        }
    }
