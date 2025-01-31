using UnityEngine;

public class AnimationEventController : MonoBehaviour {
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField] private SpriteRenderer _noteHitRenderer;
    [SerializeField] private SpriteRenderer _animationSpriteRenderer;
    [SerializeField] private GameObject _introAnimationController;
    [SerializeField] private GameObject _underscoreSprite;
    private void Start() {
        _playerSpriteRenderer.enabled = false;
        _noteHitRenderer.enabled = false;
        _underscoreSprite.SetActive(false);
        _introAnimationController.SetActive(true);
    }

    // Replace the intro animation with the underscore sprite
    private void ReplaceIntroAnimation() {
        _playerSpriteRenderer.enabled = true;
        _noteHitRenderer.enabled = true;
        _underscoreSprite.SetActive(true);
        _introAnimationController.SetActive(false);
    }
}
