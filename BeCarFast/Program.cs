using System;

namespace BeCarFast
{
    class Program
    {
        static void Main(string[] args)
        {
        
          
            Boolean win = false;

            //sterowanie
       
            
            String player1 = "$$";
            String player2 = "??";
            String currentPlayer;
            String winningTotem = "<>";
            String mapSymbol = "[]";
            Random r = new Random();
               
            String trap = "*";
            Console.WriteLine("Hello in game 'BeCarFast ' hehe");
            Console.WriteLine("Try to reach top square edge first"+"Type map levels");
            int userMapLengthInput = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("click arrow buttos to move in");
            String[,] finalUserOutMap = new String[userMapLengthInput, userMapLengthInput];
            String[,] tempArray = new string[userMapLengthInput, userMapLengthInput];
            fillMap(finalUserOutMap, userMapLengthInput); //fill with []
           fillTemporaryMap(tempArray, userMapLengthInput); //zwraca tablice z []
            fillArrWithBombs(userMapLengthInput,tempArray, trap);
            addWinningTotemP1AndP2(finalUserOutMap,winningTotem,userMapLengthInput,player1,player2); //dodaje graczy oraz wygrywajacy totem
            displayUserArr(finalUserOutMap, userMapLengthInput); //wyswietla plansze uzytkownika



            // 2621440 downArrow
            //2424832 leftArrow
            // 2555904 rightArrow

            //2490368 upArrow
                
               
                currentPlayer = player1;







            while (!win)
            {



                while (currentPlayer == player1)
                {

                    if(winCheck(finalUserOutMap, win, winningTotem, player1, player2, userMapLengthInput))
                    {
                        win = true;
                        break;
                    }

                    Console.WriteLine("Current player: " + currentPlayer);
                    ConsoleKeyInfo userMove = Console.ReadKey(true);
                    if (userMove.GetHashCode().Equals(2490368))  //tura gracza nr 1
                    {
                        player1GoUp(userMapLengthInput, finalUserOutMap, player1, mapSymbol);

                        compareTwoArr(finalUserOutMap, tempArray, player1, player2, userMapLengthInput);
                        killOponent(userMapLengthInput, finalUserOutMap, player1, player2, currentPlayer, mapSymbol, winningTotem);




                    }
                    else if (userMove.GetHashCode().Equals(2621440))
                    {
                        player1GoDown(userMapLengthInput, finalUserOutMap, player1, mapSymbol);
                        compareTwoArr(finalUserOutMap, tempArray, player1, player2, userMapLengthInput);
                        killOponent(userMapLengthInput, finalUserOutMap, player1, player2, currentPlayer, mapSymbol, winningTotem);


                    }
                    else if (userMove.GetHashCode().Equals(2424832))
                    {
                        player1GoLeft(userMapLengthInput, finalUserOutMap, player1, mapSymbol);
                        compareTwoArr(finalUserOutMap, tempArray, player1, player2, userMapLengthInput);
                        killOponent(userMapLengthInput, finalUserOutMap, player1, player2, currentPlayer, mapSymbol, winningTotem);

                    }

                    else if (userMove.GetHashCode().Equals(2555904))
                    {
                        player1GoRight(userMapLengthInput, finalUserOutMap, player1, mapSymbol);
                        compareTwoArr(finalUserOutMap, tempArray, player1, player2, userMapLengthInput);
                        killOponent(userMapLengthInput, finalUserOutMap, player1, player2, currentPlayer, mapSymbol, winningTotem);


                    }
                    
                    currentPlayer = player2;
                    break;
                }

                Console.WriteLine("Current player: " + currentPlayer);

                displayUserArr(finalUserOutMap, userMapLengthInput);
                Console.WriteLine("");
                ConsoleKeyInfo userMove1 = Console.ReadKey(true);





                while (currentPlayer == player2)
                {

                    if (userMove1.GetHashCode().Equals(2490368))  //tura gracza nr 2
                    {
                        player2GoUp(userMapLengthInput, finalUserOutMap, mapSymbol, player2);

                        compareTwoArr(finalUserOutMap, tempArray, player1, player2, userMapLengthInput);
                        killOponent(userMapLengthInput, finalUserOutMap, player1, player2, currentPlayer, mapSymbol, winningTotem);



                    }
                    else if (userMove1.GetHashCode().Equals(2621440))
                    {
                        player2GoDown(userMapLengthInput, finalUserOutMap, player2, mapSymbol);
                        compareTwoArr(finalUserOutMap, tempArray, player1, player2, userMapLengthInput);
                        killOponent(userMapLengthInput, finalUserOutMap, player1, player2, currentPlayer, mapSymbol, winningTotem);

                    }
                    else if (userMove1.GetHashCode().Equals(2424832))
                    {
                        player2GoLeft(userMapLengthInput, finalUserOutMap, player2, mapSymbol);
                        compareTwoArr(finalUserOutMap, tempArray, player1, player2, userMapLengthInput);
                        killOponent(userMapLengthInput, finalUserOutMap, player1, player2, currentPlayer, mapSymbol, winningTotem);

                    }

                    else if (userMove1.GetHashCode().Equals(2555904))
                    {
                        player2GoRight(userMapLengthInput, finalUserOutMap, player2, mapSymbol);
                        compareTwoArr(finalUserOutMap, tempArray, player1, player2, userMapLengthInput);
                        killOponent(userMapLengthInput, finalUserOutMap, player1, player2, currentPlayer, mapSymbol, winningTotem);

                    }
                    
                    displayUserArr(finalUserOutMap, userMapLengthInput);
                    currentPlayer = player1;
                    break;

                }


            }







            

            



        }
        public static void fillArrWithBombs(int userMapLengthInput,String [,] tempArray,String trap)
        {
            Random r = new Random();

                for (int v1 = 0; v1 <= userMapLengthInput / 2 + 1; v1++)
                {
                    int trapIindex = r.Next(0, userMapLengthInput - 1);
                    int trapJindex = r.Next(0, userMapLengthInput - 1);
                    tempArray[trapIindex, trapJindex] = trap;
                   
                }

            
        }
        public static void compareTwoArr(String [,] playerViewArray,String[,] tempArr,String player1, String player2,int userMapLengthInput)
        {
          
            String trap = "*";

            for(int i =0; i <= userMapLengthInput-1; i++)
            {

                for(int j = 0; j<= userMapLengthInput - 1; j++)
                {
                    if (tempArr[i, j] == trap && playerViewArray[i, j] == player1 )
                    {
                        playerViewArray[i, j] = "*";
                       
                        Console.WriteLine("OH no its trap player 1 moving to start");
                        playerViewArray[userMapLengthInput - 1, 0] = player1;

                        break;
                        
                    }
                    else if ( tempArr[i, j] == trap && playerViewArray[i, j] == player2)
                    {
                        Console.WriteLine("OH no its trap player 2 moving to start");
                        playerViewArray[i, j] = "*";
                        playerViewArray[userMapLengthInput - 1, userMapLengthInput - 1] = player2 ;
                        break;
                    }
            
                    
                }
            }
           
        }
        public static String[,] fillMap(String[,] temporarayTab, int mapLevels)
        {
            
            String output = "[]";

            for (int i = 0; i <= mapLevels - 1; i++)
            {

                for (int j = 0; j <= mapLevels - 1; j++)
                {

                    // int maxIndex = "[3][3]";

                    temporarayTab[i, j] = output;

                   
                }
               

            }

            return temporarayTab;
        }

