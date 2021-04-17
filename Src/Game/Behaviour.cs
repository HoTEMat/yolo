namespace yolo {
    public interface IBehaviour {
        Entity Entity { get; }
        void Update(Context ctx);
    }

    public interface IInteractable : IBehaviour {
        void Interact(Context ctx);
    }
}