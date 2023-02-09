using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public void SceneChange(string _name)
    {
        SceneManager.LoadScene(_name);
    }
}
