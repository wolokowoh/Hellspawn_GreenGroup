using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpdateUI : MonoBehaviour
{

    public GameOverVocals vocals;

    public GameObject RoseHealthOrb;
    public GameObject RoseHealthOrbSliderObject;
    private Slider healthSlider;
    public GameObject HealthRefillText;
    public GameObject MagicRefillText;
    public GameObject BloodOrb;
    public GameObject BloodOrbSliderObject;
    private Slider bloodSlider;
    public GameObject PoisonOrb;
    public GameObject PoisonOrbSliderObject;
    private Slider poisonSlider;
    public GameObject ClawsOrb;
    public GameObject IceOrb;
    public GameObject IceOrbSliderObject;
    private Slider iceSlider;

    private GameObject weapon;
    
    private Text HPRefillText;
    private Text MPRefillText;

    public GameObject gameOver;
    public GameObject gameOverImage;
    public GameObject gameOverText;
    private Image GOImg;
    private Text GOTxt;
    public GameObject yesButtonObj;
    public GameObject yesButtonTextObj;
    private Text yesButtonText;
    private Button yesButton;
    public GameObject noButtonObj;
    public GameObject noButtonTextObj;
    private Button noButton;
    private Text noButtonText;


    public GameObject LevelNameTextObj;
    private Text levelNameText;

    public GameObject PickupTextObj;
    private Text pickupText;

    // boss healths
    public GameObject IceBossHealthBar;
    public GameObject IceBossHealthBarSliderObject;
    private Slider iceBossHealthSlider;

    public GameObject PoisonBossHealthBar;
    public GameObject PoisonBossHealthBarSliderObject;
    private Slider poisonBossHealthSlider;

    public GameObject BloodBossHealthBar;
    public GameObject BloodBossHealthBarSliderObject;
    private Slider bloodBossHealthSlider;

    public GameObject TheWardenHealth;
    public GameObject TheWardenIceBarSliderObject;
    private Slider theWardenIceSlider;
    public GameObject TheWardenBloodBarSliderObject;
    private Slider theWardenBloodSlider;
    public GameObject TheWardenPoisonBarSliderObject;
    private Slider theWardenPoisonSlider;


    public GameObject Dialogue;
    public GameObject dialogueTextObject;
    private Text dialogueText;

    public GameObject interaction;
    public Text interactionText;


    private void Awake()
    {
        // everything should be enabled and active at first
    }
    // Start is called before the first frame update
    void Start()
    {

        interaction.SetActive(false);
        Dialogue.SetActive(false);

        iceBossHealthSlider = IceBossHealthBarSliderObject.GetComponent<Slider>();
        poisonBossHealthSlider = PoisonBossHealthBarSliderObject.GetComponent<Slider>();
        bloodBossHealthSlider = BloodBossHealthBarSliderObject.GetComponent<Slider>();

        theWardenBloodSlider = TheWardenBloodBarSliderObject.GetComponent<Slider>();
        theWardenIceSlider = TheWardenIceBarSliderObject.GetComponent<Slider>();
        theWardenPoisonSlider = TheWardenPoisonBarSliderObject.GetComponent<Slider>();

        // set these active when the boss is spawned
        IceBossHealthBar.SetActive(false);
        PoisonBossHealthBar.SetActive(false);
        BloodBossHealthBar.SetActive(false);
        TheWardenHealth.SetActive(false);

        HPRefillText = HealthRefillText.GetComponent<Text>();
        MPRefillText = MagicRefillText.GetComponent<Text>();
        bloodSlider = BloodOrbSliderObject.GetComponent<Slider>();
        poisonSlider = PoisonOrbSliderObject.GetComponent<Slider>();
        iceSlider = IceOrbSliderObject.GetComponent<Slider>();
        healthSlider = RoseHealthOrbSliderObject.GetComponent<Slider>();
        // this way you can't ue the invisible buttons
        gameOver.SetActive(false);
        // set the objects inactive based on current weapon


    }

    // Update is called once per frame
    void Update()
    {
        
        // check other scripts for updates in stats or have the game manager call the public version
    }

    public void CutOnAndDisplayInteractionText(string textToDisplay)
    {
        interaction.SetActive(true);
        interactionText.text = textToDisplay;

    }
    public void CutOffInteractionText()
    {
        interaction.SetActive(false);
    }
    public void cutOnDialogueAndSetText(string dialogueTextBlock)
    {
        Dialogue.SetActive(true);
        dialogueText = dialogueTextObject.GetComponent<Text>();
        dialogueText.text = dialogueTextBlock;
    }
    public void changeDialogTextToNextMessage(string dialogueTextBlock)
    {
        dialogueText.text = dialogueTextBlock;
    }
    public void cutOffDialogue()
    {
        Dialogue.SetActive(false);
    }
    public string getCurrentDialogueString()
    {
        return dialogueText.text;
    }
    public void cutOnIceBossHealth()
    {
        IceBossHealthBar.SetActive(true);
    }
    public void cutOffIceBossHealth()
    {
        IceBossHealthBar.SetActive(false);
    }
    public void cutOnPoisonBossHealth()
    {
        PoisonBossHealthBar.SetActive(true);
    }
    public void cutOffPoisonBossHealth()
    {
        PoisonBossHealthBar.SetActive(false);
    }
    public void cutOnBloodBossHealth()
    {
        BloodBossHealthBar.SetActive(true);
    }
    public void cutOffBloodBossHealth()
    {
        BloodBossHealthBar.SetActive(false);
    }
    public void cutOnWardenBossHealth()
    {
        IceBossHealthBar.SetActive(true);
    }
    public void cutOffWardenBossHealth()
    {
        IceBossHealthBar.SetActive(false);
    }

    public void displayPickupMessage(string pickupMessage)
    {
        pickupText = PickupTextObj.GetComponent<Text>();
        pickupText.text = pickupMessage;
        var tempColor = pickupText.color;
        tempColor.a = 1f;
        pickupText.color = tempColor;
        StartCoroutine("FadePickupText");
    }
    public void displayLevelName(string levelName)
    {
        levelNameText = LevelNameTextObj.GetComponent<Text>();
        levelNameText.text = levelName;
        var tempColor = levelNameText.color;
        tempColor.a = 1f;
        levelNameText.color = tempColor;
        StartCoroutine("FadeLevelText");
    }
    public IEnumerator FadePickupText()
    {
        float waitTime = 0.15f;
        int repeat = 25;
        for (int i = repeat; i > 0; i--)
        {
            yield return new WaitForSeconds(waitTime);
            var tempColor = pickupText.color;
            float reduction = 1f / repeat;
            float currentValue = tempColor.a;
            currentValue -= reduction;
            if (currentValue <= 0)
            {
                currentValue = 0;
            }
            tempColor.a = currentValue;
            pickupText.color = tempColor;
        }
    }

    public IEnumerator FadeLevelText()
    {
        float waitTime = 0.15f;
        int repeat = 25; 
        for(int i = repeat; i > 0; i--)
        {
            yield return new WaitForSeconds(waitTime);
            var tempColor = levelNameText.color;
            float reduction = 1f / repeat;
            float currentValue = tempColor.a;
            currentValue -= reduction;
            if(currentValue<=0)
            {
                currentValue = 0;
            }
            tempColor.a = currentValue;
            levelNameText.color = tempColor;
        }
        
    }
    public IEnumerator GameOver()
    {
        gameOver.SetActive(true);

        AudioSource[] deathClips = vocals.GetDeathClips();
        int randDeath = Random.Range(0, vocals.GetDeathClips().GetLength(0));
        AudioSource deathClipToPlay = deathClips[randDeath];

        GOImg = gameOverImage.GetComponent<Image>();
        GOTxt = gameOverText.GetComponent<Text>();
        deathClipToPlay.Play();
        for (int i = 76; i > 0; i--)
        {   
            yield return new WaitForSeconds(.1f);
            var temporaryColor = GOImg.color;
            float increase = 1f / 76;
            float currentValue = temporaryColor.a;
            currentValue += increase;
            if (currentValue >= 1)
            {
                currentValue = 1;
            }
            temporaryColor.a = currentValue;
            GOImg.color = temporaryColor;

            
            if( i > 10)
            {
                temporaryColor = GOTxt.color;
                float incr = 1f / 76;
                float curValue = temporaryColor.a;
                currentValue += incr;
                if (currentValue >= 1)
                {
                    currentValue = 1;
                }
                temporaryColor.a = currentValue;
                
                GOTxt.color = temporaryColor;
            }

        }
        

        
        // wait however long you want to before the menu pops up or we can use it for Noah's mockery
        yesButtonText = yesButtonTextObj.GetComponent<Text>();
        noButtonText= noButtonTextObj.GetComponent<Text>();
        
        
        // makes things visible before they are transparent by default to let you work on the scene
        var tempColor = GOImg.color;
        tempColor.a = 1f;
        GOImg.color = tempColor;
        tempColor = GOTxt.color;
        tempColor.a = 1f;
        GOTxt.color = tempColor;
        tempColor = yesButtonText.color;
        tempColor.a = 1f;
        yesButtonText.color = tempColor;
        tempColor = noButtonText.color;
        tempColor.a = 1f;
        noButtonText.color = tempColor;

        // we need to add in the laughing somewhere


    }


    public void setWeaponToCurrent(GameObject currentWeaponUIObject)
    {
        if(weapon == null) // NEW SCENE WILL MAKE IT NULL // make sure to call this  
            // in start method of GameManager and when the weapon changes
        {
            // no previous weapon so just set it
            weapon = currentWeaponUIObject;
            // these need to be active at the start of the scene or there will be an error
            BloodOrb.SetActive(false);
            ClawsOrb.SetActive(false);
            PoisonOrb.SetActive(false);
            IceOrb.SetActive(false);
            // this way it only displays your current
            weapon.SetActive(true);
        }
        else
        {
            // before we change the weapon
            weapon.SetActive(false);

            weapon = currentWeaponUIObject;
            weapon.SetActive(true);
        }

    }

    public void changeHPRefillText(int numberOfHPRefills)
    {
        HPRefillText.text = numberOfHPRefills.ToString();
    }
    public void changeMPRefillText(int numberOfMPRefills)
    {
        MPRefillText.text = numberOfMPRefills.ToString();
    }
    public void changeBloodSliderValue(float currentBloodMP, float maxBloodMP)
    {
        float newValue = currentBloodMP / maxBloodMP;
        bloodSlider.value = newValue;
    }
    public void changePoisonSliderValue(float currentPoisonMP, float maxPoisonMP)
    {
        float newValue = currentPoisonMP / maxPoisonMP;
        poisonSlider.value = newValue;
    }
    public void changeIceSliderValue(float currentIceMP, float maxIceMP)
    {
        float newValue = currentIceMP / maxIceMP;
        iceSlider.value = newValue;
    }
    public void changeHealthSliderValue(float currentHP, float maxHP)
    {
        float newValue = currentHP / maxHP;
        healthSlider.value = newValue;
    }


    public void changeIceBossHealthSliderValue(int bossCurrentHP, int bossMaxHP)
    {
        float newValue = bossCurrentHP / bossMaxHP;
        iceBossHealthSlider.value = newValue;
    }
    public void changePoisonBossHealthSliderValue(int bossCurrentHP, int bossMaxHP)
    {
        float newValue = bossCurrentHP / bossMaxHP;
        poisonBossHealthSlider.value = newValue;
    }
    public void changeBloodBossHealthSliderValue(int bossCurrentHP, int bossMaxHP)
    {
        float newValue = bossCurrentHP / bossMaxHP;
        bloodBossHealthSlider.value = newValue;
    }
    // for the warden we need three max hps and three current ones. Once one runs out whe swap the bar
    // just use an if to check its zero in order
    public void changeWardenBloodSliderValue(int bossCurrentBloodHP, int bossBloodMaxHP)
    {
        float newValue = bossCurrentBloodHP / bossBloodMaxHP;
        theWardenBloodSlider.value = newValue;
    }
    public void changeWardenPoisonSliderValue(int bossCurrentPoisonHP, int bossPoisonMaxHP)
    {
        float newValue = bossCurrentPoisonHP / bossPoisonMaxHP;
        theWardenPoisonSlider.value = newValue;
    }
    public void changeWardenIceSliderValue(int bossCurrentIceHP, int bossIceMaxHP)
    {
        float newValue = bossCurrentIceHP / bossIceMaxHP;
        theWardenIceSlider.value = newValue;
    }
}
