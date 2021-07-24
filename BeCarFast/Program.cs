using System;
using System.Threading;
namespace BeCarFast
{
    class Program
    {

        static void Main()
        {
            bool win = false;
            string[] herosArr = { " ^ ", " $ ", " @ ", " & ", " + " };
            string player1;
            string player2;
            string currentPlayer = "";
            const string winningTotem = "< >";
            const string mapSymbol = "[ ]";
            Random r = new Random();
            const string trap = " * ";
            Console.WriteLine("Welcome in game 'BeCareFast ' ");

            Console.WriteLine("Try to reach top middle square  first" + "\n" + "Type map size");

            int userMapLengthInput = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Your map size: " + userMapLengthInput + "x" + userMapLengthInput);
            Console.WriteLine("click arrow buttons to move");
            Thread.Sleep(2000);
            Console.Clear();

            //P1 wybiera postac
            Console.WriteLine("Pick your Hero, P1");

            for (int i = 0; i < herosArr.Length; i++)
            {

                Console.WriteLine(i + 1 + ": " + herosArr[i]);
            }
            Console.WriteLine("type numbers 1-5");

            int P1PickingHeroNumber = Convert.ToInt32(Console.ReadLine());

            player1 = herosArr[P1PickingHeroNumber - 1];
            herosArr[P1PickingHeroNumber - 1] = null;
            //P2 wybiera postac

            Console.WriteLine("P1: " + player1 + "\n" + "P2 choosing: ");

            for (int i = 0; i < herosArr.Length; i++)
            {
                if (herosArr[i] != null)
                {

                    Console.WriteLine(i + 1 + ": " + herosArr[i]);
                }
                else
                {
                    continue;
                }
            }
            Console.WriteLine("type numbers 1-5");
            int P2PickingHeroNumber = Convert.ToInt32(Console.ReadLine());
            player2 = herosArr[P2PickingHeroNumber - 1];
            Console.WriteLine("P2: " + player2);
            Thread.Sleep(2000);
            Console.Clear();
            currentPlayer = Draw(r, currentPlayer, player1, player2);
            string[,] finalUserOutMap = new string[userMapLengthInput, userMapLengthInput];
            string[,] tempArray = new string[userMapLengthInput, userMapLengthInput];
            FillMap(finalUserOutMap, userMapLengthInput); //fill with []
            FillMapWithBombs(userMapLengthInput, tempArray, trap); // wypelnia plansze bombami (*)
            AddWinningTotemP1P2(finalUserOutMap, winningTotem, userMapLengthInput, player1, player2); //dodaje graczy oraz wygrywajacy totem 
            // 2621440 downArrow
            //2424832 leftArrow
            // 2555904 rightArrow
            //2490368 upArrow    
            while (!win)
            {
                DisplayUserMap(finalUserOutMap, userMapLengthInput);
                //   IsKilled(finalUserOutMap, currentPlayer, userMapLengthInput, isKilled);
                while (currentPlayer == player1)
                {

                    if (WinCheck(finalUserOutMap, win, winningTotem, player1, player2, userMapLengthInput, currentPlayer))
                    {
                        win = true;
                        break;
                    }
                    Console.WriteLine("Current player: " + currentPlayer);
                    ConsoleKeyInfo userMove = Console.ReadKey();
                    if (userMove.GetHashCode().Equals(2490368))  //tura gracza nr 1
                    {
                        PlayerGoUp(userMapLengthInput, finalUserOutMap, mapSymbol, player1);
                        CompareTwoArr(finalUserOutMap, tempArray, player1, player2, userMapLengthInput);

                    }
                    else if (userMove.GetHashCode().Equals(2621440))
                    {
                        PlayerGoDown(userMapLengthInput, finalUserOutMap, player1, mapSymbol);
                        CompareTwoArr(finalUserOutMap, tempArray, player1, player2, userMapLengthInput);

                    }
                    else if (userMove.GetHashCode().Equals(2424832))
                    {
                        PlayerGoLeft(userMapLengthInput, finalUserOutMap, player1, mapSymbol);
                        CompareTwoArr(finalUserOutMap, tempArray, player1, player2, userMapLengthInput);


                    }
                    else if (userMove.GetHashCode().Equals(2555904))
                    {
                        PlayerGoRight(userMapLengthInput, finalUserOutMap, player1, mapSymbol);
                        CompareTwoArr(finalUserOutMap, tempArray, player1, player2, userMapLengthInput);


                    }
                    break;
                }


                if (!KillOpponent1(finalUserOutMap,userMapLengthInput,player2))
                {
                    for (int i = 0; i < userMapLengthInput-1; i++)
                    {
                        for (int j = 0; j < userMapLengthInput-1; j++)
                        {
                            finalUserOutMap[userMapLengthInput - 1, userMapLengthInput - 1] = player2;
                            finalUserOutMap[i, j] = mapSymbol;
                            finalUserOutMap[0, userMapLengthInput/2] = winningTotem;
                        }
                    }

                }
                Console.Clear();
                currentPlayer = player2;
                
                if (!win)
                {
                    Console.WriteLine("Current Player: " + currentPlayer);

                    DisplayUserMap(finalUserOutMap, userMapLengthInput);
                    Console.WriteLine("");
                }

                ConsoleKeyInfo userMove1 = Console.ReadKey();
                DisplayUserMap(finalUserOutMap, userMapLengthInput);

                while (currentPlayer == player2)
                {
                    if (WinCheck(finalUserOutMap, win, winningTotem, player1, player2, userMapLengthInput, currentPlayer))
                    {
                        win = true;
                        break;
                    }
                    if (userMove1.GetHashCode().Equals(2490368))  //tura gracza nr 2
                    {
                        PlayerGoUp(userMapLengthInput, finalUserOutMap, mapSymbol, player2);
                        CompareTwoArr(finalUserOutMap, tempArray, player1, player2, userMapLengthInput);
                       


                    }
                    else if (userMove1.GetHashCode().Equals(2621440))
                    {
                        PlayerGoDown(userMapLengthInput, finalUserOutMap, currentPlayer, mapSymbol);
                        CompareTwoArr(finalUserOutMap, tempArray, player1, player2, userMapLengthInput);
                        

                    }
                    else if (userMove1.GetHashCode().Equals(2424832))
                    {
                        PlayerGoLeft(userMapLengthInput, finalUserOutMap, player2, mapSymbol);
                        CompareTwoArr(finalUserOutMap, tempArray, player1, player2, userMapLengthInput);
                      

                    }
                    else if (userMove1.GetHashCode().Equals(2555904))
                    {
                        PlayerGoRight(userMapLengthInput, finalUserOutMap, player2, mapSymbol);
                        CompareTwoArr(finalUserOutMap, tempArray, player1, player2, userMapLengthInput);
                       

                    }
                    if (!KillOpponent2(finalUserOutMap, userMapLengthInput, player1))
                    {
                        for (int i = 0; i <userMapLengthInput-1; i++)
                        {
                            for (int j = 0; j < userMapLengthInput-1; j++)
                            {
                                finalUserOutMap[userMapLengthInput - 1, 0] =player1;
                                finalUserOutMap[i, j] = mapSymbol;
                                finalUserOutMap[0, userMapLengthInput / 2] = winningTotem;
                            }
                        }
                    }
                    Console.Clear();

                    currentPlayer = player1;
           
                    break;
                }
            }
        }
        public static String[,] FillMapWithBombs(int userMapLengthInput, String[,] tempArray, String trap)
        {
            Random r = new Random();
            for (int v1 = 0; v1 <= userMapLengthInput / 2 + 1; v1++)
            {
                int trapIindex = r.Next(0, userMapLengthInput - 1);
                int trapJindex = r.Next(0, userMapLengthInput - 1);
                tempArray[trapIindex, trapJindex] = trap;
            }
            return tempArray;
        }
        public static void CompareTwoArr(String[,] playerViewArray, String[,] tempArr, String player1, String player2, int userMapLengthInput)
        {

            String trap = " * ";
            for (int i = 0; i <= userMapLengthInput - 1; i++)
            {
                for (int j = 0; j <= userMapLengthInput - 1; j++)
                {
                    if (tempArr[i, j] == trap && playerViewArray[i, j] == player1)
                    {
                        playerViewArray[i, j] = trap;
                        playerViewArray[userMapLengthInput - 1, 0] = player1;
                        break;
                    }
                    else if (tempArr[i, j] == trap && playerViewArray[i, j] == player2)
                    {
                        playerViewArray[i, j] = trap;
                        playerViewArray[userMapLengthInput - 1, userMapLengthInput - 1] = player2;
                        break;
                    }

                }
            }
        }
        public static String[,] FillMap(String[,] temporaryArr, int mapLevels)
        {
            String output = "[ ]";

            for (int i = 0; i <= mapLevels - 1; i++)
            {

                for (int j = 0; j <= mapLevels - 1; j++)
                {
                    // int maxIndex = "[3][3]";
                    temporaryArr[i, j] = output;
                }
            }
            return temporaryArr;
        }
        public static void DisplayUserMap(String[,] finalUserMap, int userMapLength)
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
        public static Boolean WinCheck(String[,] finalUserOutMap, Boolean win, String winningTotem, String player1, String player2, int userMapLength, String currentPlayer)
        {
            if (player1 == finalUserOutMap[0, userMapLength / 2])
            {
                win = true;
                Console.WriteLine("Congrats u have won: " + player1);
            }
            else if (player2 == finalUserOutMap[0, userMapLength / 2])
            {
                win = true;
                Console.WriteLine("Congrats u have won" + player2);
            }
            return win;
        }
        public static String[,] AddWinningTotemP1P2(String[,] finalUserOutMap, String symbol, int userInputMapLength, String p1, String p2)
        {
            int a = 0;
            int b = userInputMapLength / 2;
            finalUserOutMap[a, b] = symbol;
            finalUserOutMap[userInputMapLength - 1, 0] = p1;
            finalUserOutMap[userInputMapLength - 1, userInputMapLength - 1] = p2;
            return finalUserOutMap;
        }
        //   0 1 2
        //0 [][][]
        //1 [][][]
        //2 [][][]
        //aby dzialalo musimy przypisac ruchy do temporary tab i sprawdzac elementy tab

