using ChronometerAS;

public class Program
{
    public static void Main()
    {
        var chronometar = new Chronometer();

        string line;

        while ((line=Console.ReadLine())!="exit")
        {
            if (line=="start")
            {
                Task.Run(() =>
                {
                    chronometar.Start();
                });
            }

            else if (line=="stop")
            {
                chronometar.Stop(); 
            }
            else if (line=="lap")
            {
                Console.WriteLine(chronometar.Lap());
            }

            else if (line=="laps")
            {
                if (chronometar.Laps.Count==0)
                {
                    Console.WriteLine($"Laps: no laps");
                    continue;
                }

                Console.WriteLine($"Laps: ");

                    for (int i = 0; i < chronometar.Laps.Count; i++)
                {
                    Console.WriteLine($"{i}. {chronometar.Laps[i]}");
                }
            }
            else if (line=="reset")
            {
                chronometar.Reset();
            }
            else if (line=="time")
            {
                Console.WriteLine(chronometar.GetTime);
            }
        }

        chronometar.Stop();
    }

}
