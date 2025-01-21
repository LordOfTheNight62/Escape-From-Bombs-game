namespace Escape_From_Bombs
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
            int coin = 0;
            (int x, int y) player = (5, 7);

            string welcomeText =
            """
            ---------------------------------------------------------            
            |  NASIL OYNANIR?                                       |
            |                                                       |
            |  -> W-A-S-D ile engellere çarpmadan hareket et        |
            |  -> Hedefin bombalara basmadan 5 adet altın toplamak  |              
            |  -> Eğer bombalara basarsan kaybedersin               |                           
            |  ** LÜTFEN TAM EKRAN OYNAYIN                          |
            |                                                       |
            |        BAŞLAMAK İÇİN HERHANGİ BİR TUŞA BAS            |
            |                                                       |
            |                                                       |
            |       / \      _-'                                    |
            |    _/|  \-''- _ /                                     |
            __-' { |          \                                     |
                /             \                                     |
                /       "o.  |o }                                   |
                |            \ ;                                    |
                              ',                                    |
                   \_         __\                                   |
                     ''-_    \.//                                   |
                       / '-____'                                    |
                      /                                             |
                    _'                                              |
                  _-'                                               |
            ---------------------------------------------------------
            """;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(welcomeText + "\n");
            Console.ResetColor();
            Console.ReadKey(true);

            while (!isGameOverState)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Coin {coin}/5\n");
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
                    char key = Console.ReadKey(true).KeyChar;
                    map[player.x, player.y] = ".";

                    switch (key)
                    {
                        case 'w' or 'W':
                            if (player.x > 0) player.x--;
                            break;
                        case 's' or 'S':
                            if (player.x < rows - 1) player.x++;
                            break;
                        case 'a' or 'A':
                            if (player.y > 0) player.y--;
                            break;
                        case 'd' or 'D':
                            if (player.y < columns - 1) player.y++;
                            break;
                    }

                    if (isGameOver((player.x, player.y), map))
                        isGameOverState = true;
                    if (isCoin((player.x, player.y), map))
                    {
                        coin++;
                    }
                    if (coin == 5)
                        break;

                    map[player.x, player.y] = "G";
                }
                Task.Delay(400).Wait();
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            if (coin == 5)
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

        private static bool isGameOver((int x, int y) location, string[,] map)
        {
            if (map[location.x, location.y] == "*")
                return true;
            else
                return false;
        }

        private static bool isCoin((int x, int y) location, string[,] map)
        {
            if (map[location.x, location.y] == "o")
                return true;
            else
                return false;
        }
    }
}
