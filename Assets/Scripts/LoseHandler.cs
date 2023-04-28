using TMPro;
using UnityEngine;


namespace ChickenRoast
{
    public class LoseHandler
    {
        private int loseCount = 0;
        private readonly TextMeshProUGUI loseText;

        public LoseHandler(TextMeshProUGUI loseText)
        {
            this.loseText = loseText;
        }

        public void LoseUpdate()
        {
            loseCount = PlayerPrefs.GetInt("LoseCount", 0);

            loseText.text = loseCount switch
            {
                0 => "Oh you Lose! but don't worry, the developer believes in you.",
                1 => "Oh you Lose Again! it's just a simple Game!",
                2 => "How can you do that?",
                3 => "And lose again, Interesting.",
                4 => "ok I think you must go play another game, this game is for professionals.",
                5 => "........",
                6 => "if you lose one more time, the developer will suicide.",
                7 => "Ha ha im kidding! but seriously, Stop It.",
                _ => "Thanks for Losing and Burning the poor chicken over and over."
            };

            loseCount++;
            PlayerPrefs.SetInt("LoseCount", loseCount);
        }
    }
}