        public static void displayUserArr(String[,] finalUserMap,int userMapLength)
        {


            for (int i = 0; i <= userMapLength - 1; i++)
            {
                for (int j = 0; j <= userMapLength - 1; j++)
                {
                    Console.Write(finalUserMap[i, j]);
                }
                Console.WriteLine();

            }







        }

        public static String[,] fillTemporaryMap(String[,] temporarayTab, int userMapLength)
        {

            String icon = "[]";

            for (int i = 0; i <= userMapLength - 1; i++)
            {

                for (int j = 0; j <= userMapLength - 1; j++)
                {

                    // int maxIndex = "[3][3]";

                    temporarayTab[i, j] =icon;


                }


            }
            return temporarayTab;

        }


        public static Boolean winCheck(String[,] finalUserOutMap,Boolean win,String winningTotem,String player1,String player2,int userMapLength)
        {

          
                    if (player1 == finalUserOutMap[0,userMapLength/2] || player2 == finalUserOutMap[0,userMapLength/2])
                    {
                        win = true;
                        Console.WriteLine("Congrats u have won");
                        
                    }
              
           
                
            
            return win;
        }
        public static String [,] addWinningTotemP1AndP2(String[,] finalUserOutMap,String symbol,int userInputMapLength,String p1,String p2)
        {
            int a = 0;
            int b = userInputMapLength / 2;
            finalUserOutMap[a, b] = symbol;
            finalUserOutMap[userInputMapLength - 1,0] = p1;
            finalUserOutMap[userInputMapLength - 1,userInputMapLength-1] = p2;
            
        return finalUserOutMap;
        }
        public static String [,] player1GoUp(int userMapLengthInput, String[,] finalUserOutMap, String player1, String mapSymbol)
        {
            for (int i = 0; i <= userMapLengthInput - 1; i++)
            {
                for (int j = 0; j <= userMapLengthInput - 1; j++)
                {
                    if (finalUserOutMap[i, j].Equals(player1))
                    {
                        if (i >0)
                        {
                            
                            finalUserOutMap[i, j] = mapSymbol;
                            finalUserOutMap[i - 1, j] = player1;
                            break;
                            
                        }
                        else
                        {
                            Console.WriteLine("You can't go there ");
                            Console.WriteLine("Move failed :(");
                            break;
                        }


                    }
                }

            }
            return finalUserOutMap;
        }

