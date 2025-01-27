using UnityEngine;

public class AnimationEventController : MonoBehaviour {
    [SerializeField] private SpriteRenderer _playerSprite;

    private void Start() {
        _playerSprite.enabled = false;
    }

    // Called by the intro animation event
    private void EnablePlayerSprite() {
        _playerSprite.enabled = true;
    }
}
