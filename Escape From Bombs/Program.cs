using System.Numerics;

namespace Escape_From_Bombs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string[,] map = new string[25, 50];
            int rows = map.GetLength(0), columns = map.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    map[i, j] = ".";
                }
            }
            bool isGameOverState = false;
            int coin = 0, superPower = 3;
            (int x, int y) player = (5, 7);
            (int x, int y) arrow = (0, 0);

            string welcomeText =
            """
            ----------------------------------------------------------            
            |  NASIL OYNANIR?                                        |
            |                                                        |
            |  -> W-A-S-D ile bombalara basmadan hareket et          |
            |  -> Hedefin bombalara basmadan 10 adet altın toplamak  |              
            |  -> Eğer bombalara basarsan kaybedersin                |
            |  -> 3 adet süper gücün var, ok tuşları ile yönü        |
            |  ayarlayıp SPACE tuşuna basarsan okun gösterdiği       |
            |  alandaki her şey temizlenir.                          |
            |                                                        |
            |  ** LÜTFEN TAM EKRAN OYNAYIN                           |
            |                                                        |
            |        BAŞLAMAK İÇİN HERHANGİ BİR TUŞA BAS             |
            |                                                        |
            |                                                        |
            |       / \      _-'                                     |
            |    _/|  \-''- _ /                                      |
            __-' { |          \                                      |
                /             \                                      |
                /       "o.  |o }                                    |
                |            \ ;                                     |
                              ',                                     |
                   \_         __\                                    |
                     ''-_    \.//                                    |
                       / '-____'                                     |
                      /                                              |
                    _'                                               |
                  _-'                                                |
            ----------------------------------------------------------
            """;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(welcomeText);
            Console.ResetColor();
            Console.ReadKey(true);

            while (!isGameOverState)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Coin {coin}/10");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Süper Gücünüz {superPower}/3");
                Console.ResetColor();
                Console.WriteLine();
                map[player.x, player.y] = "G";
                if (DateTime.Now.Second % 10 == 0)
                    RandomCoin(map);
                RandomBoom(map);
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (map[i, j] == "G")
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("G ");
                            Console.ResetColor();
                        }
                        else if (map[i, j] == "o")
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("o ");
                            Console.ResetColor();
                        }
                        else if (map[i, j] == "*")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("* ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(map[i, j] + " ");
                        }
                    }
                    Console.WriteLine();
                }

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    map[player.x, player.y] = ".";

                    switch (key.KeyChar)
                    {
                        case 'w' or 'W':
                            ClearArrow((player.x, player.y), map);
                            if (player.x > 0) player.x--;
                            break;
                        case 's' or 'S':
                            ClearArrow((player.x, player.y), map);
                            if (player.x < rows - 1) player.x++;
                            break;
                        case 'a' or 'A':
                            ClearArrow((player.x, player.y), map);
                            if (player.y > 0) player.y--;
                            break;
                        case 'd' or 'D':
                            ClearArrow((player.x, player.y), map);
                            if (player.y < columns - 1) player.y++;
                            break;
                    }

                    if (IsGameOver((player.x, player.y), map))
                    {
                        isGameOverState = true;
                    }
                    map[player.x, player.y] = "G";

                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (player.x > 1)
                            {
                                ClearArrow((player.x, player.y), map);
                                map[player.x - 1, player.y] = "↑";
                                arrow.x = player.x - 1;
                                arrow.y = player.y;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (player.x < rows - 1)
                            {
                                ClearArrow((player.x, player.y), map);
                                map[player.x + 1, player.y] = "↓";
                                arrow.x = player.x + 1;
                                arrow.y = player.y;
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (player.y > 0)
                            {
                                ClearArrow((player.x, player.y), map);
                                map[player.x, player.y - 1] = "←";
                                arrow.x = player.x;
                                arrow.y = player.y - 1;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            if (player.y < columns - 1)
                            {
                                ClearArrow((player.x, player.y), map);
                                map[player.x, player.y + 1] = "→";
                                arrow.x = player.x;
                                arrow.y = player.y + 1;
                            }
                            break;
                        case ConsoleKey.Spacebar:
                            if(superPower <= 0)
                            {
                                break;
                            }
                            superPower--;
                            if(arrow.y < player.y)
                            {
                                for (int i = 0; i < player.y; i++)
                                {
                                    map[player.x, i] = ".";
                                }
                            }
                            else if(arrow.y > player.y)
                            {
                                for (int i = player.y; i < map.GetLength(1); i++)
                                {
                                    map[player.x, i] = ".";
                                }
                            }
                            else if (arrow.x < player.x)
                            {
                                for (int i = 0; i < player.x; i++)
                                {
                                    map[i, player.y] = ".";
                                }
                            }
                            else if (arrow.x > player.x)
                            {
                                for (int i = player.x; i < map.GetLength(0); i++)
                                {
                                    map[i, player.y] = ".";
                                }
                            }
                            break;
                    }

                    if (IsCoin((player.x, player.y), map))
                    {
                        coin++;
                    }
                    if (coin == 10)
                        break;
                }
                Task.Delay(400).Wait();
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            if (coin == 10)
            {

                Console.WriteLine("""
                                
                                           ___    ___
                                          ( _<    >_ )
                                          //        \\                     
                                          \\___..___//
                                           `-(    )-'
                                             _|__|_
                                            /_|__|_\
                                            /_|__|_\
                                            /_\__/_\
                                             \ || /  _)
                                               ||   ( )
                                               \\___//
                                                `---'

                              
                         __     ______  _    _     __          _______ _   _ 
                         \ \   / / __ \| |  | |    \ \        / /_   _| \ | |
                          \ \_/ / |  | | |  | |     \ \  /\  / /  | | |  \| |
                           \   /| |  | | |  | |      \ \/  \/ /   | | | . ` |
                            | | | |__| | |__| |       \  /\  /   _| |_| |\  |
                            |_|  \____/ \____/         \/  \/   |_____|_| \_|
                                                  

                
                        """);
            }
            else
            {
                Console.WriteLine("""
                                
                 ________  ________  _____ ______   _______           ________  ___      ___ _______   ________     
                |\   ____\|\   __  \|\   _ \  _   \|\  ___ \         |\   __  \|\  \    /  /|\  ___ \ |\   __  \    
                \ \  \___|\ \  \|\  \ \  \\\__\ \  \ \   __/|        \ \  \|\  \ \  \  /  / | \   __/|\ \  \|\  \   
                 \ \  \  __\ \   __  \ \  \\|__| \  \ \  \_|/__       \ \  \\\  \ \  \/  / / \ \  \_|/_\ \   _  _\  
                  \ \  \|\  \ \  \ \  \ \  \    \ \  \ \  \_|\ \       \ \  \\\  \ \    / /   \ \  \_|\ \ \  \\  \| 
                   \ \_______\ \__\ \__\ \__\    \ \__\ \_______\       \ \_______\ \__/ /     \ \_______\ \__\\ _\ 
                    \|_______|\|__|\|__|\|__|     \|__|\|_______|        \|_______|\|__|/       \|_______|\|__|\|__|



                
                """);
            }

            Console.ResetColor();
            Console.WriteLine("Çıkmak için herhangi bir tuşa basın");
            Console.ReadKey(true);
        }

        private static void RandomBoom(string[,] map)
        {
            Random rnd = new Random();
            int randomRow = rnd.Next(map.GetLength(0));
            int randomColumn = rnd.Next(map.GetLength(1));
            if (map[randomRow, randomColumn] == "G" || map[randomRow, randomColumn] == "*")
                return;
            map[randomRow, randomColumn] = "*";
        }

        private static void RandomCoin(string[,] map)
        {
            Random rnd = new Random();
            int randomRow = rnd.Next(map.GetLength(0));
            int randomColumn = rnd.Next(map.GetLength(1));
            if (map[randomRow, randomColumn] == "G" || map[randomRow, randomColumn] == "*")
                return;
            map[randomRow, randomColumn] = "o";
        }

        private static bool IsGameOver((int x, int y) location, string[,] map)
        {
            if (map[location.x, location.y] == "*")
                return true;
            else
                return false;
        }

        private static bool IsCoin((int x, int y) location, string[,] map)
        {
            if (map[location.x, location.y] == "o")
                return true;
            else
                return false;
        }

        private static void ClearArrow((int x, int y) location, string[,] map)
        {
            (int, int)[] arrowsLocation = // calculating arrows location
            {
                (location.x-1, location.y ),
                (location.x+1, location.y ),
                (location.x, location.y - 1),
                (location.x, location.y + 1)
            };
            foreach (var (arrowX, arrowY) in arrowsLocation)
            {
                if (arrowX >= 0 && arrowX < map.GetLength(0) && arrowY >= 0 && arrowY < map.GetLength(1))
                {
                    if (map[arrowX, arrowY] == "↑" || map[arrowX, arrowY] == "↓" || map[arrowX, arrowY] == "←" || map[arrowX, arrowY] == "→")
                    {
                        map[arrowX, arrowY] = ".";
                    }
                }
            }

        }
    }
}