        public static String[,] player1GoDown(int userMapLengthInput, String[,] finalUserOutMap, String player1, String mapSymbol)
        {
            for (int i = 0; i <= userMapLengthInput - 1; i++)
            {
                for (int j = 0; j <= userMapLengthInput - 1; j++)
                {
                    if (finalUserOutMap[i, j].Equals(player1))
                    {
                        if (i<userMapLengthInput-1)
                        {

                            finalUserOutMap[i, j] = mapSymbol;
                            finalUserOutMap[i + 1, j] = player1;
                            i++;
                            break;

                        }
                        else
                        {
                            Console.WriteLine("You can't go there");
                            Console.WriteLine("Move failed :(");
                            break;
                        }


                    }
                }

            }
            return finalUserOutMap;
        }

        public static String[,] player1GoLeft(int userMapLengthInput, String[,] finalUserOutMap, String player1, String mapSymbol)
        {
            for (int i = 0; i <= userMapLengthInput - 1; i++)
            {
                for (int j = 0; j <= userMapLengthInput - 1; j++)
                {
                    if (finalUserOutMap[i, j].Equals(player1))
                    {
                        if (j!=0)
                        {
                            finalUserOutMap[i, j] = mapSymbol;
                            finalUserOutMap[i , j-1] = player1;

                            break;

                        }
                        else
                        {
                            Console.WriteLine("You can't go there ");
                            Console.WriteLine("Move failed :(");
                            break;
                        }



                    }
                }

            }
            return finalUserOutMap;
        }


        public static String[,] player1GoRight(int userMapLengthInput, String[,] finalUserOutMap, String player1, String mapSymbol)
         
        {
            for (int i = 0; i <= userMapLengthInput - 1; i++)
            {
                for (int j = 0; j <= userMapLengthInput - 1; j++)
                {
                    if (finalUserOutMap[i, j].Equals(player1))
                    {
                        if (j < userMapLengthInput -1)
                        {
                            finalUserOutMap[i, j] = mapSymbol;
                            finalUserOutMap[i, j + 1] = player1;
                            break;

                        }
                        else
                        {
                            Console.WriteLine("You can't go there ");
                            Console.WriteLine("Move failed :(");
                            break;
                        }



                    }
                }

            }
            return finalUserOutMap;
        }


