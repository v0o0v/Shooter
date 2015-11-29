using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// PlayerManager script.
/// Manages player's data (i.e. hp, life, bomb etc).
/// </summary>
public class PlayerManager : MonoBehaviour
{
    enum UIType
    {
        None, HP, Life, Bomb, Length
    }

    public int hp;
    public int life;
    public int bomb;
    public int level;

    public Transform[] uiHpBar;
    public Transform[] uiLifeBar;
    public Transform[] uiBombBar;
    public Image bombButtonImage;
    public Sprite normalButtonSprite;
    public Sprite disabledButtonSprite;

    public int maxHPCount = 6;
    public int maxBombCount = 3;
    public int maxLevelCount = 3;
    public bool isImmortal = false;

    public bool isTutorial = false;

    private UIScoreManager _scoreManager;
    private Animator _animator;
    private PlayerFire _playerFire;
    private PlayerImmortalEffect _playerImmortal;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _scoreManager = GameObject.Find("Game Manager").GetComponent<UIScoreManager>();
        _playerFire = GetComponent<PlayerFire>();
        _playerImmortal = GetComponent<PlayerImmortalEffect>();
    }

    void ShowBar(UIType uiType, int current)
    {
        if (isTutorial)
            return;

        if (current < 0)
            return;

        Transform[] targetT;
        switch (uiType)
        {
            case UIType.Bomb: targetT = uiBombBar; break;
            case UIType.HP: targetT = uiHpBar; break;
            case UIType.Life: targetT = uiLifeBar; break;
            default: targetT = null; break;
        }

        // targerT should not be null -> Exception.
        if (targetT == null)
            return;

        for (int ix = 0; ix < current; ++ix)
        {
            targetT[ix].gameObject.SetActive(true);
        }

        for (int ix = current; ix < targetT.Length; ++ix)
        {
            targetT[ix].gameObject.SetActive(false);
        }
    }

    public void AddHP(int point)
    {
        if (isTutorial)
            return;

        if (isImmortal && point < 0)
            return;

        hp = Mathf.Clamp(hp + point, 0, maxHPCount);
        SetImmortal(2f);
        //SendMessage("SetImmortal", 2, SendMessageOptions.DontRequireReceiver);

        if (hp == 0)
        {
            // play start animation.
            _animator.enabled = true;
            _animator.Play(_animator.GetAnimatorTransitionInfo(0).nameHash);

            // set immortal mode.
            SetImmortal(5f);
            //SendMessage("SetImmortal", 5, SendMessageOptions.DontRequireReceiver);
            
            // Don't fire missile until player start animation finished.
            _playerFire.SetDisableShoot();

            life -= 1;
            if (life == -1)
            {
                _scoreManager.GameOver();
                Destroy(gameObject);
            }
            else
                hp = 6;

            ShowBar(UIType.Life, life);
        }

        ShowBar(UIType.HP, hp);
    }

    public void AddBomb(int point)
    {
        if (isTutorial)
            return;

        bomb = Mathf.Clamp(bomb + point, 0, maxBombCount);
        ShowBar(UIType.Bomb, bomb);

        if (bomb == 0)
            bombButtonImage.sprite = disabledButtonSprite;
        else
            bombButtonImage.sprite = normalButtonSprite;
    }

    public void AddLevel(int point)
    {
        if (isTutorial)
            return;

        level = Mathf.Clamp(level + point, 1, 3);

        CancelInvoke("DownLevel");
        InvokeRepeating("DownLevel", 8f, 8f);
    }

    public void SetDefault()
    {
        level = 1;
        hp = maxHPCount;
        bomb = maxBombCount;

        ShowBar(UIType.HP, hp);
        ShowBar(UIType.Bomb, bomb);
    }

    void DownLevel()
    {
        level = Mathf.Clamp(level - 1, 1, 4);
    }

    void SetImmortal(float time)
    {
        CancelInvoke("Mortal");
        isImmortal = true;
        Invoke("Mortal", time);

        _playerImmortal.SetImmortal(time);
    }

    void Mortal()
    {
        isImmortal = false;
    }
}