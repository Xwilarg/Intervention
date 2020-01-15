using System;

public static class LocalRandom
{
    private static Random rand = new Random();

    public static int RandomNumber(int min, int max)
        => rand.Next(min, max);
}