        public static String[,] player2GoUp(int userMapLengthInput, String[,] finalUserOutMap, String mapSymbol,String player2)
        {
            for (int i = 0; i <= userMapLengthInput - 1; i++)
            {
                for (int j = 0; j <= userMapLengthInput - 1; j++)
                {
                    if (finalUserOutMap[i, j].Equals(player2))
                    {
                        if (i != 0)
                        {
                            finalUserOutMap[i, j] = mapSymbol;
                            finalUserOutMap[i - 1, j] = player2;
                            break;

                        }
                        else
                        {
                            Console.WriteLine("You can't go there ");
                            Console.WriteLine("Move failed :(");
                            break;
                        }


                    }
                }

            }
            return finalUserOutMap;
        }
        //   0 1 2
        //0 [][][]
        //1 [][][]
        //2 [][][]
        public static String[,] player2GoDown(int userMapLengthInput, String[,] finalUserOutMap, String player2, String mapSymbol)
        {
            for (int i = 0; i <= userMapLengthInput - 1; i++)
            {
                for (int j = 0; j <= userMapLengthInput - 1; j++)
                {
                    if (finalUserOutMap[i, j].Equals(player2))
                    {
                        if (i < userMapLengthInput - 1)
                        {
                            finalUserOutMap[i, j] = mapSymbol;
                            finalUserOutMap[i + 1, j] = player2;
                            i++;
                            break;

                        }
                        else
                        {
                            Console.WriteLine("You can't go there ");
                            Console.WriteLine("Move failed :(");
                            break;
                        }


                    }
                }
            }
                return finalUserOutMap;
        }


        public static String[,] player2GoLeft(int userMapLengthInput, String[,] finalUserOutMap, String player2, String mapSymbol)
        {
            for (int i = 0; i <= userMapLengthInput - 1; i++)
            {
                for (int j = 0; j <= userMapLengthInput - 1; j++)
                {
                    if (finalUserOutMap[i, j].Equals(player2))
                    {
                        if (j != 0)
                        {
                            finalUserOutMap[i, j] = mapSymbol;
                            finalUserOutMap[i, j - 1] = player2;

                          

                        }
                        else
                        {
                            Console.WriteLine("You can't go there ");
                            Console.WriteLine("Move failed :(");
                          
                        }



                    }
                }

            }
            return finalUserOutMap;
        }


        public static String[,] player2GoRight(int userMapLengthInput, String[,] finalUserOutMap, String player2, String mapSymbol)
        {
            for (int i = 0; i <= userMapLengthInput - 1; i++)
            {
                for (int j = 0; j <= userMapLengthInput - 1; j++)
                {
                    if (finalUserOutMap[i, j].Equals(player2))
                    {
                        if (j < userMapLengthInput - 1 )
                        {
                            finalUserOutMap[i, j] = mapSymbol;
                            finalUserOutMap[i, j + 1] = player2;
                            break;

                        }
                        else
                        {
                            Console.WriteLine("You can't go there ");
                            Console.WriteLine("Move failed :(");
                            break;
                        }



                    }
                }

            }
            return finalUserOutMap;
        }

        //aby dzialalo musimy przypisac ruchy do temporary tab i sprawdzac elementy tab
        public static void killOponent(int userLenghtMapInput,String [,] finalUserArr,String player1,String player2,String currentPlayer,String mapSymbol,String winningTotem)
        {
            for (int i =0; i < userLenghtMapInput - 1;i++)
            {
                for (int j = 0; j< userLenghtMapInput - 1; j++)
                {
                    if (currentPlayer.Equals(player1) && finalUserArr[i, j].Equals(player2) && finalUserArr[i, j].Equals(player1))
                    {

                        Console.WriteLine("Player 2 got killed hes moving to start");
                        finalUserArr[i, j] = mapSymbol;
                        finalUserArr[userLenghtMapInput - 1, userLenghtMapInput - 1] = player2;
                        break;
                    }
                    else if (currentPlayer.Equals(player2) && finalUserArr[i, j].Equals(player1) && finalUserArr[i, j].Equals (player2))
                    {
                        Console.WriteLine("Player 1 got killed hes moving to start");
                        finalUserArr[i, j] = mapSymbol;
                        finalUserArr[userLenghtMapInput - 1, 0] = player1;
                        break;
                       
                    }
                }
            }
        }
    }
}
