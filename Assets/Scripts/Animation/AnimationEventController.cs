using UnityEngine;

public class AnimationEventController : MonoBehaviour {
    [SerializeField] private SpriteRenderer _playerSprite;
    [SerializeField] private SpriteRenderer _noteHitSprite;
    [SerializeField] private Sprite _underscoreSprite;

    private void Start() {
        _playerSprite.enabled = false;
        _noteHitSprite.enabled = false;
    }

    // Called by the intro animation event
    private void EnablePlayerSprite() {
        _playerSprite.enabled = true;
        _noteHitSprite.enabled = true;
    }

    // Replace the intro animation with the underscore sprite
    private void ReplaceIntroAnimation() {
        GetComponent<SpriteRenderer>().sprite = _underscoreSprite;
    }
}
