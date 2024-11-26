using System;
using System.Threading;

class ArabaYarisi
{
    static int pistGenislik = 20;
    static int pistYukseklik = 10;
    static char[,] pist = new char[pistYukseklik, pistGenislik];

    // Arabaların pozisyonları
    static int araba1X = pistGenislik / 4;
    static int araba2X = 3 * pistGenislik / 4;
    static int araba1Y = pistYukseklik - 1;
    static int araba2Y = pistYukseklik - 1;

    static bool oyunDevamEdiyor = true;

    static void Main(string[] args)
    {
        Console.CursorVisible = false; // İmleci gizle
        PistOlustur();

        // Kontrolleri dinlemek için ayrı bir iş parçacığı
        Thread kontrolThread = new Thread(KontrolDinle);
        kontrolThread.Start();

        while (oyunDevamEdiyor)
        {
            PistGuncelle();
            PistCiz();
            Thread.Sleep(200); // Hız kontrolü
        }

        Console.WriteLine("\nOyun bitti! Kazanan belli oldu.");
    }

    // Pist oluşturma
    static void PistOlustur()
    {
        for (int i = 0; i < pistYukseklik; i++)
        {
            for (int j = 0; j < pistGenislik; j++)
            {
                // Pist sınırları
                if (j == 0 || j == pistGenislik - 1)
                    pist[i, j] = '|';
                else
                    pist[i, j] = ' ';
            }
        }
    }

    // Pist güncelleme
    static void PistGuncelle()
    {
        // Arabaların konumlarını pistte işaretle
        for (int i = 0; i < pistYukseklik; i++)
        {
            for (int j = 1; j < pistGenislik - 1; j++)
            {
                pist[i, j] = ' ';
            }
        }

        pist[araba1Y, araba1X] = '1';
        pist[araba2Y, araba2X] = '2';

        // Kazanma kontrolü
        if (araba1Y == 0)
        {
            oyunDevamEdiyor = false;
            Console.Clear();
            Console.WriteLine("Araba 1 kazandı! Tebrikler!");
        }
        else if (araba2Y == 0)
        {
            oyunDevamEdiyor = false;
            Console.Clear();
            Console.WriteLine("Araba 2 kazandı! Tebrikler!");
        }
    }

    // Pist çizimi
    static void PistCiz()
    {
        Console.Clear();
        for (int i = 0; i < pistYukseklik; i++)
        {
            for (int j = 0; j < pistGenislik; j++)
            {
                Console.Write(pist[i, j]);
            }
            Console.WriteLine();
        }
    }

    // Kontrolleri dinler
    static void KontrolDinle()
    {
        while (oyunDevamEdiyor)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                // Araba 1 kontrolleri
                case ConsoleKey.A: // Sola
                    if (araba1X > 1) araba1X--;
                    break;
                case ConsoleKey.D: // Sağa
                    if (araba1X < pistGenislik - 2) araba1X++;
                    break;
                case ConsoleKey.W: // Yukarı
                    if (araba1Y > 0) araba1Y--;
                    break;
                case ConsoleKey.S: // Aşağı
                    if (araba1Y < pistYukseklik - 1) araba1Y++;
                    break;

                // Araba 2 kontrolleri
                case ConsoleKey.LeftArrow: // Sola
                    if (araba2X > 1) araba2X--;
                    break;
                case ConsoleKey.RightArrow: // Sağa
                    if (araba2X < pistGenislik - 2) araba2X++;
                    break;
                case ConsoleKey.UpArrow: // Yukarı
                    if (araba2Y > 0) araba2Y--;
                    break;
                case ConsoleKey.DownArrow: // Aşağı
                    if (araba2Y < pistYukseklik - 1) araba2Y++;
                    break;
            }
        }
    }
}
