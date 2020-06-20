using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scribble
{
    class Program
    {
        static Random rnd = new Random();

        static Dictionary[] vocabulary = new Dictionary[50000];
        static Reservoir[] letters = new Reservoir[26];
        //static Reservoir[] letters = new Reservoir[29];

        static char[,] boardArray = new char[15, 15];
        static char[,] previousArray = new char[15, 15];
        static char[,] tempArray = new char[15, 15];

        static string[] prevWordList = new string[100];
        static string[] tempWordList = new string[100];
        static string[] scoringWList = new string[100];

        static char[] player1BagUsed = new char[7];
        static char[] player2BagUsed = new char[7];

        static char[] player1BagReturn = new char[7];
        static char[] player2BagReturn = new char[7];

        static char[] player1Query = new char[15];
        static char[] player2Query = new char[15];

        static string[] queryList = new string[50000];

        static char[] p1Bag = new char[7];
        static char[] p2Bag = new char[7];

        static int cursorx = 16;
        static int cursory = 10;

        static int arrayx = 7;
        static int arrayy = 7;

        static int temparrayx = 0;
        static int temparrayy = 0;

        static int p1OldScore = 0;
        static int p2OldScore = 0;

        static int player1Score = 0;
        static int player2Score = 0;

        static int p1QueryChance = 3;
        static int p2QueryChance = 3;

        static int round = 1;

        static int passCount = 4;

        static int queryCountP1 = 0;
        static int queryCountP2 = 0;


        struct Reservoir
        {
            public char name;
            public int amount;
            public int point;
        }
        struct Dictionary
        {
            public string words;
        }
        static void StartUpScreen()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(@"           ___   ___   ___        ___    ___          ___        ");
            Console.WriteLine(@"          |     |     |   \   |  |   \  |   \  |     |           ");
            Console.WriteLine(@"          |__   |     |___/   |  |___/  |___/  |     |___        ");
            Console.WriteLine(@"             |  |     |   \   |  |   \  |   \  |     |           ");
            Console.WriteLine(@"          ___|  |___  |    \  |  |___/  |___/  |___  |___        ");
            Console.WriteLine("          __________________________________________________      ");
            Console.WriteLine(" ");
            Console.Write("                           LOADING                                ");
            for (int i = 0; i < 100; i++)
            {
                Console.SetCursorPosition(36, 10);
                Console.Write("%" + i);
                System.Threading.Thread.Sleep(35);
            }
            Console.WriteLine("");
            Menu();

        }
        static void Menu()
        {            
            Console.SetCursorPosition(15, 10);
            Console.WriteLine("                            ");
            Console.WriteLine("                   ____________________________");
            Console.WriteLine("                  |                            |");
            Console.WriteLine("                  |    1) PLAY                 |");
            Console.WriteLine("                  |    2) INSTRUCTIONS         |");
            Console.WriteLine("                  |    3) EXIT                 |");
            Console.WriteLine("                  |____________________________|");
            Console.WriteLine("");
                Console.Write("                  Select:");

        }
        static void Instructıons()
        {
            int instCursorX = 65;
            
            Console.SetCursorPosition(instCursorX, 3);
            Console.WriteLine(" ______________________________");
            Console.SetCursorPosition(instCursorX, 4);
            Console.WriteLine(" ");
            Console.SetCursorPosition(instCursorX, 5);
            Console.WriteLine("  INSTRUCTIONS");
            Console.SetCursorPosition(instCursorX, 6);
            Console.WriteLine(" ______________________________");
            Console.SetCursorPosition(instCursorX, 7);
            Console.WriteLine("");
            Console.SetCursorPosition(instCursorX, 8);
            Console.WriteLine(@"  RETURNS ---> <Tab> / Change your letter(s)");
            Console.SetCursorPosition(instCursorX, 9);
            Console.WriteLine(@"  QUERY   ---> <Insert> / Search acceptable words");
            Console.SetCursorPosition(instCursorX, 10);
            Console.WriteLine("  PASS    ---> <*> / Pass your turn without any option");
            Console.SetCursorPosition(instCursorX, 11);
            Console.WriteLine(" ");
            Console.SetCursorPosition(instCursorX, 12);
            Console.WriteLine("  Good Luck!");
            
        }

        static void ReturnOption() {

            bool ret = true;

            cursorx = 44;
            cursory = 6;

            ConsoleKeyInfo ckiRet;

            Console.SetCursorPosition(cursorx, cursory);

            while (ret) {
                ckiRet = Console.ReadKey(true);
                if (ckiRet.Key == ConsoleKey.LeftArrow)
                {

                    if (cursorx > 44)
                    {
                        cursorx -= 2;

                    }

                }
                if (ckiRet.Key == ConsoleKey.RightArrow)
                {

                    if (cursorx < 56)
                    {
                        cursorx += 2;

                    }

                }
               
                if (char.IsLetter(ckiRet.KeyChar))
                {

                    char[] chchar = ckiRet.KeyChar.ToString().ToUpper().ToCharArray();
                    if (round % 2 == 1)
                    {
                        for (int i = 0; i < p1Bag.Length; i++)
                        {
                            if (chchar[0].Equals(p1Bag[i]))
                            {
                                Console.Write(ckiRet.KeyChar.ToString().ToUpper());
                                player1BagReturn[i] = chchar[0];
                                //boardArray[arrayx, arrayy] = chchar[0];
                                //writeWord += cki.KeyChar.ToString().ToUpper();
                                break;
                            }
                        }
                    }
                    else if (round % 2 == 0)
                    {
                        for (int i = 0; i < p2Bag.Length; i++)
                        {
                            if (chchar[0].Equals(p2Bag[i]))
                            {
                                Console.Write(ckiRet.KeyChar.ToString().ToUpper());
                                player2BagReturn[i] = chchar[0];
                                //boardArray[arrayx, arrayy] = chchar[0];
                                //writeWord += cki.KeyChar.ToString().ToUpper();
                                break;
                            }
                        }
                    }






                }
                if (ckiRet.Key == ConsoleKey.Enter)
                {
                    if (round % 2 == 1) {
                        BagRefill(p1Bag, player1BagReturn, false);
                    }
                    if (round % 2 == 0)
                    {
                        BagRefill(p2Bag, player2BagReturn, false);
                    }

                    ret = false;
                    

                }
                if (ckiRet.Key == ConsoleKey.Backspace)
                {
                    Console.Write("_");
                }
                

                Console.SetCursorPosition(cursorx, cursory);
            }
        }

        static void QueryOption() {
            bool que = true;

            cursorx = 44;
            cursory = 8;

            ConsoleKeyInfo ckiQue;

            Console.SetCursorPosition(cursorx, cursory);

            while (que)
            {
                ckiQue = Console.ReadKey(true);
                if (ckiQue.Key == ConsoleKey.LeftArrow)
                {

                    if (cursorx > 44)
                    {
                        cursorx -= 2;

                    }

                }
                else if (ckiQue.Key == ConsoleKey.RightArrow)
                {

                    if (cursorx < 72)
                    {
                        cursorx += 2;

                    }

                }
                else if (ckiQue.Key == ConsoleKey.Enter)
                {                    
                    if (round % 2 == 1)
                    {                   
                        QueryProcess(player1Query);
                    }
                    else if (round % 2 == 0)
                    {
                        QueryProcess(player2Query);
                    }
                    que = false;

                }
                if (char.IsLetter(ckiQue.KeyChar) || ckiQue.KeyChar == 46)
                {

                    char[] chchar = ckiQue.KeyChar.ToString().ToUpper().ToCharArray();
                    Console.Write(ckiQue.KeyChar.ToString().ToUpper());

                    
                    if (round % 2 == 1)
                    {

                        player1Query[queryCountP1] = chchar[0];
                        queryCountP1++;
                    }
                    else if (round % 2 == 0)
                    {
                        player2Query[queryCountP2] = chchar[0];
                        queryCountP2++;
                    }

                }
                else if (ckiQue.Key == ConsoleKey.Backspace)
                {
                    Console.Write("_");
                }

                
                

                Console.SetCursorPosition(cursorx, cursory);
            }
        }

        static void QueryProcess(char[] query)
        {
            int queryWordLength = 0;
            int findingWord = 0;
            
            for (int i = 0; i < query.Length; i++)
            {
                if (!query[i].Equals('_')) {
                    queryWordLength++;
                }
            }
            for (int i = 0; i < vocabulary.Length; i++)
            {
                if (vocabulary[i].words != null) {
                    string quedic = vocabulary[i].words.ToString().ToUpper();
                    if (quedic.Length == queryWordLength)
                    {
                        char[] quechar = quedic.ToCharArray();
                        int pointCounter = 0; 
                        int commonLetterCounter = 0;
                        for (int j = 0; j < queryWordLength; j++)
                        {
                            if (query[j] == quechar[j]) {
                                commonLetterCounter++;
                            }
                            if (query[j] == '.')
                            {
                                pointCounter++;
                            }
                        }
                        if (commonLetterCounter + pointCounter == queryWordLength) // Their sum must be equal to the length of word
                        {
                            if (findingWord <= 9) // We only show 10 result
                            {
                                queryList[findingWord] = quedic;
                                findingWord++;
                            }
                        }
                    }
                   
                }
                
            }
            int quecursory = 10;
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(34, quecursory);
                Console.Write("                 ");
                quecursory++;
            }
            Console.BackgroundColor = ConsoleColor.Red;
            quecursory = 10;
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(34, quecursory);
                Console.Write(queryList[i]);
                quecursory++;
            }
            for (int i = 0; i < queryList.Length; i++)
            {
                queryList[i] = "";
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        static void PrintBoard() {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.Write("Letter/Point/Amount: ");
            for (int i = 0; i < letters.Length; i++)
            {
                Console.Write(letters[i].name + ":" + letters[i].point + ":"+letters[i].amount+" ");
                if (i == 12)
                {
                    Console.WriteLine("  ");
                    Console.Write("                     ");
                }
            }
            Console.WriteLine();
            Console.WriteLine("+ - - - - - - - - - - - - - - - +");

            for (int i = 0; i < boardArray.GetLength(0); i++)
            {
                Console.Write("|");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(" ");
                for (int j = 0; j < boardArray.GetLength(1); j++)
                {
                    if (boardArray[i, j] == '.')
                    {
                        if (i == 7 && j == 7)
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(boardArray[i, j]);
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(" ");
                        }
                        else {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(boardArray[i, j] + " ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                       
                       
                        
                    }
                    else {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(boardArray[i, j] + " ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    
                }
                Console.Write("|");
                Console.WriteLine(" ");
            }
            Console.WriteLine("+ - - - - - - - - - - - - - - - +");
            
        }

        static void PrintRoundInfo() {
            Console.SetCursorPosition(33, 4);
            Console.Write(" Round   : " + round );
            if (round % 2 == 1) {
                Console.Write("  (Player 1) ");
            }
            else if (round % 2 == 0)
            {
                Console.Write("  (Player 2) ");
            }
            
            Console.SetCursorPosition(33, 6);
            Console.Write(" Returns : " + "_ _ _ _ _ _ _");
            Console.SetCursorPosition(33, 8);
            Console.Write(" Query   : " + "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _");
        }

        static void PrintPlayerBags() {
            Console.SetCursorPosition(75, 4);
            Console.Write(" Player 1: ");
            Console.SetCursorPosition(75, 6);
            Console.Write(" Bag: ");
            for (int d = 0; d < 7; d++)
            {
                Console.Write(p1Bag[d] + " ");
            }
            Console.SetCursorPosition(75, 8);
            Console.Write(" Score: "+player1Score);
            Console.SetCursorPosition(75, 10);
            Console.Write(" ____________________________________");
            Console.SetCursorPosition(75, 12);
            Console.Write(" Player 2: ");
            Console.SetCursorPosition(75, 14);
            Console.Write(" Bag: ");
            for (int d = 0; d < 7; d++)
            {
                Console.Write(p2Bag[d] + " ");
            }
            Console.SetCursorPosition(75, 16);
            Console.Write(" Score: "+player2Score);
        }

        static void InitializeBoard() {

            for (int i = 0; i < boardArray.GetLength(0); i++)
            {
                for (int j = 0; j < boardArray.GetLength(1); j++)
                {
                    boardArray[i, j] = '.';
                }
            }
            for (int i = 0; i < tempArray.GetLength(0); i++)
            {
                for (int j = 0; j < tempArray.GetLength(1); j++)
                {
                    tempArray[i, j] = '.';
                }
            }
            for (int i = 0; i < previousArray.GetLength(0); i++)
            {
                for (int j = 0; j < previousArray.GetLength(1); j++)
                {
                    previousArray[i, j] = '.';
                }
            }
            /*boardArray[3, 2] = 'D';boardArray[3, 3] = 'E';boardArray[3, 4] = 'S';boardArray[3, 5] = 'K';
                                                                                 boardArray[4, 5] = 'E';
                                                                                 boardArray[5, 5] = 'Y';*/
        }

        static void InitializeBag(char[] bag) {

            int bag_letters;
            for (int i = 0; i < bag.Length; i++)
            {
                do
                {
                    bag_letters = rnd.Next(0, 25);
                    //bag_letters = rnd.Next(0, 28);
                }
                while (letters[bag_letters].amount == 0);
                letters[bag_letters].amount--;
                bag[i] = letters[bag_letters].name;

            }

        }

        static void InitializeWordList() {

            for (int i = 0; i < prevWordList.Length; i++)
            {
                prevWordList[i] = "";
            }

            for (int i = 0; i < tempWordList.Length; i++)
            {
                tempWordList[i] = "";
            }
            for (int i = 0; i < scoringWList.Length; i++)
            {
                scoringWList[i] = "";
            }
        }

        static void InitializeQuery() {

            for (int i = 0; i < player1Query.Length; i++)
            {
                player1Query[i] = '_';
            }
            for (int i = 0; i < player2Query.Length; i++)
            {
                player2Query[i] = '_';
            }

        }

        static void GetDictionary() {
            
            StreamReader srDictionary = File.OpenText("dictionary_1.txt"); 
            int line = 0;
            while (!srDictionary.EndOfStream)
            {
                string str = srDictionary.ReadLine();
                string[] strArray = str.Split(' ');
                vocabulary[line].words = strArray[0];
                line++;
            }
            srDictionary.Close();
        }

        static void GetReservoir() {
            
            StreamReader srReservoir = File.OpenText("letter_reservoir_1.txt");
            int index = 0;
            while (!srReservoir.EndOfStream)
            {
                string str = srReservoir.ReadLine();
                string[] strArray = str.Split(' ');
                letters[index].name = strArray[0].ToCharArray()[0];
                letters[index].point = Convert.ToInt32(strArray[1]);
                letters[index].amount = Convert.ToInt32(strArray[2]);
                index++;
            }
            srReservoir.Close();
        }

        static bool WordControl(char[,] temp) {

            bool wordcontrol = false;

            string prevword = "";
            int prevwordcount = 0;

            for (int i = 0; i < previousArray.GetLength(0); i++)
            {
                for (int j = 0; j < previousArray.GetLength(1); j++)
                {
                    if (!previousArray[i,j].Equals('.')) {

                        prevword += previousArray[i, j];
                    }
                }
                prevWordList[prevwordcount] = prevword;
                prevword = "";
                prevwordcount++;
            }
            prevword = "";

            for (int i = 0; i < previousArray.GetLength(0); i++)
            {
                for (int j = 0; j < previousArray.GetLength(1); j++)
                {
                    if (!previousArray[j, i].Equals('.'))
                    {

                        prevword += previousArray[j, i];
                    }
                }
                prevWordList[prevwordcount] = prevword;
                prevword = "";
                prevwordcount++;
            }

            string tempword = "";
            int tempwordcount = 0;
            
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    if (!temp[i, j].Equals('.'))
                    {

                        tempword += temp[i, j];
                    }
                }
                tempWordList[tempwordcount] = tempword;
                tempword = "";
                tempwordcount++;
            }
            tempword = "";

            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    if (!temp[j, i].Equals('.'))
                    {

                        tempword += temp[j, i];
                    }
                }
                tempWordList[tempwordcount] = tempword;
                tempword = "";
                tempwordcount++;
            }

            for (int i = 0; i < tempWordList.Length; i++)
            {
                for (int j = 0; j < prevWordList.Length; j++)
                {
                    if (tempWordList[i].Equals(prevWordList[j])) {

                        tempWordList[i] = "";
                    }
                }
            }

            for (int i = 0; i < tempWordList.Length; i++)
            {
                if (tempWordList[i].Length < 2) {
                    tempWordList[i] = "";
                }
            }

            int scoringArrayCount = 0;
            
            for (int i = 0; i < vocabulary.Length; i++)
            {
                for (int j = 0; j < tempWordList.Length; j++)
                {
                    string wrd = tempWordList[j];
                    string dic = vocabulary[i].words;
                    if (!wrd.Equals("") && wrd.Equals(dic)) {

                        scoringWList[scoringArrayCount] = tempWordList[j];
                        scoringArrayCount++;
                    }
                }
            }

            int nonEmptyTemp = 0;
            for (int i = 0; i < tempWordList.Length; i++)
            {
                if (!tempWordList[i].Equals("")) {

                    nonEmptyTemp++;
                }
            }
            int comparison = 0;
            for (int i = 0; i < tempWordList.Length; i++)
            {
                for (int j = 0; j < scoringWList.Length; j++)
                {
                    if (!tempWordList[i].Equals("") && tempWordList[i].Equals(scoringWList[j]))
                    {
                        comparison++;
                    }
                }
            }

            Console.SetCursorPosition(23, 23);
            Console.Write("nonEmptyTemp:" + nonEmptyTemp + " comparison:" + comparison);

            if (nonEmptyTemp == comparison)
            {
                wordcontrol = true;
            }
            else 
            {
                wordcontrol = false;
            }

            


            return wordcontrol;
        }

        static void Scoring(string[] t) {

            int localScore = 0;

            

            for (int i = 0; i < t.Length; i++)
            {
                if (!t[i].Equals(""))
                {
                    char[] calculatingWord = t[i].ToCharArray();
                    for (int j = 0; j < calculatingWord.Length; j++)
                    {
                        for (int k = 0; k < letters.Length; k++)
                        {
                            if (calculatingWord[j].Equals(letters[k].name))
                            {
                                localScore += letters[k].point;
                            }
                        }
                    }
                }
            }

            if (round % 2 == 1) {
                player1Score += localScore;
            }
            else if (round % 2 == 0)
            {
                player2Score += localScore;
            }

            for (int i = 0; i < scoringWList.Length; i++)
            {
                scoringWList[i] = "";
            }


        }
        static void ArrayExchange(char[,] a1, char[,] a2) {
            for (int i = 0; i < a1.GetLength(0); i++)
            {
                for (int j = 0; j < a1.GetLength(1); j++)
                {
                    a1[i, j] = a2[i, j];
                }
            }
        }

        static void BagRefill(char[] bag, char[] used, bool operation) {

            if (operation)
            {
                // player put letter on board
                for (int i = 0; i < bag.Length; i++)
                {
                    for (int j = 0; j < used.Length; j++)
                    {
                        if (bag[i].Equals(used[j])) {
                            bag[i] = '-';
                            used[j] = '-';
                        }
                    }
                }

                int bag_letters;
                for (int i = 0; i < bag.Length; i++)
                {
                    if (bag[i].Equals('-')) {
                        do
                        {
                            //bag_letters = rnd.Next(0, 25);
                            bag_letters = rnd.Next(0, 28);
                        }
                        while (letters[bag_letters].amount == 0);
                        letters[bag_letters].amount--;
                        bag[i] = letters[bag_letters].name;
                    }
                }

                for (int i = 0; i < used.Length; i++)
                {
                    used[i] = '.';
                }

            }
            else {
                // player makes return
                

                int bag_letters;
                for (int i = 0; i < bag.Length; i++)
                {
                    for (int j = 0; j < used.Length; j++)
                    {
                        if (bag[i].Equals(used[j]))
                        {
                            for (int k = 0; k < letters.Length; k++)
                            {
                                if (used[j].Equals(letters[k].name)) {
                                    letters[k].amount++;
                                }
                            }
                            bag[i] = '-';
                            used[j] = '-';
                            if (bag[i].Equals('-'))
                            {
                                do
                                {
                                    //bag_letters = rnd.Next(0, 25);
                                    bag_letters = rnd.Next(0, 28);
                                }
                                while (letters[bag_letters].amount == 0);
                                letters[bag_letters].amount--;
                                bag[i] = letters[bag_letters].name;
                            }
                        }
                    }
                    
                }

                for (int i = 0; i < used.Length; i++)
                {
                    used[i] = '.';
                }
            }
        }

        static void Main(string[] args)
        {
            Console.SetWindowPosition(0, 0);
            Console.SetWindowSize(130, 30);
            

            GetDictionary();
            GetReservoir();

            

            
            InitializeBag(p1Bag);
            InitializeBag(p2Bag);
            InitializeQuery();
            
            StartUpScreen();

            int option = Convert.ToInt32(Console.ReadLine());
            
            //int option = 1;

            while (option != 3) {
                switch (option)
                {

                    case 1:
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        InitializeWordList();
                        InitializeBoard();
                        PrintBoard();
                        PrintRoundInfo();
                        PrintPlayerBags();

                        ConsoleKeyInfo cki;

                        Console.SetCursorPosition(cursorx, cursory);

                        
                        
                        bool game = true;

                        while (game) {
                            cki = Console.ReadKey(true);
                          
                            if (cki.Key == ConsoleKey.LeftArrow)
                            {                                 
                                if (cursorx > 2) {
                                    cursorx -= 2;
                                    arrayy--;
                                }     
                            }
                            else if (cki.Key == ConsoleKey.RightArrow) 
                            {                                                               
                                if (cursorx < 30){
                                    cursorx += 2;
                                    arrayy++;
                                }                                
                            }
                            else if (cki.Key == ConsoleKey.UpArrow) 
                            {                                
                                if (cursory > 3)
                                {
                                    cursory--;
                                    arrayx--;
                                }                                
                            }
                            else if (cki.Key == ConsoleKey.DownArrow) 
                            {                              
                                if (cursory < 17)
                                {
                                    cursory++;
                                    arrayx++;
                                }                               
                            }
                            else if (cki.Key == ConsoleKey.Escape) {
                                game = false;
                                break;
                            }
                            
                            if (char.IsLetter(cki.KeyChar)) {


                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                temparrayx = arrayx;
                                temparrayy = arrayy;
                                char[] chchar = cki.KeyChar.ToString().ToUpper().ToCharArray();
                                if (round % 2 == 1) {
                                    for (int i = 0; i < p1Bag.Length; i++)
                                    {
                                        if (chchar[0].Equals(p1Bag[i]) && boardArray[temparrayx, temparrayy]=='.') {
                                            Console.Write(cki.KeyChar.ToString().ToUpper());
                                            tempArray[temparrayx,temparrayy] = chchar[0];
                                            player1BagUsed[i] = chchar[0];
                                            break;
                                        }
                                    }
                                }
                                else if (round % 2 == 0)
                                {
                                    for (int i = 0; i < p2Bag.Length; i++)
                                    {
                                        if (chchar[0].Equals(p2Bag[i]) && boardArray[temparrayx, temparrayy] == '.')
                                        {
                                            Console.Write(cki.KeyChar.ToString().ToUpper());
                                            tempArray[temparrayx, temparrayy] = chchar[0];
                                            player2BagUsed[i] = chchar[0];
                                            break;
                                        }
                                    }
                                }






                            }
                            if (cki.Key == ConsoleKey.Backspace && boardArray[temparrayx, temparrayy]=='.') {
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write(".");

                                temparrayx = arrayx;
                                temparrayy = arrayy;
                                tempArray[temparrayx, temparrayy] = '.';


                            }

                            if (cki.Key == ConsoleKey.Enter) {

                                if (WordControl(tempArray))
                                {
                                    Scoring(scoringWList);
                                    ArrayExchange(boardArray,tempArray);
                                    ArrayExchange(previousArray, tempArray);
                                    
                                    if (round % 2 == 1) {
                                        BagRefill(p1Bag, player1BagUsed,true);
                                    }
                                    else if (round % 2 == 0)
                                    {
                                        BagRefill(p2Bag, player2BagUsed,true);
                                    }


                                    round++;
                                    cursorx = 16;
                                    cursory = 10;
                                    arrayx = 7;
                                    arrayy = 7;
                                    PrintBoard();
                                    PrintRoundInfo();
                                    PrintPlayerBags();
                                    p1QueryChance = 3;
                                    p2QueryChance = 3;


                                }
                                else {
                                    ArrayExchange(tempArray, previousArray);
                                    cursorx = 16;
                                    cursory = 10;
                                    arrayx = 7;
                                    arrayy = 7;
                                    PrintBoard();
                                    PrintRoundInfo();
                                    PrintPlayerBags();
                                    Console.SetCursorPosition(25, 25);
                                    Console.Write("your word(s) not found");
                                }
                                
                                


                            }
                            
                            //ConsoleKey.Spacebar

                            if (cki.Key == ConsoleKey.Multiply){ // pass

                                round++;
                                passCount--;
                                if (passCount == 0) {
                                    game = false;
                                }
                                cursorx = 16;
                                cursory = 10;
                                arrayx = 7;
                                arrayy = 7;
                                PrintBoard();
                                PrintRoundInfo();
                                PrintPlayerBags();
                            }
                            if (cki.Key == ConsoleKey.Tab){  // return                                
                                ReturnOption();
                                cursorx = 16;
                                cursory = 10;
                                arrayx = 7;
                                arrayy = 7;
                                round++;
                                PrintBoard();
                                PrintRoundInfo();
                                PrintPlayerBags();
                            }
                            if (cki.Key == ConsoleKey.Insert){ // query
                                if (round % 2 == 1) {
                                    if (p1QueryChance > 0) {
                                        QueryOption();
                                        InitializeQuery();
                                        queryCountP1 = 0;
                                        queryCountP2 = 0;
                                        cursorx = 16;
                                        cursory = 10;
                                        arrayx = 7;
                                        arrayy = 7;
                                        p1QueryChance--;
                                    }
                                    
                                }
                                else if (round % 2 == 0)
                                {
                                    if (p2QueryChance > 0)
                                    {
                                        QueryOption();
                                        InitializeQuery();
                                        queryCountP1 = 0;
                                        queryCountP2 = 0;
                                        cursorx = 16;
                                        cursory = 10;
                                        arrayx = 7;
                                        arrayy = 7;
                                        p2QueryChance--;
                                    }
                                   
                                }
                                
                            }
                            

                            Console.SetCursorPosition(21, 21);
                            Console.Write("cursorx:" + cursorx + " cursory:" + cursory + " arrayx:" + arrayx + " arrayy:" + arrayy);


                            Console.SetCursorPosition(cursorx, cursory);

                            
                        } // while game

                        Console.Clear();
                        
                            Console.WriteLine(@"     ___    ___    __ __   ___      ___        ___   ___    ");
                            Console.WriteLine(@"    |   |   ___|  |  |  | |___|    |   | \  / |___| |       ");
                            Console.WriteLine(@"    |___|  |___|  |  |  | |___     |___|  \/  |___  |       ");
                            Console.WriteLine(@"        |                                                   ");
                            Console.WriteLine(@"     ___|                                                   ");
                            Console.WriteLine(@"___________________________________________________________ ");
                            Console.WriteLine(" ");
                        if (player1Score > player2Score)
                        {
                            Console.WriteLine(@"     ___  |  ___         ___   ___   |                 ___        ");
                            Console.WriteLine(@"    |   | |  ___| |   | |___| |      |    \  /\  / |  |   |       ");
                            Console.WriteLine(@"    |___| | |___| |___| |___  |      |     \/  \/  |  |   |       ");
                            Console.WriteLine(@"    |                 |                                           ");
                            Console.WriteLine(@"    |              ___|                                           ");

                        }
                        else if (player1Score < player2Score)
                        {
                            Console.WriteLine(@"     ___  |  ___         ___   ___   | |                ___        ");
                            Console.WriteLine(@"    |   | |  ___| |   | |___| |      | |   \  /\  / |  |   |       ");
                            Console.WriteLine(@"    |___| | |___| |___| |___  |      | |    \/  \/  |  |   |       ");
                            Console.WriteLine(@"    |                 |                                            ");
                            Console.WriteLine(@"    |              ___|                                            ");

                        }
                        else {
                            Console.WriteLine(@"   _|_      ___         ");
                            Console.WriteLine(@"    |    | |___|        ");
                            Console.WriteLine(@"    |___ | |___         ");
                            

                        }
                        Console.WriteLine(" ");
                        Console.WriteLine("Press ENTER for Exit");
                        Console.ReadLine();
                        option = 3;
                        break;
                    case 2:
                        Instructıons();
                        Console.SetCursorPosition(25, 18);
                        option = Convert.ToInt32(Console.ReadLine());
                        break;                    
                } // switch
            }  // while option  
            
            
        }
    }
}
