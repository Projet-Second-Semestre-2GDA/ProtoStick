using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GestionMenu : MonoBehaviour
    {
        public void PlayOnPlayerMode()
        {
            PlayerOptionChoose.ModeDeJoueur = PlayerMode.OnePlayer;
            EnterNextScene();
        }

        public void PlayTwoPlayerMode()
        {
            PlayerOptionChoose.ModeDeJoueur = PlayerMode.TwoPlayer;
            EnterNextScene();
        }

        private void EnterNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}