        public static String Draw(Random r, String currentPlayer, String player1, String player2)
        {

            int whichPlayerIsFirst = r.Next(0, 2);

            if (whichPlayerIsFirst == 0)
            {
                currentPlayer = player1;
            }
            else
            {
                currentPlayer = player2;
            }
            return currentPlayer;
        }

        private static String[,] PlayerGoUp(int userMapLengthInput, String[,] finalUserOutMap, String mapSymbol, String currentPlayer)
        {
            for (int i = 0; i <= userMapLengthInput - 1; i++)
            {
                for (int j = 0; j <= userMapLengthInput - 1; j++)
                {
                    if (finalUserOutMap[i, j].Equals(currentPlayer))
                    {
                        if (i != 0)
                        {
                            finalUserOutMap[i - 1, j] = currentPlayer;
                            finalUserOutMap[i, j] = mapSymbol;
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
        private static String[,] PlayerGoDown(int userMapLengthInput, String[,] finalUserOutMap, String currentPlayer, String mapSymbol)
        {
            for (int i = 0; i <= userMapLengthInput - 1; i++)
            {
                for (int j = 0; j <= userMapLengthInput - 1; j++)
                {
                    if (finalUserOutMap[i, j].Equals(currentPlayer))
                    {
                        if (i < userMapLengthInput - 1)
                        {
                            finalUserOutMap[i, j] = mapSymbol;
                            finalUserOutMap[i + 1, j] = currentPlayer;
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
        private static String[,] PlayerGoLeft(int userMapLengthInput, String[,] finalUserOutMap, String currentPlayer, String mapSymbol)
        {
            for (int i = 0; i <= userMapLengthInput - 1; i++)
            {
                for (int j = 0; j <= userMapLengthInput - 1; j++)
                {
                    if (finalUserOutMap[i, j].Equals(currentPlayer))
                    {
                        if (j != 0)
                        {
                            finalUserOutMap[i, j] = mapSymbol;
                            finalUserOutMap[i, j - 1] = currentPlayer;
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
        private static String[,] PlayerGoRight(int userMapLengthInput, String[,] finalUserOutMap, String currentPlayer, String mapSymbol)
        {
            for (int i = 0; i <= userMapLengthInput - 1; i++)
            {
                for (int j = 0; j <= userMapLengthInput - 1; j++)
                {
                    if (finalUserOutMap[i, j].Equals(currentPlayer))
                    {
                        if (j < userMapLengthInput - 1)
                        {
                            finalUserOutMap[i, j] = mapSymbol;
                            finalUserOutMap[i, j + 1] = currentPlayer;
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
        private static bool KillOpponent1(string[,] finalUserArr, int userLengthInput, string player2)
        {  

            bool isContainingP1 = false;

            foreach (var item in finalUserArr)
            {
                if (item.Equals(player2))
                {
                    
                   
                    isContainingP1 = true;
                }
            }
            return isContainingP1;


        }
        private static bool KillOpponent2(string[,] finalUserArr,int userLengthInput, string player1)
        {
           

            bool isContainingP1 = false;

            
                    foreach (var item in finalUserArr)
                    {
                        if (item.Equals(player1))
                        {
                    
                    isContainingP1 = true;
                        }
                    }
                    
                    
                
            
            return isContainingP1;


        }

    }

}
    


/* Console.WriteLine("Player 2 got killed hes moving to start");
finalUserArr[userLengthInput - 1, userLengthInput - 1] = player2;
finalUserArr[i, j] = mapSymbol;
break; */

/*
    Console.WriteLine("Player 1 got killed hes moving to start");
                        finalUserArr[userLengthInput-1, 0] = player1;
                        finalUserArr[i, j] = mapSymbol;
                        break;
 */