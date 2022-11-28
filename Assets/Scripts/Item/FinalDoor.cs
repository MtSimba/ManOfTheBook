
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class FinalDoor : Interactable
{
    // Start is called before the first frame update

    public PlayableDirector timeline;
    public GameObject outroCanvas;

    public override void Interact()
    {
        base.Interact();

        if (HasKey())
            PlayLevel1Outro();

    }

    // Update is called once per frame
    bool HasKey()
    {
        var item = Inventory.instance.items.FirstOrDefault(e => e.name == "key");
        if (item != null)
        {
            Debug.LogWarning("has key");
            return true;
        }
        else
        {
            Debug.LogWarning("has no key");
            return false;
        }
    }

    private void PlayLevel1Outro()
    {
        outroCanvas.SetActive(true);
        timeline.Play();
        Destroy(gameObject);
    }
}
