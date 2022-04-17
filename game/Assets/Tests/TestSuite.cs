using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class TestSuite
{
    /// <summary>
    /// Возвращет имя текущей сцены
    /// </summary>
    private string ActiveSceneName { get => SceneManager.GetActiveScene().name; }

    /// <summary>
    /// Тестирует загрузку сцены меню
    /// </summary>
    [UnityTest]
    public IEnumerator TestMenuLoaded()
    {
		SceneManager.LoadScene("Menu");
        
        yield return null;

        Assert.AreEqual("Menu", ActiveSceneName);
    }

    /// <summary>
    /// Тестирует нажатие клавиши старта игры
    /// </summary>
    [UnityTest]
    public IEnumerator TestCheckClickStartButton()
    {
		SceneManager.LoadScene("Menu");

        yield return null;
        
        TestHelper.ClickButton("NewGame");

        yield return null;

        Assert.AreEqual("FirstLevel", ActiveSceneName);
    }

    /// <summary>
    /// Тестирует присутствие главных компонентов на сцене игры
    /// </summary>
    [UnityTest]
    public IEnumerator TestPlayerIsOnFirstLevel()
    {
		SceneManager.LoadScene("FirstLevel");

        yield return null;

        Assert.That(UnityEngine.Object.FindObjectOfType<Player>(), Is.Not.Null);
        TestHelper.AssertComponents(new string[] {"MainCharacter", "End (Idle)", "Ground", "Start (Idle)"});
    }

    /// <summary>
    /// Тестирует победу игрока
    /// </summary>
    [UnityTest]
    public IEnumerator TestWinning()
    {
		SceneManager.LoadScene("FirstLevel");

        yield return null;

        Player player = UnityEngine.Object.FindObjectOfType<Player>();
        GameObject endFlag = GameObject.Find("End (Idle)");

        player.transform.position = endFlag.transform.position;

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual("Finish", ActiveSceneName);
    }

    /// <summary>
    /// Тестирует проигрыш игрока
    /// </summary>
    [UnityTest]
    public IEnumerator TestLosing()
    {
		SceneManager.LoadScene("FirstLevel");

        yield return null;

        Player player = UnityEngine.Object.FindObjectOfType<Player>();
        GameObject death = GameObject.Find("DeathTrigger");

        player.transform.position = death.transform.position;
	    
        yield return new WaitForSeconds(1f);

        Assert.AreEqual("YouLost", ActiveSceneName);
    }
	
    /// <summary>
    /// Тестирует нанесение урона игроку
    /// </summary>
    [UnityTest]
    public IEnumerator TestItHurts()
    {
		SceneManager.LoadScene("FirstLevel");

        yield return null;

        Player player = UnityEngine.Object.FindObjectOfType<Player>();
        GameObject trap = GameObject.Find("Trap");

        trap.transform.position = player.transform.position;
	    
        yield return new WaitForSeconds(3f);

        Assert.AreEqual("YouLost", ActiveSceneName);
    }

    /// <summary>
    /// Тестирует нанесение урона врагу
    /// </summary>
    [UnityTest]
    public IEnumerator TestPlayerDamaging()
    {
        SceneManager.LoadScene("FirstLevel");

        yield return null;

        Player player = UnityEngine.Object.FindObjectOfType<Player>();
        GameObject enemy = GameObject.Find("ShootingEnemy");
            
        enemy.transform.position = player.transform.position;
        for (int i = 0; i < 5; i++) player.Shooting();
    
        yield return new WaitForSeconds(3f);

        Assert.IsTrue(enemy == null);
    }
}
