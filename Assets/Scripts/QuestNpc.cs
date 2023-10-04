using UnityEngine;

public class QuestNpc : Collideable {
    public static QuestNpc instance;

    public Animator assuranceQuestionAnimator;
    public Animator mathQuestAnimator;
    public bool isQuestAssurancePanelOpened;
    public bool isMathQuestAssurancePanelOpened;

    public GameObject clickLockingPanel;

    private readonly float cooldown = 3.0f;
    private float lastShout = -3.0f; // Instant reply at the beginning.

    private float minimumRequiredDistance = 0.2f;

    private float npcCurrentX;
    private float npcCurrentY;

    private GameObject player;

    private float playerCurrentX;
    private float playerCurrentY;

    private float xDistance;
    private float yDistance;

    // Hmm
    private void Awake() {
        if (instance != null) return;
        instance = this;

        player = GameObject.Find("Player");
    }


    private void Update() {
        CalculateInteractDistance();
    }

    private void CalculateInteractDistance() {
        // Calculating distance between player and npc that effects a panel animation:

        // Assigning required position values into variables: 
        npcCurrentX = transform.position.x;
        npcCurrentY = transform.position.y;

        playerCurrentX = player.transform.position.x;
        playerCurrentY = player.transform.position.y;

        // Determining the distance between required points:
        xDistance = Mathf.Abs(npcCurrentX - playerCurrentX);
        yDistance = Mathf.Abs(npcCurrentY - playerCurrentY);

        var distance = Mathf.Sqrt(xDistance * xDistance + yDistance * yDistance);

        if (distance < 0.18f && !isQuestAssurancePanelOpened)
            // Triggering animation and setting next cooldown:
            if (Time.time - lastShout > cooldown) {
                lastShout = Time.time;
                isQuestAssurancePanelOpened = true;
                assuranceQuestionAnimator.SetTrigger("show");
            }
    }

    // This can wait some time:

    //protected override void OnCollide(Collider2D coll) {
    //    if (coll.gameObject.name == "Player" && !isQuestAssurancePanelOpened) {
    //        if (Time.time - lastShout > cooldown) {
    //            lastShout = Time.time;
    //            isQuestAssurancePanelOpened = true;
    //            assuranceQuestionAnimator.SetTrigger("show");

    //        }
    //    }
    //}

    public void SoundManagerPlay(AudioClip audioClip) {
        SoundManager.instance.audioSource.PlayOneShot(audioClip);
    }

    public void CloseQuestAssurancePanel() {
        isQuestAssurancePanelOpened = false;
        assuranceQuestionAnimator.SetTrigger("hide");
        SoundManagerPlay(SoundManager.instance.buttonTap);
    }

    public void OpenMathQuestPanel() {
        // Call click locking panel:
        clickLockingPanel.SetActive(true);
        isMathQuestAssurancePanelOpened = true;
        //isQuestAssurancePanelOpened = false;
        assuranceQuestionAnimator.SetTrigger("hide");
        mathQuestAnimator.SetTrigger("show");
        SoundManagerPlay(SoundManager.instance.buttonTap);
    }

    public void CloseMathQuestPanel() {
        // Exit button calls this function.
        clickLockingPanel.SetActive(false);
        isMathQuestAssurancePanelOpened = false;
        isQuestAssurancePanelOpened = false;
        mathQuestAnimator.SetTrigger("hide");
    }
}