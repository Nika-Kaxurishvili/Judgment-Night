using UnityEngine;

public class npcScript : MonoBehaviour
{
    [Header("-----Camera-----")]
    public Camera mainCamera;

    [Header("-----GameObjects-----")]
    public GameObject pressEUI;                  // UI რომელიც აჩვენებს "Press E"
    public GameObject[] dialogueBlocks;          // დიალოგის ბლოკები (UI Panel-ები)
    public GameObject playerIs;                  // მოთამაშის GameObject (სკრიპტის ჩასაქრობად დამჭირდა მარტო :D)
    public GameObject staminabar;

    [Header("-----Transforms-----")]
    public Transform player;
    public Transform cameraTargetPosition;       // კამერის პოზიცია დიალოგისთვის
    public Transform playerTargetPosition;       // მოთამაშის პოზიცია დიალოგისთვის

    [Header("-----Floats-----")]
    public float activationDistance = 3f;

    [Header("-----Bool-----")]
    private bool playerNearby = false;
    private bool isInDialogue = false;

    [Header("-----Int-----")]
    private int currentDialogueIndex = 0;

    void Start()
    {
        pressEUI.SetActive(false);
        HideAllDialogueBlocks();
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        // დისტანციის გამოთვლა
        if (distance <= activationDistance && !isInDialogue)
        {
            pressEUI.SetActive(true);
            playerNearby = true;
            //E ღილაკის დაჭერისას
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartDialogue();
            }
        }
        else if (!isInDialogue)
        {
            pressEUI.SetActive(false);
            playerNearby = false;
        }

        if (isInDialogue && Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextDialogueBlock();
        }
    }
    //დიალოგის დასაწყისი
    void StartDialogue()
    {
        isInDialogue = true;
        // GameObject ების გაქრობა
        pressEUI.SetActive(false);
        staminabar.SetActive(false);
        // პოზიციების შეცვლა
        player.position = playerTargetPosition.position;
        player.rotation = playerTargetPosition.rotation;

        mainCamera.transform.position = cameraTargetPosition.position;
        mainCamera.transform.rotation = cameraTargetPosition.rotation;

        playerIs.GetComponent<FirstPersonController>().enabled = false; // გამორთვა დიალოგის დაწყებისას

        currentDialogueIndex = 0;
        ShowNextDialogueBlock();
        Debug.Log("დაელაპარაკა ნიკუშას");
    }
    //შემდეგი დიალოგის ნახვა
    void ShowNextDialogueBlock()
    {
        HideAllDialogueBlocks();

        if (currentDialogueIndex < dialogueBlocks.Length)
        {
            dialogueBlocks[currentDialogueIndex].SetActive(true);
            currentDialogueIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    void HideAllDialogueBlocks()
    {
        foreach (GameObject go in dialogueBlocks)
        {
            go.SetActive(false);
        }
    }
    //დიალოგის დასასრული
    void EndDialogue()
    {
        isInDialogue = false;
        HideAllDialogueBlocks();
        playerIs.GetComponent<FirstPersonController>().enabled = true;
        staminabar.SetActive(true);
    }

}
