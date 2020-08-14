using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Saladpuk.TextAnime
{
    class Program
    {
        static void Main(string[] args)
        {
            var shouldShowTutorial = !args.Any();
            if (shouldShowTutorial)
            {
                showTutorial();
                return;
            }

            var result = generateText(args);
            createOutputFile(result);
            Console.WriteLine(result);
        }

        static void showTutorial()
        {
            var msg = new StringBuilder()
                    .Append("Usage: [text] [Commands]")
                    .AppendLine()
                    .AppendLine()
                    .Append("Text: The text that you want to used to play the animation.")
                    .AppendLine()
                    .AppendLine()
                    .Append("Commands:")
                    .AppendLine()
                    .Append("fs - Wording will fall straight.").AppendLine()
                    .Append("bl - Wording will bounce to the left.").AppendLine()
                    .Append("br - Wording will bounce to the right.").AppendLine()
                    .Append("be - Wording will bounce to the edge.").AppendLine()
                    .Append("sl - Wording will slide to the left.").AppendLine()
                    .Append("sr - Wording will slide to the right.").AppendLine()
                    .AppendLine()
                    .Append("[Example]")
                    .AppendLine()
                    .Append("dotnet run saladpuk fs bl br sl sr")
                    .AppendLine()
                    .AppendLine()
                    .Append("When finish, you will see the file named 'output.txt' ;)")
                    .ToString();
            Console.WriteLine(msg);
        }
        static string generateText(string[] args)
        {
            var builder = new TextAnimeBuilder(getText());
            foreach (var cmd in getCommands())
            {
                switch (cmd)
                {
                    case AnimeCommand.FS:
                        builder.FallStraight();
                        break;
                    case AnimeCommand.BL:
                        builder.BounceLeft();
                        break;
                    case AnimeCommand.BR:
                        builder.BounceRight();
                        break;
                    case AnimeCommand.BE:
                        builder.BounceEdge();
                        break;
                    case AnimeCommand.SL:
                        builder.SlideLeft();
                        break;
                    case AnimeCommand.SR:
                        builder.SlideRight();
                        break;
                    default:
                    case AnimeCommand.Unknow:
                        break;
                }
            }
            return builder.Build();

            IEnumerable<AnimeCommand> getCommands()
            {
                const int SkipText = 1;
                return args.Skip(SkipText).Where(it => null != it)
                  .Select(it => Enum.TryParse(it, true, out AnimeCommand cmd) ? cmd : AnimeCommand.Unknow)
                  .Where(it => it != AnimeCommand.Unknow);
            }
            string getText() => args.FirstOrDefault();
        }
        static void createOutputFile(string result)
        {
            using var writer = File.CreateText("output.txt");
            writer.WriteLine(result);
        }

        enum AnimeCommand
        {
            Unknow,
            FS,
            BL, BR, BE,
            SL, SR,
        }
    }
}
