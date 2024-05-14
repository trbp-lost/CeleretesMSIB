using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public int characterLocation;
    public TextMeshProUGUI dialogueArea;
    public GameObject panel;
    public Player player;

    private Queue<DialogueLine> lines;
    [SerializeField] private List<RectTransform> iconCharShowed;
    public List<RectTransform> iconLocation;

    [HideInInspector] public bool isDialogueActive = false;
    public float typingSpeed = .07f;
    public Animator animator;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();

        panel.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        player.canControl = false;
        //animator.Play("show");
        panel.SetActive(true);

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        DiscoloredIcon();

        characterIcon.sprite = currentLine.character.icon;
        characterLocation = currentLine.character.iconLocation;
        characterName.text = currentLine.character.name;



        RectTransform newImageChar = CharacterIcon();
        //newImageChar

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void EndDialogue()
    {
        CleanDialogue();
        isDialogueActive = false;
        //animator.Play("hide");
        player.canControl = true;
        panel.SetActive(false);

    }

    private RectTransform CharacterIcon()
    {
        RectTransform spawnCharIcon = Instantiate(characterIcon, iconLocation[characterLocation - 1]).GetComponent<RectTransform>();
        spawnCharIcon.localPosition = Vector3.zero;
        spawnCharIcon.gameObject.SetActive(true);

        iconCharShowed.Insert(characterLocation - 1, spawnCharIcon);
        return spawnCharIcon;
    }

    private void DiscoloredIcon()
    {
        if (iconCharShowed == null) return;

        Debug.Log("aaaa");
        float changeColor = 65f / 255f;
        foreach (RectTransform x in iconCharShowed)
        {
            Image img = x.GetComponent<Image>();
            img.color = new Color(changeColor, changeColor, changeColor);
        }
    }

    private void CleanDialogue()
    {
        if (iconCharShowed != null) iconCharShowed.Clear();
        Image[] images = GameObject.Find("Canvas").GetComponentsInChildren<Image>(true);

        foreach (Image image in images)
        {
            if (image.name == "CharImage(Clone)")
            {
                Destroy(image.gameObject);
            }
        }
    }
}
