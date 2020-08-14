using System;

namespace Saladpuk.TextAnime
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = "saladpuk.com";
            var builder = new TextAnimeBuilder(text);
            var result = builder
                .FallStraight(6)
                .BounceRight()
                .BounceLeft()
                .BounceRight()
                .BounceLeft()
                .BounceRight()
                .BounceLeft()
                .FallStraight()
                .SlideLeft()
                .SlideLeft()
                .FallStraight(6)
                .SlideRight()
                .BounceRightEdge()
                .BounceRightEdge()
                .Build();
            Console.WriteLine(result);
        }
    }
}
