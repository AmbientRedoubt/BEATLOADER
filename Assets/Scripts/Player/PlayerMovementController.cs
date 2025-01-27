using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
    [SerializeField] private Animator _noteHitAnimatorController;
    [SerializeField] private SpriteRenderer _playerSprite;

    private void Awake() {
        PlayerEvents.OnNoteHit += OnNoteHit;
    }

    private void OnUp() {
        // Debug.Log("Move");
        // transform.position = new Vector3(transform.position.x, yPosition * positionMultiplier, transform.position.z);
    }

    private void OnDown() {
        // Debug.Log("Move");
    }

    private void OnNoteHit(NoteInput _) {
        _noteHitAnimatorController.SetTrigger("NoteHit");
    }

    private void OnDestroy() {
        PlayerEvents.OnNoteHit -= OnNoteHit;
    }
}
