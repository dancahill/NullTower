using UnityEngine;

public abstract class ClickableAbstract : MonoBehaviour, IClickable { //inherit from this to override ClickAction, you can reference via IClickable

    public virtual void ClickAction() {
        print("No Click Action Specified for " + gameObject.name);
    }

}