namespace Escape_From_Bombs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[,] map = new string[30, 50];
            int rows = map.GetLength(0), columns = map.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    map[i, j] = ".";
                }
            }
            bool isGameOverState = false;
            (int x, int y) player = (5, 7);

            string welcomeText =
            """
            -----------------------------------------------------            
            |  NASIL OYNANIR?                                   |
            |                                                   |
            |  -> W A S D ile engellere çarpmadan hareket et    |
            |  -> Eğer kırmızı engellere çarparsan kaybedersin  |                           
            |  ** LÜTFEN TAM EKRAN OYNAYIN                      |
            |                                                   |
            |        BAŞLAMAK İÇİN HERHANGİ BİR TUŞA BAS        |
            -----------------------------------------------------
            """;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(welcomeText + "\n");
            Console.ResetColor();
            Console.ReadKey(true);

            while (!isGameOverState)
            {
                Console.Clear();
                map[player.x, player.y] = "G";
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

                    map[player.x, player.y] = "G";
                }
                Task.Delay(400).Wait();
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("""
                                
                 ________  ________  _____ ______   _______           ________  ___      ___ _______   ________     
                |\   ____\|\   __  \|\   _ \  _   \|\  ___ \         |\   __  \|\  \    /  /|\  ___ \ |\   __  \    
                \ \  \___|\ \  \|\  \ \  \\\__\ \  \ \   __/|        \ \  \|\  \ \  \  /  / | \   __/|\ \  \|\  \   
                 \ \  \  __\ \   __  \ \  \\|__| \  \ \  \_|/__       \ \  \\\  \ \  \/  / / \ \  \_|/_\ \   _  _\  
                  \ \  \|\  \ \  \ \  \ \  \    \ \  \ \  \_|\ \       \ \  \\\  \ \    / /   \ \  \_|\ \ \  \\  \| 
                   \ \_______\ \__\ \__\ \__\    \ \__\ \_______\       \ \_______\ \__/ /     \ \_______\ \__\\ _\ 
                    \|_______|\|__|\|__|\|__|     \|__|\|_______|        \|_______|\|__|/       \|_______|\|__|\|__|



                
                """);
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

        private static bool isGameOver((int x, int y) location, string[,] map)
        {
            if (map[location.x, location.y] == "*")
                return true;
            else
                return false;
        }
    }
}
