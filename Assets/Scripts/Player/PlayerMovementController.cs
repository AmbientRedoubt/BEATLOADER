using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
    [SerializeField] private GameObject _player;
    [SerializeField] private Animator _noteHitAnimatorController;
    // [SerializeField] private SpriteRenderer _playerSprite;

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

    private void OnNoteHit(NoteInput noteInput) {
        _noteHitAnimatorController.SetTrigger("NoteHit");

        float yPosition = noteInput.Amplitude;
        Vector2 newPosition = _player.transform.position;
        newPosition.y = yPosition;
        _player.transform.position = newPosition;
    }

    private void OnDestroy() {
        PlayerEvents.OnNoteHit -= OnNoteHit;
    }

    // private IEnumerator NoteHitAnimation() {
    //     _playerSprite.color = Color.green;
    //     yield return new WaitForSeconds(0.1f);
    //     _playerSprite.color = Color.white;
    // }
}
