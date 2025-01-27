using UnityEngine;

public class AnimationEventController : MonoBehaviour {
    [SerializeField] private SpriteRenderer _playerSprite;

    private void Start() {
        _playerSprite.enabled = false;
    }

    private void EnablePlayerSprite() {
        _playerSprite.enabled = true;
    }
}
