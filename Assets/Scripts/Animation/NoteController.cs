using UnityEngine;

public class NoteController : MonoBehaviour {
    [SerializeField] private Animator _NoteAnimatorController;

    public void FadeOut() {
        _NoteAnimatorController.SetTrigger("ShouldFadeOut");
    }

    public void DestroyNote() {
        Destroy(gameObject);
    }
}
