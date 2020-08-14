using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saladpuk.TextAnime
{
    public class TextAnimeBuilder
    {
        private int leftPadding;
        private readonly string displayText;
        private readonly StringBuilder builder;

        public TextAnimeBuilder(string text)
        {
            displayText = text;
            builder = new StringBuilder();
        }

        public TextAnimeBuilder FallStraight(int times = 1)
        {
            const int Lines = 5;
            var totalLines = Lines * times;
            while (totalLines-- > 0)
            {
                builder.Append(displayText).AppendLine();
            }
            return this;
        }

        public TextAnimeBuilder BounceLeft()
        {
            shiftTextToLeft();
            shiftTextToRight();
            return this;

            void shiftTextToLeft()
            {
                var index = 0;
                while (index < displayText.Length)
                {
                    addTextFromIndex(index++);
                }
            }
            void shiftTextToRight()
            {
                var index = displayText.Length;
                while (index > 0)
                {
                    addTextFromIndex(--index);
                }
            }
            void addTextFromIndex(int index)
            {
                var text = displayText.Substring(index);
                builder.Append(text).AppendLine();
            }
        }

        public TextAnimeBuilder BounceRight()
        {
            const int ShiftingTimes = 16;
            moveRight(ShiftingTimes);
            moveLeft(ShiftingTimes);
            return this;
        }

        public TextAnimeBuilder BounceEdge()
        {
            const int maxCharPerLine = 83;
            var maximumPaddingToEdge = maxCharPerLine - displayText.Length;
            moveRight(maximumPaddingToEdge);
            moveLeft(maximumPaddingToEdge);
            return this;
        }

        public TextAnimeBuilder SlideLeft()
        {
            const int ShiftingTimes = 20;
            moveRight(ShiftingTimes);
            moveEachCharacterToTheLeftHandSide();
            return this;

            void moveEachCharacterToTheLeftHandSide()
            {
                // TODO: The code below should be refactor.
                const int DefaultRightPadding = 2;
                const int ShiftOneForActiveCharacter = 1;
                var completedCharacters = new List<char>();
                for (int i = 0; i < displayText.Length; i++)
                {
                    var completed = completedCharacters.Count();
                    var leftText = string.Join(string.Empty, completedCharacters);
                    var rightText = displayText.Substring(completed + ShiftOneForActiveCharacter);
                    var characterToMove = displayText.Skip(completed).FirstOrDefault();
                    var rightPadding = DefaultRightPadding;
                    var requiredAnime = !char.IsWhiteSpace(characterToMove);
                    while (requiredAnime)
                    {
                        var midText = characterToMove.ToString().PadRight(rightPadding).PadLeft(leftPadding);
                        var text = $"{leftText}{midText}{rightText}";
                        builder.Append(text).AppendLine();
                        requiredAnime = ++rightPadding <= leftPadding;
                    }
                    completedCharacters.Add(displayText.Skip(completed).FirstOrDefault());
                }
                const int DefaultLeftPaddingPosition = 0;
                leftPadding = DefaultLeftPaddingPosition;
            }
        }

        public TextAnimeBuilder SlideRight()
        {
            const int ShiftingTimes = 20;
            moveEachCharacterToTheRightHandSide();
            moveLeft(ShiftingTimes);
            return this;

            void moveEachCharacterToTheRightHandSide()
            {
                // TODO: The code below should be refactor.
                leftPadding = ShiftingTimes;
                const int DefaultRightPadding = 2;
                const int ShiftOneForActiveCharacter = 1;
                var completedCharacters = new List<char>();
                for (int i = 0; i < displayText.Length; i++)
                {
                    var completed = completedCharacters.Count();
                    var leftText = displayText.Substring(0, displayText.Length - completed - ShiftOneForActiveCharacter);
                    var rightText = string.Join(string.Empty, string.Join(string.Empty, completedCharacters).Reverse());
                    var characterToMove = displayText.Reverse().Skip(completed).FirstOrDefault();
                    var rightPadding = DefaultRightPadding;
                    var requiredAnime = !char.IsWhiteSpace(characterToMove);
                    while (requiredAnime)
                    {
                        var midText = characterToMove.ToString().PadLeft(rightPadding).PadRight(leftPadding);
                        var text = $"{leftText}{midText}{rightText}";
                        builder.Append(text).AppendLine();
                        requiredAnime = ++rightPadding <= leftPadding;
                    }
                    completedCharacters.Add(displayText.Reverse().Skip(completed).FirstOrDefault());
                }
                leftPadding = 19;
            }
        }

        public string Build() => builder.ToString();

        private void moveLeft(int shiftingTimes)
        {
            while (shiftingTimes-- > 0)
            {
                addTextWithPadding(() => leftPadding--);
            }
        }
        private void moveRight(int shiftingTimes)
        {
            while (shiftingTimes-- > 0)
            {
                addTextWithPadding(() => leftPadding++);
            }
        }
        private void addTextWithPadding(Action onCompleted)
        {
            var padding = leftPadding + displayText.Length;
            var text = displayText.PadLeft(padding);
            builder.Append(text).AppendLine();
            onCompleted();
        }
    }
}
