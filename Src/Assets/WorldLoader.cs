namespace yolo {
    public interface IWorldLoader {
        World LoadWorld();
    }

    public class WorldLoader : IWorldLoader {
        public World LoadWorld() {
            throw new System.NotImplementedException();
        }
    }
}