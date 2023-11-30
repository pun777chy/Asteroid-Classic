using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

using Zenject;
namespace Asteroids.UI
{
    /// <summary>
    /// Represents a UI screen with associated elements and functionality.
    /// </summary>
    public class ScreenUI : MonoBehaviour
    {
        [SerializeField] private GameObject screen;
        [SerializeField] private Button screenBtn;
        [SerializeField] private TextMeshProUGUI scoreText;
       
        /// <summary>
        /// Set the action to be executed on button click.
        /// </summary>
        /// <param name="onClick">UnityAction to be executed.</param>
        public void SetButtonAction(UnityAction onClick)
        {
            screenBtn.onClick.AddListener(onClick);
        }

        /// <summary>
        /// Show or hide the screen and update the score text.
        /// </summary>
        /// <param name="isShown">Flag indicating whether the screen should be shown.</param>
        /// <param name="score">Survived time to be displayed in the score text.</param>
        public void ShowScreen(bool isShown, string score)
        {
            screen.SetActive(isShown);
            UpdateScoreText(score);
        }

        /// <summary>
        /// Update the score text with the given score.
        /// </summary>
        /// <param name="score">Survived time to be displayed in the score text.</param>
        public void ShowScreenTime(string score)
        {
            UpdateScoreText(score);
        }

        /// <summary>
        /// Update the score text with the given score.
        /// </summary>
        /// <param name="score">Survived time to be displayed in the score text.</param>
        private void UpdateScoreText(string score)
        {
            scoreText.text = "Survived Time: " + score;
        }
      
    }
}
